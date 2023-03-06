using Code.Data.DataPersistence;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Logic;
using Code.Logic.DayOfTime;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>, IInitializable, ICoroutineRunner
    {
        public LoadingCurtain curtain;
        public ProgressService progressService;
        public TimeOfDayController time;

        
        public override void InstallBindings()
        {
            BindInterfaces();
            BindSaveData();
            BindDataService();
            BindStateMachine();
            BindTimeOfDayController();
        }

        public void Initialize() =>
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();

        private void BindInterfaces() =>
            Container.BindInterfacesTo<GameInstaller>()
                .FromInstance(this);

        private void BindDataService() =>
            Container.Bind<ProgressService>().FromInstance(progressService).AsSingle().NonLazy();
        
        private void BindSaveData() =>
        Container.Bind<SaveData>().AsSingle().NonLazy();

        private void BindStateMachine() =>
            Container.Bind<GameStateMachine>()
                .AsSingle().WithArguments(new SceneLoader(this), curtain, progressService).NonLazy();

        private void BindTimeOfDayController()
        {
            Container.BindInterfacesAndSelfTo<TimeOfDayController>().FromInstance(time).AsSingle().NonLazy();
            Container.Resolve<SaveData>().Add(time);
        }
    }
}