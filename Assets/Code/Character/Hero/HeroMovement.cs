using Code.Data.Configs;
using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Debugers;
using Code.Services;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;


namespace Code.Character.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroMovement : MonoBehaviour, ISavedData, IHeroMovement
    {
        [SerializeField] private Rigidbody _body;

        private IHero _hero;
        private HeroConfig _heroConfig;
        private InputService _input;

        #region Values

        public bool IsCrouch { get; private set; } 
        public float DirectionX => _directionX;
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
        private void Construct(InputService input, GameConfig gameConfig,SavedDataCollection dataCollection)
        {
            _hero = GetComponent<IHero>();
            _input = input;
            _heroConfig = gameConfig.heroConfig;
            dataCollection.Add(this);
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
                _input.PlayerCrochEvent += OnPressCrouch;
                _input.PlayerMovementEvent += OnPressMovement;

            }
            else
            {
                _input.PlayerCrochEvent -= OnPressCrouch;
                _input.PlayerMovementEvent -= OnPressMovement;
            }       
        }

        public async UniTaskVoid BlockMovement(bool unblockCondition)
        {
            Log.ColorLog($"HERO CAN MOVE {_heroCanMove}",ColorType.Red);
            await UniTask.WaitUntil(() => unblockCondition, cancellationToken: this.GetCancellationTokenOnDestroy());
            _heroCanMove = true;
            Log.ColorLog($"HERO CAN MOVE {_heroCanMove}",ColorType.Red);
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

        //TODO переделать. Этот параметр должен находится в отдельном классе, из которого берется значение 
   
        private void SetDesiredVelocity()
        {
            //TODO Непотребство
            var speedMultiplayer = _heroConfig.maxSpeed;
            if (_hero.Upgrade != null)
                speedMultiplayer += _hero.Upgrade.BonusSpeed;
            _desiredVelocity = new Vector2(_directionX, 0f) * speedMultiplayer;
    
        }

        #endregion

        #region Movement


        private void MoveWithAcceleration()
        {
            _acceleration = _hero.Collision.OnGround ? _heroConfig.maxAcceleration : _heroConfig.maxAirAcceleration;
            _deceleration = _hero.Collision.OnGround ? _heroConfig.maxDeceleration : _heroConfig.maxAirDeceleration;
            _turnSpeed = _hero.Collision.OnGround ? _heroConfig.maxTurnSpeed : _heroConfig.maxAirTurnSpeed;

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
                          (IsCrouch ? _heroConfig.crouchSpeed : 1);

            if (_heroCanMove)
            {
                _body.velocity = _velocity + _supportVelocity;
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

        #region Save and Load

        public void LoadData(SavedData savedData)
        {
            if (savedData.heroPositionData.scene != CurrentLevel() ||
                savedData.heroPositionData.position.AsUnityVector() == Vector3.zero)
                return;

            Vector3Data savedPosition = savedData.heroPositionData.position;
            transform.position = savedPosition.AsUnityVector() + Vector3.up;
        }

        public void SaveData(SavedData savedData) =>
            savedData.heroPositionData =
                new PositionData(CurrentLevel(), transform.position.AsVectorData());

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

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