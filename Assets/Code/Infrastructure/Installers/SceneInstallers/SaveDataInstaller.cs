using Code.Data.ProgressData;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SaveDataInstaller: MonoInstaller, IInitializable
    {
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

        public void Initialize() => 
            LoadGameProgress();

        private void BindInterfaces() =>
            Container.BindInterfacesTo<SaveDataInstaller>()
                .FromInstance(this);

        private void BindSaveData() => 
            Container.Bind<SavedDataCollection>().AsSingle().NonLazy();
        
        private void LoadGameProgress()
        {
            _savedService.SetSavedDataCollection(Container.Resolve<SavedDataCollection>());
        }
    }
}