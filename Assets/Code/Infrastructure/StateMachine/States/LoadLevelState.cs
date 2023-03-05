using Code.Data.DataPersistence;
using Code.Debugers;
using Code.Logic;
using Code.Services;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            ProgressService progressService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void onLoaded()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<GameLoopState>();
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.FindAllIDataPersistence();
            _progressService.LoadProgress();
        }
    }
}