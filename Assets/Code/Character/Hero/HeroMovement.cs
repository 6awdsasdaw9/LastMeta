using Code.Data.Configs;
using Code.Data.DataPersistence;
using Code.Data.Stats;
using Code.Debugers;
using Code.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(HeroCollision))]
    public class HeroMovement : MonoBehaviour, IMovement, IDataPersistence
    {
        private MovementLimiter _movementLimiter;
        private PlayerConfig _config;

        [Title("Components")] 
        [SerializeField] private Rigidbody _body;
        [SerializeField] private HeroCollision _collision;
        
        private float _maxSpeed = 10f;

        [HideInInspector] public float directionX;
        private Vector2 _desiredVelocity;
        private Vector2 _velocity;
        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;


        public bool isCrouch => _collision.onGround && pressingCrouch;
        private bool pressingMove;
        private bool pressingCrouch;



        [Inject]
        private void Construct(InputController input, MovementLimiter limiter,GameConfig config)
        {
            input.PlayerCrochEvent += OnCrouch;
            input.PlayerMovementEvent += OnMovement;
            
            _movementLimiter = limiter;
            _movementLimiter.OnDisableMovementMode += StopMovement;
            
            _config = config.playerConfig;
            _maxSpeed = config.playerConfig.maxSpeed;
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
            _desiredVelocity = new Vector2(directionX, 0f) * _maxSpeed;

        public void StopMovement()
        {
            directionX = 0;
            pressingCrouch = false;
            pressingMove = false;
        }


        private void MoveWithAcceleration()
        {
            _acceleration = _collision.onGround ? _config.maxAcceleration : _config.maxAirAcceleration;
            _deceleration = _collision.onGround ? _config.maxDeceleration : _config.maxAirDeceleration;
            _turnSpeed = _collision.onGround ? _config.maxTurnSpeed : _config.maxAirTurnSpeed;

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
                          (isCrouch ? _config.crouchSpeed : 1);

            _body.velocity = _velocity;
        }

        public void LoadData(ProgressData progressData)
        {
            if (progressData.worldData.positionOnLevel.level != CurrentLevel() ||
                progressData.worldData.positionOnLevel == null)
                return;

            Vector3Data savedPosition = progressData.worldData.positionOnLevel.position;
            transform.position = savedPosition.AsUnityVector();
            Log.ColorLog("Hero Loaded");
        }

        public void SaveData(ProgressData progressData) =>
            progressData.worldData.positionOnLevel =
                new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}