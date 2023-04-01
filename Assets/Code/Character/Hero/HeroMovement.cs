using System.Linq;
using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Data.States;
using Code.Services;
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
        private HeroConfig _config;
        private InputService _input;

        [Title("Components")] [SerializeField] private Rigidbody _body;
        [SerializeField] private HeroCollision _collision;
        [SerializeField] private HeroAttack _attack;

        [HideInInspector] public float directionX;
        private float _maxSpeed;
        private Vector2 _desiredVelocity;
        private Vector2 _velocity;
        private Vector2 _supportVelocity;
        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;
        private float _bonusSpeed;
        
        public bool isCrouch { get; private set; }

        private bool pressingMove;
        private bool pressingCrouch;

        private const float _maxSupportVelocity = 1f;
        private const float _supportVelocityMultiplayer = 0.15f;

        [Inject]
        private void Construct(InputService input, MovementLimiter limiter, ConfigData configData,
            SavedDataCollection dataCollection)
        {
            _input = input;
            _movementLimiter = limiter;

            _input.PlayerCrochEvent += OnPressCrouch;
            _input.PlayerMovementEvent += OnPressMovement;
            _movementLimiter.OnDisableMovementMode += StopMovement;

            _config = configData.heroConfig;
            //_maxSpeed = configData.heroConfig.maxSpeed;

            /*
            _bonusSpeed = configData.heroConfig.Config
                .FirstOrDefault(s => s.Param == BonusConfig.ParamType.Speed && s.Lvl == 1)?.Value ?? 1;*/

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

        private void OnDestroy()
        {
            _input.PlayerCrochEvent -= OnPressCrouch;
            _input.PlayerMovementEvent -= OnPressMovement;
            _movementLimiter.OnDisableMovementMode -= StopMovement;
        }

        [Button]
        public void LevelUpSpeed()
        {
            _maxSpeed = _config.Config
                .FirstOrDefault(s => s.Param == ParamType.Speed && s.Lvl == 1)?.Value ?? 1;
        }

        public void SetSupportVelocity(Vector2 supportVelocity)
        {
            if (directionX != 0)
            {
                if ((directionX > 0 && supportVelocity.x > 0) ||(directionX < 0 && supportVelocity.x < 0))
                {
                    _supportVelocity = new Vector2(supportVelocity.x,0) * _supportVelocityMultiplayer * 0.7f;
                }
                else
                {
                    _supportVelocity = new Vector2(supportVelocity.x,0) * _supportVelocityMultiplayer;
                }
                
            }
            else 
                _supportVelocity = new Vector2(supportVelocity.x,0);
            
            if (_supportVelocity.x > _maxSupportVelocity)
                _supportVelocity = new Vector2(_maxSupportVelocity, 0);
            else if(_supportVelocity.x < -_maxSupportVelocity)
                _supportVelocity = new Vector2(-_maxSupportVelocity,0);
        }



        private void OnPressMovement(InputAction.CallbackContext context)
        {
            if (_movementLimiter.charactersCanMove)
            {
                directionX = context.ReadValue<float>();

                if (_attack && _attack.attackIsActive) 
                    directionX = 0;
            }
            pressingMove = directionX != 0;
        }


        private void OnPressCrouch(InputAction.CallbackContext context) =>
            pressingCrouch = context.started;

        private void Crouch()
        {
            if (pressingCrouch && _collision.onGround)
                isCrouch = true;
            else if (!_collision.underCeiling)
                isCrouch = false;
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
            _body.velocity = Vector3.zero;
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

            _body.velocity = _velocity + _supportVelocity;
        }

        public void LoadData(SavedData savedData)
        {
            _maxSpeed = savedData.heroParamData.speed;
            
            if (savedData.heroPositionData.scene != CurrentLevel() ||
                savedData.heroPositionData.position.AsUnityVector() == Vector3.zero)
                return;

            Vector3Data savedPosition = savedData.heroPositionData.position;
            transform.position = savedPosition.AsUnityVector();
        }

        public void SaveData(SavedData savedData) =>
            savedData.heroPositionData =
                new PositionData(CurrentLevel(), transform.position.AsVectorData());

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}