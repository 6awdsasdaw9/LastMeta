using Code.Data.ProgressData;
using Code.Infrastructure.Factory;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Logic;
using Code.Services;
using Code.UI;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>, IInitializable, ICoroutineRunner
    {
        //TODO разделить на несколько инсталлеров
        public LoadingCurtain Curtain;
        public PersistentSavedDataService ProgressService;

        public override void InstallBindings()
        {
            BindInterfaces();

            BindSceneLoader();
            BindLoadingCurtain();

            BindDataService();

            BindFactory();
            BindStateMachine();
        }

        public void Initialize() =>
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();

        private void BindInterfaces() =>
            Container.BindInterfacesTo<ProjectInstaller>()
                .FromInstance(this);

        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>()
                .AsSingle()
                .WithArguments(this)
                .NonLazy();

        private void BindLoadingCurtain() =>
            Container.Bind<LoadingCurtain>().FromInstance(Curtain).AsSingle().NonLazy();


        private void BindDataService() =>
            Container.Bind<PersistentSavedDataService>().FromInstance(ProgressService).AsSingle().NonLazy();

        private void BindFactory() =>
            Container.Bind<GameFactory>().AsSingle().NonLazy();

        private void BindStateMachine() =>
            Container.BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle().WithArguments(Container).NonLazy();
    }
}