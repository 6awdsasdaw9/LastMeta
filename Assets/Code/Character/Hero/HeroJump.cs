using Code.Data.GameData;
using Code.Data.States;
using Code.Debugers;
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
        private MovementLimiter _movementLimiter;
        private HeroConfig _config;

        [Title("Components")] 
        [SerializeField] private HeroMovement _move;
        [SerializeField] private HeroCollision _collision;
        [SerializeField] private Rigidbody _body;

        [Title("Jumping Stats")] private float _jumpHeight = 7.3f;
        private int _maxAirJumps;

        //Calculations
        private const float _defaultGravityScale = 1;
        private Vector2 _velocity;
        private float _jumpSpeed;
        private float _gravMultiplier;
        private bool _desiredJump;
        private float _jumpBufferCounter;
        private float _coyoteTimeCounter;
        private bool _currentlyJumping;
        private bool _canJumpAgain;


        [Inject]
        private void Construct(InputService input, MovementLimiter limiter, ConfigData configData)
        {
            input.PlayerJumpEvent += OnJump;

            _movementLimiter = limiter;
            _config = configData.heroConfig;

            _jumpHeight = configData.heroConfig.jumpHeight;
            _maxAirJumps = configData.heroConfig.maxAirJumps;
        }

        private void Update()
        {
            CheckJumpBuffer();
            CheckCoyoteTime();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (!_movementLimiter.charactersCanMove || _move.isCrouch) 
                return;

            if (context.started)
            {
                _desiredJump = true;
            }

        }

        private void CheckCoyoteTime()
        {
            if (!_currentlyJumping && !_collision.onGround)
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


   
        private void CalculateGravity()
        {
            switch (_body.velocity.y)
            {
                case > 0.01f when _collision.onGround:
                    _gravMultiplier = _defaultGravityScale;
                    break;
                case > 0.01f:
                    _gravMultiplier = _config.upwardMovementMultiplier;
                    break;
                case < -0.01f when _collision.onGround:
                    _gravMultiplier = _defaultGravityScale;
                    break;
                case < -0.01f:
                    _gravMultiplier = _config.downwardMovementMultiplier;
                    break;
                default:
                {
                    if (_collision.onGround)
                    {
                        _currentlyJumping = false;
                    }

                    _gravMultiplier = _defaultGravityScale;
                    break;
                }
            }
            _body.velocity = new Vector3(_velocity.x, Mathf.Clamp(_velocity.y, -_config.speedLimit, 100));
        }

        private void Jump()
        {
            if (!IsCanJump()) return;
            
            _desiredJump = false;
            _jumpBufferCounter = 0;
            _coyoteTimeCounter = 0;
            _jumpSpeed = _jumpHeight;

  
            _canJumpAgain = _maxAirJumps == 1 && _canJumpAgain == false;

            switch (_velocity.y)
            {
                case > 0f:
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                    break;
                case < 0f:
                    _jumpSpeed += Mathf.Abs(_body.velocity.y);
                    break;
            }
            
            _velocity.y += _jumpSpeed;
            _currentlyJumping = true;
        }

        private bool IsCanJump()
        {
            return _collision.onGround || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < _config.coyoteTime) ||
                   _canJumpAgain;
        }
    }
}