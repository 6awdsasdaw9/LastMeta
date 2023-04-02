using Code.Data.ProgressData;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SaveDataInstaller:MonoInstaller, IInitializable
    {

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
            PersistentSavedDataService dataService = Container.Resolve<PersistentSavedDataService>();
            dataService.SetSavedDataCollection(Container.Resolve<SavedDataCollection>());
            dataService.LoadProgress();
        }
    }
}