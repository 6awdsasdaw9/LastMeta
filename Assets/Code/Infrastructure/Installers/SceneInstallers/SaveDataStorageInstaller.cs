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
        private void Construct(SavedService savedService,GameClock gameClock)
        {
            _savedService = savedService;
            _gameClock = gameClock;
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
            _savedDataStorage = new SavedDataStorage(_gameClock);
            Container.Bind<SavedDataStorage>().FromInstance(_savedDataStorage).AsSingle().NonLazy();
        }

        private void SetSavedDataCollection()
        {
            _savedService.SetSavedDataCollection(_savedDataStorage);
        }
    }
}