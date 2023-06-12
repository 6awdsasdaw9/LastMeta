using Code.Data.GameData;
using Code.Services.SaveServices;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class DataServicesInstaller : MonoInstaller
    {
        [SerializeField] private SavedService _progressService;
        public override void InstallBindings()
        {
            BindDataService();
            BindGameSceneData();
        }

        private void BindDataService()
        {
            Container.Bind<SavedService>().FromInstance(_progressService).AsSingle().NonLazy();
        }

        private void BindGameSceneData()
        {
            Container.Bind<GameSceneData>().AsSingle().NonLazy();
        }
    }
}