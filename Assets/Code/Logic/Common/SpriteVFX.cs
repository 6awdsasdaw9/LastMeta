using Code.Data.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Logic.Common
{
    public class SpriteVFX : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Material _glitchMaterial;
        private Material _defaultMaterial;

        [Inject]
        private void Construct(AssetsConfig assetsConfig)
        {
            _defaultMaterial = assetsConfig.CharacterMaterial;
            _glitchMaterial = assetsConfig.GlitchMaterial;
        }

        public void RedColorize()
        {
            _spriteRenderer.DOColor(Constants.RedColor, 0.5f)
                .OnComplete(() => _spriteRenderer.DOColor(Color.white, 0.5f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
        
        public void SetGlitchMaterial() => _spriteRenderer.material = _glitchMaterial;
        public void SetDefaultMaterial() => _spriteRenderer.material = _defaultMaterial;

        public void SetFlipX(bool isFlip) => _spriteRenderer.flipX = isFlip;
        public void InverseFlipX() => _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}