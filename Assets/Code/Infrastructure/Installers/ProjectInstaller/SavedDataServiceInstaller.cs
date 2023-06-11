using Code.Services.SaveServices;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class SavedDataServiceInstaller : MonoInstaller
    {
        [SerializeField] private SavedService _progressService;
        public override void InstallBindings()
        {
            BindDataService();
        }

        private void BindDataService()
        {
            Container.Bind<SavedService>().FromInstance(_progressService).AsSingle().NonLazy();
        }
    }
}