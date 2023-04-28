using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects
{
    public class ObjectsPhysicsMovement : ObjectsMovement
    {
        [SerializeField] private Rigidbody _rigidbody;
        private Vector3 _startPosition, _finishPosition, _targetPosition;

        private bool _isMove = true;
        private MovementLimiter _movementLimiter;

        [Inject]
        private void Construct(MovementLimiter limiter)
        {
            _movementLimiter = limiter;
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void Start()
        {
            SetPositions();
        }

        private void Update()
        {
            if (_isMove)
            {
                MovementCycle();
            }
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        protected override void StartMove() =>
            _isMove = true;

        protected override void Move() =>
            _rigidbody.velocity = GetForward() * Speed;

        protected override void StopMove() =>
            _isMove = false;

        private async UniTaskVoid MovementCycle()
        {
            Move();

            await UniTask.WaitUntil(CheckDistance, cancellationToken: this.GetCancellationTokenOnDestroy());

            SwitchForward();
        }

        private bool CheckDistance() =>
            Vector3.Distance(transform.position, TargetPosition) < 0.1f;

        private Vector3 GetForward() =>
            (TargetPosition - transform.position).normalized;

        private void SwitchForward()
        {
            _rigidbody.velocity = Vector3.zero;
            TargetPosition = TargetPosition == _startPosition ? _finishPosition : _startPosition;
        }

        protected override void SetPositions()
        {
            _startPosition = transform.position;

            _finishPosition = CurrentAxis switch
            {
                Axis.X => transform.position + Vector3.right * Distance,
                Axis.Y => transform.position + Vector3.up * Distance,
                _ => transform.position
            };

            TargetPosition = _finishPosition;
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _movementLimiter.OnDisableMovementMode += StopMove;
                _movementLimiter.OnEnableMovementMode += StartMove;
            }
            else
            {
                _movementLimiter.OnDisableMovementMode -= StopMove;
                _movementLimiter.OnEnableMovementMode -= StartMove; 
            }
        }
    }
}