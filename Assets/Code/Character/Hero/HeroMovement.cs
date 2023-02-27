using Code.Data;
using Code.Data.DataPersistence;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(HeroCollision))]
    public class HeroMovement : MonoBehaviour, IMovement, IDataPersistence
    {
        [Header("Components")] [SerializeField]
        private MovementLimiter _movementLimiter;

        [SerializeField] private Rigidbody _body;
        [SerializeField] private HeroCollision _collision;
        private InputController _input;

        [Header("Movement Stats")] [SerializeField, Range(0f, 20f)]
        private float maxSpeed = 10f;

        [SerializeField, Range(0f, 1f)] private float _crouchSpeed = 0.5f;
        [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 52f;
        [SerializeField, Range(0f, 100f)] private float _maxDeceleration = 52f;
        [SerializeField, Range(0f, 100f)] private float _maxTurnSpeed = 80f;
        [SerializeField, Range(0f, 100f)] public float _maxAirAcceleration;
        [SerializeField, Range(0f, 100f)] public float _maxAirDeceleration;
        [SerializeField, Range(0f, 100f)] public float _maxAirTurnSpeed = 80f;


        [Header("Calculations")] public float directionX;
        private Vector2 _desiredVelocity;
        private Vector2 _velocity;
        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;


        [Header("Current State")] private bool pressingMove;
        private bool pressingCrouch;

        public bool isCrouch => _collision.onGround && pressingCrouch;

        [Inject]
        private void Construct(InputController input, MovementLimiter limiter)
        {
            //_input = input;
            input.PlayerCrochEvent += OnCrouch;
            input.PlayerMovementEvent += OnMovement;
            _movementLimiter = limiter;
            _movementLimiter.OnDisableMovementMode += StopMovement;
        }


        private void Update()
        {
            SetDesiredVelocity();
            Rotation();
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;
            MoveWithAcceleration();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (_movementLimiter.charactersCanMove)
                directionX = context.ReadValue<float>();

            pressingMove = directionX != 0;
        }


        public Vector2 GetVelocity() =>
            _velocity;

        private void OnCrouch(InputAction.CallbackContext context) =>
            pressingCrouch = context.started;

        private void Rotation()
        {
            if (directionX != 0)
                transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
        }

        private void SetDesiredVelocity() =>
            _desiredVelocity = new Vector2(directionX, 0f) * maxSpeed;

        public void StopMovement()
        {
            directionX = 0;
            pressingCrouch = false;
            pressingMove = false;
        }


        private void MoveWithAcceleration()
        {
            _acceleration = _collision.onGround ? _maxAcceleration : _maxAirAcceleration;
            _deceleration = _collision.onGround ? _maxDeceleration : _maxAirDeceleration;
            _turnSpeed = _collision.onGround ? _maxTurnSpeed : _maxAirTurnSpeed;

            if (pressingMove)
            {
                if (Mathf.Sign(directionX) != Mathf.Sign(_velocity.x))
                    _maxSpeedChange = _turnSpeed * Time.deltaTime;
                else
                    _maxSpeedChange = _acceleration * Time.deltaTime;
            }
            else
            {
                _maxSpeedChange = _deceleration * Time.deltaTime;
            }

            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange) *
                          (isCrouch ? _crouchSpeed : 1);

            _body.velocity = _velocity;
        }

        public void LoadData(ProgressData progressData)
        {
            if (progressData.worldData.positionOnLevel.level != CurrentLevel() ||
                progressData.worldData.positionOnLevel == null)
                return;

            Vector3Data savedPosition = progressData.worldData.positionOnLevel.position;
            transform.position = savedPosition.AsUnityVector();
        }

        public void SaveData(ProgressData progressData) =>
            progressData.worldData.positionOnLevel =
                new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}