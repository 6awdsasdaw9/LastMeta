using DG.Tweening;
using UnityEngine;

namespace Code.Logic.Common
{
    public class SpriteVFX : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void RedColorize()
        {
            _spriteRenderer.DOColor(Constants.RedColor, 0.5f)
                .OnComplete(() => _spriteRenderer.DOColor(Color.white, 0.5f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void SetFlipX(bool isFlip)
        {
            _spriteRenderer.flipX = isFlip;
        }

        public void InverseFlipX()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}