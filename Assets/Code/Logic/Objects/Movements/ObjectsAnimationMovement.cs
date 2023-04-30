using DG.Tweening;
using UnityEngine;

namespace Code.Logic.Objects
{
    public class ObjectsAnimationMovement : ObjectsMovement
    {
        private Tween _moveTween;

        void Start()
        {
            SetPositions();
            StartMove();
        }

        private void OnDisable()
        {
            StopMove();
        }
        
        protected override void StartMove()
        {
            Move();
        }

        protected override void Move()
        {
            _moveTween = transform.DOMove(TargetPosition, Speed)
                .SetSpeedBased(true)
                .SetEase(Ease.Flash)
                .SetLoops(-1, LoopType.Yoyo);
        }

        protected override void StopMove()
        {
            _moveTween.Kill();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            switch (CurrentAxis)
            {
                case Axis.X:
                    Gizmos.DrawRay(transform.position, Vector3.right * Distance);
                    break;
                case Axis.Y:
                    Gizmos.DrawRay(transform.position, Vector3.up * Distance);
                    break;
            }
        }
    }
}