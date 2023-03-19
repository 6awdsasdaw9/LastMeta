using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Data.States;
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
    public class HeroMovement : MonoBehaviour, ISavedData
    {
        private MovementLimiter _movementLimiter;
        private PlayerConfig _config;

        [Title("Components")] 
        [SerializeField] private Rigidbody _body;
        [SerializeField] private HeroCollision _collision;

        [HideInInspector] public float directionX;
        private float _maxSpeed = 10f;
        private Vector2 _desiredVelocity;
        private Vector2 _velocity;
        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;

        public bool isCrouch { get; private set; }

        private bool pressingMove;
        private bool pressingCrouch;

        [Inject]
        private void Construct(InputService input, MovementLimiter limiter, ConfigData configData,
            SavedDataCollection dataCollection)
        {
            input.PlayerCrochEvent += OnPressCrouch;
            input.PlayerMovementEvent += OnPressMovement;

            _movementLimiter = limiter;
            _movementLimiter.OnDisableMovementMode += StopMovement;

            _config = configData.playerConfig;
            _maxSpeed = configData.playerConfig.maxSpeed;

            dataCollection.Add(this);
        }


        private void Update()
        {
            SetDesiredVelocity();
            Rotation();
            Crouch();
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;
            MoveWithAcceleration();
        }

        private void OnPressMovement(InputAction.CallbackContext context)
        {
            if (_movementLimiter.charactersCanMove)
                directionX = context.ReadValue<float>();

            pressingMove = directionX != 0;
        }


        private void OnPressCrouch(InputAction.CallbackContext context)
        {
            pressingCrouch = context.started;
        }

        private void Crouch()
        {
            if (pressingCrouch && _collision.onGround)
            {
                isCrouch = true;
            }
            else if(!_collision.underCeiling)
            {
                isCrouch = false;
            }
        }

        private void Rotation()
        {
            if (directionX != 0)
                transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
        }

        private void SetDesiredVelocity() =>
            _desiredVelocity = new Vector2(directionX, 0f) * _maxSpeed;

        private void StopMovement()
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

        public void LoadData(SavedData savedData)
        {
            if (savedData.heroPositionData.level != CurrentLevel() ||
                savedData.heroPositionData.position.AsUnityVector() == Vector3.zero)
                return;

            Vector3Data savedPosition = savedData.heroPositionData.position;
            transform.position = savedPosition.AsUnityVector();
        }

        public void SaveData(SavedData savedData) =>
            savedData.heroPositionData =
                new HeroPositionData(CurrentLevel(), transform.position.AsVectorData());

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}