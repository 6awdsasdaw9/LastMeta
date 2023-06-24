using Code.Infrastructure.Factories;
using Code.Logic.Missile;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class MissilesInstaller: MonoInstaller
    {
        [SerializeField] private HeroMissile _heroMissilePrefabs;
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
                .FromComponentInNewPrefab(_heroMissilePrefabs)
                .UnderTransformGroup("Missiles");
        }
        
        private void BindMissileFactory()
        {
            Container.Bind<MissilesFactory>().AsSingle().NonLazy();
        }
    }
}