using DG.Tweening;

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
    }
}