using Code.Data.ProgressData;
using Code.Infrastructure.Factory;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Logic;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>, IInitializable, ICoroutineRunner
    {
        public LoadingCurtain curtain;
        public PersistentSavedDataService persistentSavedDataService;
        
        public override void InstallBindings()
        {
            BindInterfaces();
            
            BindDataService();
            
            BindFactory();
           BindStateMachine();
        }

        public void Initialize() => 
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();

        private void BindInterfaces() =>
            Container.BindInterfacesTo<ProjectInstaller>()
                .FromInstance(this);

        private void BindDataService() => 
            Container.Bind<PersistentSavedDataService>().FromInstance(persistentSavedDataService).AsSingle().NonLazy();
        
        private void BindFactory() => 
            Container.Bind<GameFactory>().AsSingle().NonLazy();

        private void BindStateMachine() =>
            Container.BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle().WithArguments(new SceneLoader(this), curtain, persistentSavedDataService).NonLazy();

    }
}