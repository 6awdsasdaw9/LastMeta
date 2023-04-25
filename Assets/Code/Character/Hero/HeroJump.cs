using Code.Data.Configs;
using Code.Data.States;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Sirenix.OdinInspector;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(HeroCollision))]
    public class HeroJump : MonoBehaviour
    {
        private InputService _input;
        private MovementLimiter _movementLimiter;
        private HeroConfig _config;

        [Title("Components")] 
        [SerializeField] private HeroMovement _move;
        [SerializeField] private HeroCollision _collision;
        [SerializeField] private HeroAudio _heroAudio;
        [SerializeField] private Rigidbody _body;

        #region Values
        private float _jumpHeight = 7.3f;

        private int _maxAirJumps;
        //Calculations
        private Vector2 _velocity;
        private float _jumpSpeed;
        private bool _desiredJump;
        private float _jumpBufferCounter;
        private float _coyoteTimeCounter;
        private bool _canJumpAgain;
        private float _upgradeJumpHeight;

        public bool CurrentlyJumping { get; private set; }

        #endregion

        #region Run Time
        [Inject]
        private void Construct(InputService input, MovementLimiter limiter, GameConfig gameConfig)
        {
            _input = input;

            _movementLimiter = limiter;
            _config = gameConfig.heroConfig;

            _jumpHeight = gameConfig.heroConfig.jumpHeight;
            _maxAirJumps = gameConfig.heroConfig.maxAirJumps;
        }

        private void Start() => 
            _input.PlayerJumpEvent += OnJump;

        private void Update()
        {
            CheckJumpBuffer();
            CheckCoyoteTime();
        }
        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            if (_desiredJump)
            {
                Jump();
                _body.velocity = _velocity;
                return;
            }

            CalculateGravity();
        }

        private void OnDestroy() => 
            _input.PlayerJumpEvent -= OnJump;
        
        #endregion

        #region Input
        
        private void OnJump(InputAction.CallbackContext context)
        {
            if (!_movementLimiter.charactersCanMove || _move.isCrouch) 
                return;

            if (context.started)
            {
                _desiredJump = true;
            }

        }
        #endregion

        #region Conditions
        private bool IsCanJump()
        {
            return _collision.onGround || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < _config.coyoteTime) ||
                   _canJumpAgain;
        }
        private void CheckCoyoteTime()
        {
            if (!CurrentlyJumping && !_collision.onGround)
                _coyoteTimeCounter += Time.deltaTime;
            else
                _coyoteTimeCounter = 0;
        }

        private void CheckJumpBuffer()
        {
            if (!(_config.jumpBuffer > 0) || !_desiredJump) 
                return;
            
            _jumpBufferCounter += Time.deltaTime;

            if (_jumpBufferCounter > _config.jumpBuffer)
            {
                _desiredJump = false;
                _jumpBufferCounter = 0;
            }
        }
        
        #endregion

        #region Ser Params

        public void SetUpgradeJumpHeight(float value) => 
            _upgradeJumpHeight = value;

        public void SetMaxAirJump(int maxJump) => 
            _maxAirJumps = maxJump;

        private void CalculateGravity()
        {
            switch (_body.velocity.y)
            {
                case > 0.01f when _collision.onGround:
                    break;
                case > 0.01f:
                    break;
                case < -0.01f when _collision.onGround:
                    break;
                case < -0.01f:
                    break;
                default:
                {
                    if (_collision.onGround)
                    {
                        CurrentlyJumping = false;
                    }

                    break;
                }
            }
            _body.velocity = new Vector3(_velocity.x, Mathf.Clamp(_velocity.y, -_config.speedLimit, 100));
        }

        #endregion
        

        private void Jump()
        {
            if (!IsCanJump()) 
                return;
            
            if(!CurrentlyJumping)
                _heroAudio.PlayJump();
            
            _desiredJump = false;
            _jumpBufferCounter = 0;
            _coyoteTimeCounter = 0;
            
            _canJumpAgain = _maxAirJumps >= 1 && _canJumpAgain == false;

            switch (_velocity.y)
            {
                case > 0f:
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                    break;
                case < 0f:
                    _jumpSpeed += Mathf.Abs(_body.velocity.y);

                    break;
            }
            
            _velocity.y += _jumpHeight + _upgradeJumpHeight;
            CurrentlyJumping = true;
        }
    }
}