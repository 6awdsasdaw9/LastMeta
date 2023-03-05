using Code.Data.DataPersistence;
using Code.Services;

namespace Code.Infrastructure.StateMachine.States
{
    //Start work in GameBootstrapper.It is first gamestate
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ProgressService _progressService;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader,ProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _progressService = progressService;
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.initialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(_progressService.gameProgressData.worldData.positionOnLevel
                .level);
           // _stateMachine.Enter<LoadProgressState>();
        }
    }
}