using System;
using Code.Data.Configs;
using Code.Services;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Movements
{
    public class ObjectsPhysicsMovement : ObjectsMovement
    {
        [Space, Title("Components")] 
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private PlatformFriction _friction;
        private MovementLimiter _movementLimiter;

        private Vector3 _startPosition, _finishPosition, _targetPosition;
        private bool _isMove = true;

        [Inject]
        private void Construct(MovementLimiter limiter,AssetsConfig assetsConfig)
        {
            _movementLimiter = limiter;
            SetPhysicsParams(assetsConfig.FrictionMaterial, assetsConfig.NoFrictionMaterial);
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
            SetPositions();
        }

        private void Update()
        {
            if (_isMove)
            {
                MovementCycle().Forget();
            }
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        #region Movement

        protected override void StartMove()
        {
            _isMove = true;
            _rigidbody.isKinematic = false;
        }

        protected override void Move() =>
            _rigidbody.velocity = GetForward() * Speed;

        protected override void StopMove()
        {
            _isMove = false;
            _rigidbody.isKinematic = true;
        }

        private async UniTaskVoid MovementCycle()
        {
            Move();

            await UniTask.WaitUntil(CheckDistance, cancellationToken: this.GetCancellationTokenOnDestroy());

            SwitchForward();
        }

        #endregion

        #region Forward

        private bool CheckDistance() =>
            Vector3.Distance(transform.position, DefaultTargetPosition) < 0.1f;

        private Vector3 GetForward() =>
            (DefaultTargetPosition - transform.position).normalized;

        private void SwitchForward()
        {
            _rigidbody.velocity = Vector3.zero;
            DefaultTargetPosition = DefaultTargetPosition == _startPosition ? _finishPosition : _startPosition;
        }

        #endregion

        #region Settings

        protected override void SetPositions()
        {
            _startPosition = transform.position;

            _finishPosition = CurrentAxis switch
            {
                Axis.X => transform.position + Vector3.right * Distance,
                Axis.Y => transform.position + Vector3.up * Distance,
                _ => transform.position
            };

            DefaultTargetPosition = _finishPosition;
        }

        private void SetPhysicsParams(PhysicMaterial frictionMaterial, PhysicMaterial noFrictionMaterial)
        {
            switch (CurrentAxis)
            {
                case Axis.X:
                    _collider.material = frictionMaterial;
                    _rigidbody.useGravity = true;
                    _rigidbody.constraints = RigidbodyConstraints.FreezePositionY |
                                             RigidbodyConstraints.FreezeRotationZ |
                                             RigidbodyConstraints.FreezeRotation;
                    break;
                case Axis.Y:
                    _friction.enabled = false;
                    _rigidbody.useGravity = false;
                    _collider.material = noFrictionMaterial;
                    _rigidbody.constraints = RigidbodyConstraints.FreezePositionX |
                                             RigidbodyConstraints.FreezeRotationZ |
                                             RigidbodyConstraints.FreezeRotation;
                    break;
            }
        }

        #endregion

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

        private void OnDrawGizmos()
        {
            switch (CurrentAxis)
            {
                case Axis.X:
                    Gizmos.color = new Color32(0, 100, 255, 255);
                    var forward = Distance < 0 ? Distance - _collider.size.x / 2 : Distance + _collider.size.x / 2;
                    Gizmos.DrawRay(transform.position, Vector3.right * forward);
                    break;
                case Axis.Y:
                    Gizmos.color = new Color32(0, 100, 255, Convert.ToByte(100*Speed));
                    Gizmos.DrawCube(transform.position + Vector3.up * Distance / 2,
                        new Vector3(_collider.size.x, Distance));
                    break;
            }
        }
    }
}