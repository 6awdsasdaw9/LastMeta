using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroVFX : MonoBehaviour, IHeroVFX
    {
        //TODO вытащить в отдельных класс VFX игрока
        private GameObject _deathFx;
        private IHero _hero;

        [Inject]
        private void Construct(PrefabsData prefabsData)
        {
            _hero = GetComponent<IHero>();
            _deathFx = prefabsData.VFX_PlayerDeath;
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