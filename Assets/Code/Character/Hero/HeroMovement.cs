using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(HeroCollision))]
    public class HeroMovement : MonoBehaviour
    {
        [Header("Components")] private MovementLimiter _movementLimiter;
        private Rigidbody _body;
        private HeroCollision _collision;

        [Header("Movement Stats")] [SerializeField, Range(0f, 20f)]
        public float maxSpeed = 10f;

        [SerializeField, Range(0f, 1f)] private float _crouchSpeed = 0.5f;

        [SerializeField, Range(0f, 100f)] [Tooltip("How fast to reach max speed")]
        public float _maxAcceleration = 52f;

        [SerializeField, Range(0f, 100f)] [Tooltip("How fast to stop after letting go")]
        public float _maxDecceleration = 52f;

        [SerializeField, Range(0f, 100f)] [Tooltip("How fast to stop when changing direction")]
        public float _maxTurnSpeed = 80f;

        [SerializeField, Range(0f, 100f)] [Tooltip("How fast to reach max speed when in mid-air")]
        public float _maxAirAcceleration;

        [SerializeField, Range(0f, 100f)] [Tooltip("How fast to stop in mid-air when no direction is used")]
        public float _maxAirDeceleration;

        [SerializeField, Range(0f, 100f)] [Tooltip("How fast to stop when changing direction when in mid-air")]
        public float _maxAirTurnSpeed = 80f;


        [Header("Calculations")] public float directionX;
        private Vector2 _desiredVelocity;
        private Vector2 _velocity;
        private float _maxSpeedChange;
        private float _acceleration;
        private float _deceleration;
        private float _turnSpeed;


        [Header("Current State")] private bool pressingMove;
        public bool isCrouch => _collision.onGround && pressingCrouch;
        private bool pressingCrouch;

        private void Awake()
        {
            _movementLimiter = new MovementLimiter(true);
            _movementLimiter.OnDisableMovementMode += StopMovement;
            _body = GetComponent<Rigidbody>();
            _collision = GetComponent<HeroCollision>();
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
            if (_movementLimiter.characterCanMove)
                directionX = context.ReadValue<float>();

            if (directionX != 0)
            {
                pressingMove = true;
            }
            else
            {
                pressingMove = false;
            }
        }


        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started)
                pressingCrouch = true;

            else
                pressingCrouch = false;
        }

        public Vector2 GetVelocity() =>
            _velocity;

        private void Rotation()
        {
            if (directionX != 0)
            {
                transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
            }
        }

        private void SetDesiredVelocity() =>
            _desiredVelocity = new Vector2(directionX, 0f) * maxSpeed;

        private void StopMovement()
        {
            directionX = 0;
            pressingCrouch = false;
            pressingMove = false;
        }


        private void MoveWithAcceleration()
        {
            _acceleration = _collision.onGround ? _maxAcceleration : _maxAirAcceleration;
            _deceleration = _collision.onGround ? _maxDecceleration : _maxAirDeceleration;
            _turnSpeed = _collision.onGround ? _maxTurnSpeed : _maxAirTurnSpeed;

            if (pressingMove)
            {
                if (Mathf.Sign(directionX) != Mathf.Sign(_velocity.x))
                {
                    _maxSpeedChange = _turnSpeed * Time.deltaTime;
                }
                else
                {
                    _maxSpeedChange = _acceleration * Time.deltaTime;
                }
            }
            else
            {
                _maxSpeedChange = _deceleration * Time.deltaTime;
            }

            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange) *
                          (isCrouch ? _crouchSpeed : 1);

            _body.velocity = _velocity;
        }
    }
}