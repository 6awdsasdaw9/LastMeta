using Code.Character;
using Code.Character.Hero;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private Transform initialPoint;

        public override void InstallBindings()
        {
            BindInput();
            BindLimiter();
            BindHero();
        }

        private void BindHero()
        {
            HeroMovement hero = Container.InstantiatePrefabForComponent<HeroMovement>(heroPrefab, initialPoint.position,
                Quaternion.identity, null);
            Container.Bind<HeroMovement>().FromInstance(hero).AsSingle().NonLazy();
        }

        private void BindInput()
        {
            Container.Bind<InputController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLimiter()
        {
            Container.Bind<MovementLimiter>()
                .AsSingle()
                .NonLazy();
        }
    }
}