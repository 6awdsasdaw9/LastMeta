using Code.Data.DataPersistence;
using Zenject;

namespace Code.Infrastructure.Factory.States
{
    public class LoadProgressState :IState
    {
        [Inject] private ProgressService _progressService;
        private readonly GameStateMachine _gameStateMachine;
        
        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState,string>(_progressService.gameProgressData.worldData.positionOnLevel.level);
        }

        public void Exit()
        {

        }

        private void LoadProgressOrInitNew()
        {
            _progressService.LoadProgress();
        }
    }
}