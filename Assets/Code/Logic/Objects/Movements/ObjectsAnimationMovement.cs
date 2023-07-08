using Code.Character.Common;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Objects.Movements
{
    public class ObjectsAnimationMovement : ObjectsMovement
    {
        private Tween _moveTween;
        [SerializeField] private bool _isFlipSprite;

        [ShowIf(nameof(_isFlipSprite)), SerializeField]
        private SpriteVFX _sprite;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
            SetPositions();
        }

        private void OnEnable()
        {
            
            _sprite.SetFlipX(isFlip: false);
            StartMove();
        }

        private void OnDisable()
        {
            StopMove();
        }
        
        protected override void StartMove()
        {
            _moveTween?.Kill();
            transform.DOMove(_startPosition, Speed).OnComplete(Move);
        }

        protected override void Move()
        {
            _moveTween = transform.DOMove(RandomTargetPosition, Speed)
                .SetSpeedBased(true)
                .SetEase(Ease.Flash)
                .SetLoops(-1, LoopType.Yoyo)
                .OnStepComplete(() =>
                {
                    if (_isFlipSprite && _sprite != null)
                    {
                        _sprite.InverseFlipX();
                    }
                });
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
                default:
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