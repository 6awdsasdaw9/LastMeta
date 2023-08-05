using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Logic.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroVFX : MonoBehaviour, IHeroVFX
    {
        public SpriteVFX SpriteVFX => _spriteVFX;
        [SerializeField] private SpriteVFX _spriteVFX;
        private GameObject _deathFx;

        [Inject]
        private void Construct(AssetsConfig assetsConfig)
        {
            _deathFx = assetsConfig.VFX_PlayerDeath;
        }

        public void PlayDeathVFX()
        {
            PlayVFXWithDelay(_deathFx,1.5f).Forget();
        }

        public void PlayDeathVFX(Vector3 vfxPosition)
        {
            var vfx = Instantiate(_deathFx, transform.position, Quaternion.identity);
            vfx.transform.position -= Vector3.right * transform.localScale.x * 0.5f;
        }

        private async UniTaskVoid PlayVFXWithDelay(GameObject VFX, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: gameObject.GetCancellationTokenOnDestroy());
            Instantiate(VFX, transform.position, Quaternion.identity);
        }
    }
    
}