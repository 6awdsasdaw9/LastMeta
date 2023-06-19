using Code.Infrastructure.Factories;
using Code.Logic.Missile;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class FactoriesInstaller: MonoInstaller
    {
        [SerializeField] private HeroMissile heroMissilePrefabs;
        public override void InstallBindings()
        {
            BindMissilePool();
            BindMissileFactory();
            /*BindMissilePool();
            BindMissileFactory();*/
        }

        private void BindMissilePool()
        {
            Container.BindMemoryPool<HeroMissile, HeroMissile.Pool>()
                .WithInitialSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(heroMissilePrefabs)
                .UnderTransformGroup("Missile");
        }
        
        private void BindMissileFactory()
        {
            Container.Bind<MissilesFactory>().AsSingle().NonLazy();
        }
    }
}