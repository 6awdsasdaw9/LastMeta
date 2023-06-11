using Code.Services.SaveServices;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class SaveDataStorageInstaller: MonoInstaller, IInitializable
    {
        private SavedDataStorage _savedDataStorage;
        private SavedService _savedService;
        
        [Inject]
        private void Construct(SavedService savedService)
        {
            _savedService = savedService;
        }
        public override void InstallBindings()
        {
            BindInterfaces();
            BindSaveData();
        }

        public void Initialize()
        {
            SetSavedDataCollection();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesTo<SaveDataStorageInstaller>().FromInstance(this);
        }

        private void BindSaveData()
        {
            _savedDataStorage = new SavedDataStorage();
            Container.Bind<SavedDataStorage>().FromInstance(_savedDataStorage).AsSingle().NonLazy();
        }

        private void SetSavedDataCollection()
        {
            _savedService.SetSavedDataCollection(_savedDataStorage);
        }
    }
}