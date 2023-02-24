using Code.Character;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefabs;
        [SerializeField] private Transform initialPoint;

        public override void InstallBindings()
        {
            BindInput();
            BindLimiter();
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