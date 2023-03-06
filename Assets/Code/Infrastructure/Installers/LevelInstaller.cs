using System.Collections.Generic;
using Code.Character;
using Code.Character.Hero;
using Code.Data.DataPersistence;
using Code.Logic.DayOfTime;
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
            Container.Resolve<SaveData>().Add(hero);
          
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
