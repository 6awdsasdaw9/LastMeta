using Code.Services;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly SavedService _savedService;

        public BootstrapState(GameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _sceneLoader = container.Resolve<SceneLoader>();
            _savedService = container.Resolve<SavedService>();
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
            _savedService.LoadData();

            var level = _savedService.SavedData.CurrentScene;

            _stateMachine.Enter<LoadLevelState, string>(level);
        }
    }
}