using Code.Data.Configs;
using Code.Data.GameData;
using Code.Debugers;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroJump : MonoBehaviour, IHeroJump
    {
        [SerializeField] private Rigidbody _body;

        private IHero _hero;
        private InputService _input;
        private HeroConfig _config;

        #region Values

        private float _jumpHeight = 7.3f;
        private int _maxAirJumps;

        //Calculations
        private Vector2 _velocity;
        private float _jumpSpeed;
        private float _jumpBufferCounter;
        private float _coyoteTimeCounter;

        //States
        private bool _isCanJump = true;
        private bool _isCanJumpAgain;
        private bool _isDesiredJump;
        private bool _isCurrentlyJumping;

        #endregion

        #region Run Time

        [Inject]
        private void Construct(InputService input, GameConfig gameConfig)
        {
            _hero = GetComponent<IHero>();
            _input = input;
            _config = gameConfig.heroConfig;
            _jumpHeight = gameConfig.heroConfig.jumpHeight;
            _maxAirJumps = gameConfig.heroConfig.maxAirJumps;
        }

        private void OnEnable() => 
            Enable();

        private void Update()
        {
            CheckJumpBuffer();
            CheckCoyoteTime();
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            if (_isDesiredJump)
            {
                Jump();
                _body.velocity = _velocity;
                return;
            }

            CalculateGravity();
        }

        private void OnDisable() => 
            Disable();

        #endregion

        #region Input

        private void OnJump(InputAction.CallbackContext context)
        {
            if (_hero.Movement.IsCrouch)
                return;

            if (context.started)
                _isDesiredJump = true;
        }

        #endregion

        #region Conditions

        private bool IsCanJump() =>
            _hero.Collision.OnGround
            || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < _config.coyoteTime)
            || _isCanJumpAgain;

        private void CheckCoyoteTime()
        {
            if (!_isCurrentlyJumping && !_hero.Collision.OnGround)
                _coyoteTimeCounter += Time.deltaTime;
            else
                _coyoteTimeCounter = 0;
        }

        private void CheckJumpBuffer()
        {
            if (!(_config.jumpBuffer > 0) || !_isDesiredJump)
                return;

            _jumpBufferCounter += Time.deltaTime;

            if (_jumpBufferCounter > _config.jumpBuffer)
            {
                _isDesiredJump = false;
                _jumpBufferCounter = 0;
            }
        }

        #endregion

        #region Jumping

        private void CalculateGravity()
        {
            switch (_body.velocity.y)
            {
                case > 0.01f when _hero.Collision.OnGround:
                    break;
                case > 0.01f:
                    break;
                case < -0.01f when _hero.Collision.OnGround:
                    break;
                case < -0.01f:
                    break;
                default:
                {
                    if (_hero.Collision.OnGround)
                    {
                        _isCurrentlyJumping = false;
                    }

                    break;
                }
            }

            _body.velocity = new Vector3(_velocity.x, Mathf.Clamp(_velocity.y, -_config.speedLimit, 100));
        }

        private void Jump()
        {
            if (!IsCanJump())
                return;

            if (!_isCurrentlyJumping)
                _hero.Audio.PlayJump();

            _isDesiredJump = false;
            _jumpBufferCounter = 0;
            _coyoteTimeCounter = 0;

            //TODO непотребство 
            var airJump = _maxAirJumps;
            if (_hero.Upgrade != null)
            {
                airJump += _hero.Upgrade.BonusAirJump;
            }

            _isCanJumpAgain = airJump >= 1 && _isCanJumpAgain == false;

            switch (_velocity.y)
            {
                case > 0f:
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                    break;
                case < 0f:
                    _jumpSpeed += Mathf.Abs(_body.velocity.y);

                    break;
            }

            //TODO непотребство
            _velocity.y += _jumpHeight /* + _hero.Upgrade.BonusHeightJump*/;
            _isCurrentlyJumping = true;
        }

        #endregion

        public void Disable() => 
            _input.PlayerJumpEvent -= OnJump;

        public void Enable() => 
            _input.PlayerJumpEvent += OnJump;
    }
}