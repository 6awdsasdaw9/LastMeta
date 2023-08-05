using Code.Services.GameTime;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class SaveDataStorageInstaller: MonoInstaller, IInitializable
    {
        private SavedDataStorage _savedDataStorage;
        private SavedService _savedService;
        private GameClock _gameClock;

        [Inject]
        private void Construct(SavedService savedService)
        {
            _savedService = savedService;
        }
        public override void InstallBindings()
        {
            BindInterfaces();
            BindSaveDataStorage();
        }

        public void Initialize()
        {
            SetSavedDataCollection();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesTo<SaveDataStorageInstaller>().FromInstance(this);
        }

        private void BindSaveDataStorage()
        {
            _savedDataStorage = new SavedDataStorage(Container);
            Container.Bind<SavedDataStorage>().FromInstance(_savedDataStorage).AsSingle().NonLazy();
        }

        private void SetSavedDataCollection()
        {
            _savedService.SetSavedDataStorage(_savedDataStorage);
        }
    }
}