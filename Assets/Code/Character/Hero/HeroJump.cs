using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Debugers;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using HeroConfig = Code.Data.Configs.HeroConfigs.HeroConfig;

namespace Code.Character.Hero
{
    public class HeroJump : MonoBehaviour, IHeroJump
    {
        [SerializeField] private Rigidbody _body;

        private IHero _hero;
        private InputService _input;
        private HeroParams _params;
        
        #region Values

        public float Height => _hero.Stats.JumpHeight;
        private int _maxAirJumps => _hero.Stats.AirJump;

        //Calculations
        private Vector2 _velocity;
        private float _jumpSpeed;
        private float _jumpBufferCounter;
        private float _coyoteTimeCounter;

        //States
        private bool _isCanJumpAgain;
        private bool _isDesiredJump;
        public bool IsCurrentlyJumping { get; private set; }

        #endregion

        #region Run Time

        [Inject]
        private void Construct(InputService input, HeroConfig heroConfig)
        {
            _hero = GetComponent<IHero>();
            _input = input;
            _params = heroConfig.HeroParams;
        }

        private void OnEnable() =>
            EnableComponent();

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
            DisableComponent();

        #endregion

        #region Input

        private void OnJump(InputAction.CallbackContext context)
        {
            if (_hero.Stats.IsCrouch || !_hero.Stats.OnGround)
                return;

            if (context.started) 
                _isDesiredJump = true;
        }

        #endregion

        #region Conditions

        private bool IsCanJump() =>
            _hero.Collision.OnGround
            || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < _params.coyoteTime)
            || _isCanJumpAgain;

        private void CheckCoyoteTime()
        {
            if (!IsCurrentlyJumping && !_hero.Collision.OnGround)
                _coyoteTimeCounter += Time.deltaTime;
            else
                _coyoteTimeCounter = 0;
        }

        private void CheckJumpBuffer()
        {
            if (!(_params.jumpBuffer > 0) || !_isDesiredJump)
                return;

            _jumpBufferCounter += Time.deltaTime;

            if (_jumpBufferCounter > _params.jumpBuffer)
            {
                _isDesiredJump = false;
                _jumpBufferCounter = 0;
            }
        }

        #endregion

        #region Jumping

        private void CalculateGravity()
        {
            if (_hero.Stats.IsDash)
            {
                _body.velocity = new Vector3(_velocity.x, 0, 0);
                return;
            }

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
                        IsCurrentlyJumping = false;
                    }

                    break;
                }
            }

            _body.velocity = new Vector3(_velocity.x, Mathf.Clamp(_velocity.y, -_params.speedLimit, 100), 0);
        }

        private void Jump()
        {
            if (!IsCanJump())
                return;

            if (!IsCurrentlyJumping)
                _hero.Audio.PlayJumpSound();

            _isDesiredJump = false;
            _jumpBufferCounter = 0;
            _coyoteTimeCounter = 0;

            //TODO непотребство 
            var airJump = _maxAirJumps;
            if (_hero.Upgrade != null)
            {
                airJump += _hero.Upgrade.BonusAttack;
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

            _velocity.y += Height;
            IsCurrentlyJumping = true;
        }

        #endregion

        public void DisableComponent() => _input.OnPressJump -= OnJump;

        public void EnableComponent() => _input.OnPressJump += OnJump;
    }
}