using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Services;
using Code.Services.Input;
using Code.Services.SaveServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;
using HeroConfig = Code.Data.Configs.HeroConfig;


namespace Code.Character.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroMovement : MonoBehaviour, IHeroMovement
    {
        [SerializeField] private Rigidbody _body;

        private IHero _hero;
        private HeroParams _heroParams;
        private InputService _input;

        #region Values

        public bool IsCrouch { get; private set; }
        public float DirectionX => _directionX;
        public float Speed => _hero.Stats.Speed  * _hero.Stats.ModeSpeedMultiplayer;

        private float _directionX;
        private Vector2 _desiredVelocity;
        private Vector2 _velocity;
        private Vector2 _supportVelocity;

        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;

        private bool _heroCanMove = true;
        private bool _pressingMove;
        private bool _pressingCrouch;

        private const float _maxSupportVelocity = 1f;
        private const float _supportVelocityMultiplayer = 0.15f;

        #endregion

        #region Run Time

        [Inject]
        private void Construct(InputService input, HeroConfig heroConfig, SavedDataStorage dataStorage)
        {
            _hero = GetComponent<IHero>();
            _input = input;
            _heroParams = heroConfig.HeroParams;
            //dataCollection.Add(this);
        }

        private void OnEnable() =>
            SubscribeToEvent(true);

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

        private void OnDestroy() =>
            SubscribeToEvent(false);

        #endregion

        #region Events

        private void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _input.OnPressCrouch += OnPressCrouch;
                _input.OnPressMovement += OnPressMovement;
            }
            else
            {
                _input.OnPressCrouch -= OnPressCrouch;
                _input.OnPressMovement -= OnPressMovement;
            }
        }


        public void BlockMovement()
        {
            _directionX = 0;
            _heroCanMove = false;
            _pressingCrouch = false;
            _pressingMove = false;
            _body.velocity = Vector3.zero;
        }

        public void UnBlockMovement()
        {
            _heroCanMove = true;
            _directionX = _input.GetDirection();
        }

        #endregion

        #region Input

        private void OnPressMovement(InputAction.CallbackContext context)
        {
            if (_heroCanMove)
            {
                _directionX = context.ReadValue<float>();
            }

            _pressingMove = DirectionX != 0;
        }

        private void OnPressCrouch(InputAction.CallbackContext context) =>
            _pressingCrouch = context.started;

        #endregion

        #region Velocity

        public void SetBonusSpeed(float bonusSpeed)
        {
            if (bonusSpeed == 0)
            {
                _supportVelocity = Vector2.zero;
            }
            else
            {
                _supportVelocity = new Vector2(transform.localScale.x, 0) * bonusSpeed;
            }
        }

        public void SetSupportVelocity(Vector2 otherObjectVelocity)
        {
            if (_directionX != 0)
            {
                if ((_directionX > 0 && otherObjectVelocity.x > 0) || (DirectionX < 0 && otherObjectVelocity.x < 0))
                {
                    _supportVelocity = new Vector2(otherObjectVelocity.x, 0) * _supportVelocityMultiplayer * 0.7f;
                }
                else
                {
                    _supportVelocity = new Vector2(otherObjectVelocity.x, 0) * _supportVelocityMultiplayer;
                }
            }
            else
                _supportVelocity = new Vector2(otherObjectVelocity.x, 0);

            _supportVelocity = _supportVelocity.x switch
            {
                > _maxSupportVelocity => new Vector2(_maxSupportVelocity, 0),
                < -_maxSupportVelocity => new Vector2(-_maxSupportVelocity, 0),
                _ => _supportVelocity
            };
        }


        private void SetDesiredVelocity() => 
            _desiredVelocity = new Vector2(_directionX, 0f) * _hero.Stats.Speed * _hero.Stats.ModeSpeedMultiplayer;

        #endregion

        #region Movement

        private void MoveWithAcceleration()
        {
            _acceleration = _hero.Collision.OnGround ? _heroParams.maxAcceleration : _heroParams.maxAirAcceleration;
            _deceleration = _hero.Collision.OnGround ? _heroParams.maxDeceleration : _heroParams.maxAirDeceleration;
            _turnSpeed = _hero.Collision.OnGround ? _heroParams.maxTurnSpeed : _heroParams.maxAirTurnSpeed;

            if (_pressingMove)
            {
                if (Mathf.Sign(DirectionX) != Mathf.Sign(_velocity.x))
                    _maxSpeedChange = _turnSpeed * Time.deltaTime;
                else
                    _maxSpeedChange = _acceleration * Time.deltaTime;
            }
            else
            {
                _maxSpeedChange = _deceleration * Time.deltaTime;
            }

            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange) *
                          (IsCrouch ? _heroParams.crouchSpeed : 1);

            if (_heroCanMove)
            {
                _body.velocity = _velocity + _supportVelocity;
            }
            else
            {
                _body.velocity = _supportVelocity;
            }
        }

        private void Rotation()
        {
            if (DirectionX != 0)
                transform.localScale = new Vector3(DirectionX > 0 ? 1 : -1, 1, 1);
        }

        private void Crouch()
        {
            if (_pressingCrouch && _hero.Collision.OnGround)
                IsCrouch = true;
            else if (!_hero.Collision.UnderCeiling)
                IsCrouch = false;
        }

        #endregion


        public void Disable()
        {
            BlockMovement();
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
            _heroCanMove = true;
        }
    }
}