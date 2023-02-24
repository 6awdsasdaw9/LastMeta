using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(HeroCollision))]
    public class HeroJump : MonoBehaviour
    {
        [Header("Components")] private MovementLimiter _movementLimiter;
        private HeroCollision _collision;
        private CharacterJuice _juice;
        private Rigidbody _body;
        private Vector2 _velocity;

        [Header("Jumping Stats")] [SerializeField] [Range(2f, 5.5f)]
        private float _jumpHeight = 7.3f;

        [SerializeField] [Range(0.2f, 1.25f)] private float _timeToJumpApex;
        [SerializeField] [Range(0f, 5f)] private float upwardMovementMultiplier = 1f;
        [SerializeField] [Range(1f, 10f)] private float downwardMovementMultiplier = 6.17f;
        [SerializeField] [Range(0, 1)] private int maxAirJumps;


        [Header("Options")] [Tooltip("Should the character drop when you let go of jump?")]
        public bool variablejumpHeight;

        [SerializeField] [Range(1f, 10f)] [Tooltip("Gravity multiplier when you let go of jump")]
        public float jumpCutOff;

        [SerializeField] [Tooltip("Максимальная скорость, с которой персонаж может упасть")]
        private float speedLimit;

        [SerializeField] [Range(0f, 0.3f)] [Tooltip("Сколько должно длиться время койота?")]
        private float coyoteTime = 0.15f;

        [SerializeField] [Range(0f, 0.3f)] [Tooltip("Как далеко от земли кушируется прыжок?")]
        private float jumpBuffer = 0.15f;


        //Calculations
        private float _jumpSpeed;

        private float _defaultGravityScale;
        private float _gravMultiplier;

        [Header("Current State")] [SerializeField]
        private bool _desiredJump;

        private float _jumpBufferCounter;
        private float _coyoteTimeCounter;
        private bool _pressingJump;
        private bool _currentlyJumping;
        private bool _canJumpAgain;
        private int jumpPhase;
        private InputController _input;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _collision = GetComponent<HeroCollision>();
            _juice = GetComponentInChildren<CharacterJuice>();
           // _movementLimiter = new MovementLimiter(true);
            _defaultGravityScale = 1f;
        }
        
        [Inject]
        private void Construct(InputController input,MovementLimiter limiter)
        {
            _movementLimiter = limiter;
            _input = input;
            _input.PlayerJumpEvent += OnJump;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (!_movementLimiter.charactersCanMove) return;
           
            if (context.started)
            {
                _desiredJump = true;
                _pressingJump = true;
            }

            if (context.canceled) _pressingJump = false;
        }

        private void Update()
        {
            SetPhysics();

            CheckJumpBuffer();

            CheckCoyoteTime();
        }

        private void CheckCoyoteTime()
        {
            //Если мы не на земле и в данный момент не прыгаем, значит, мы сошли с края платформы.
            //Итак, запускаем счетчик времени койота...
            if (!_currentlyJumping && !_collision.onGround)
                _coyoteTimeCounter += Time.deltaTime;
            else
                //Сбрасываем, когда касаемся земли или прыгаем
                _coyoteTimeCounter = 0;
        }

        private void CheckJumpBuffer()
        {
            // Буфер прыжков позволяет нам ставить в очередь прыжок, который будет воспроизводиться, когда мы в следующий раз коснемся земли
            if (jumpBuffer > 0)
                //Вместо того, чтобы сразу отключить "desireJump", начинаем считать...
                //Все это время функция DoAJump будет постоянно запускаться
                if (_desiredJump)
                {
                    _jumpBufferCounter += Time.deltaTime;

                    if (_jumpBufferCounter > jumpBuffer)
                    {
                        //If time exceeds the jump buffer, turn off "desireJump"
                        _desiredJump = false;
                        _jumpBufferCounter = 0;
                    }
                }
        }


        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            if (_desiredJump)
            {
                Jump();
                _body.velocity = _velocity;
                //Пропустить вычисления гравитации в этом кадре, поэтому в настоящее время прыжки не отключаются
                //Это гарантирует, что вы не сможете совершить ошибку двойного прыжка во время койота
                return;
            }

            CalculateGravity();
        }


        private void SetPhysics()
        {
            //Определите шкалу гравитации персонажа, используя предоставленную статистику. Умножьте это на gravMultiplier, используемый позже
            // var newGravity = new Vector2(0, -2 * _jumpHeight / (_timeToJumpApex * _timeToJumpApex));
            // _body.gravityScale = newGravity.y / Physics2D.gravity.y * _gravMultiplier;
        }


        /// <summary>
        ///     Сhange the character's gravity based on her Y direction
        /// </summary>
        private void CalculateGravity()
        {
            //если персонаж двигается вверх 
            if (_body.velocity.y > 0.01f)
            {
                if (_collision.onGround)
                {
                    //Don't change it if Kit is stood on something (such as a moving platform)
                    _gravMultiplier = _defaultGravityScale;
                }
                else
                {
                    //If we're using variable jump height...)
                    if (variablejumpHeight)
                    {
                        //Apply upward multiplier if player is rising and holding jump
                        if (_pressingJump && _currentlyJumping)
                            _gravMultiplier = upwardMovementMultiplier;
                        //But apply a special downward multiplier if the player lets go of jump
                        else
                            _gravMultiplier = jumpCutOff;
                    }
                    else
                    {
                        _gravMultiplier = upwardMovementMultiplier;
                    }
                }
            }

            //если персонаж двигается вниз
            else if (_body.velocity.y < -0.01f)
            {
                if (_collision.onGround)
                    //Не меняйте его, если персонаж стоит на чем-то (например, на движущейся платформе)
                    _gravMultiplier = _defaultGravityScale;
                else
                    //Иначе примените нисходящий множитель гравитации
                    _gravMultiplier = downwardMovementMultiplier;
            }

            else
            {
                if (_collision.onGround)
                {
                    _currentlyJumping = false;
                    jumpPhase = 0;
                }

                _gravMultiplier = _defaultGravityScale;
            }


            _body.velocity = new Vector3(_velocity.x, Mathf.Clamp(_velocity.y, -speedLimit, 100));
        }

        private void Jump()
        {
            //Создаем прыжок, если мы на земле, во время койота или доступен двойной прыжок
            if (IsCanJump())
            {
                _desiredJump = false;
                _jumpBufferCounter = 0;
                _coyoteTimeCounter = 0;
                
                /*if (jumpPhase < 1)
                    _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _body.gravityScale * _jumpHeight);
                else
                    _jumpSpeed = _jumpHeight  * _body.gravityScale;*/
                _jumpSpeed = _jumpHeight;
                
                jumpPhase += 1;

                //если у нас включен двойной прыжок, разрешите нам прыгнуть еще раз (но только один раз)
                _canJumpAgain = maxAirJumps == 1 && _canJumpAgain == false;


                //Определяем силу прыжка, основываясь на нашей гравитации и статистике

                //Если движется вверх или вниз во время прыжка (например, при двойном прыжке), измените jumpSpeed;
                // Это обеспечит одинаковую силу прыжка, независимо от вашей скорости.
                if (_velocity.y > 0f)
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                else if (_velocity.y < 0f) _jumpSpeed += Mathf.Abs(_body.velocity.y);


                _velocity.y += _jumpSpeed;
                _currentlyJumping = true;
            }
        }

        private bool IsCanJump()
        {
            return _collision.onGround || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < coyoteTime) ||
                   _canJumpAgain;
        }
    }
}