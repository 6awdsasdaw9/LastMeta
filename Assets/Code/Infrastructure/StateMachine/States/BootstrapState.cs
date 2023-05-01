using Code.Data.ProgressData;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly PersistentSavedDataService _persistentSavedDataService;

        public BootstrapState(GameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _sceneLoader = container.Resolve<SceneLoader>();
            _persistentSavedDataService = container.Resolve<PersistentSavedDataService>();
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.Scenes.Initial.ToString(), onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _persistentSavedDataService.LoadData();

            var level = _persistentSavedDataService.savedData.Scene;

            _stateMachine.Enter<LoadLevelState, string>(level);
        }
    }
}