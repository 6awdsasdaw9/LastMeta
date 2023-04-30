using Code.Data.ProgressData;
using Code.Debugers;
using Code.Services;
using Code.UI;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly PersistentSavedDataService _dataService;
        
        public LoadLevelState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = container.Resolve<SceneLoader>();
            _loadingCurtain = container.Resolve<LoadingCurtain>();
            _dataService = container.Resolve<PersistentSavedDataService>();
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            Log.ColorLog("LOAD LEVEL STATE",ColorType.Red);
            _dataService.LoadProgress();
            _stateMachine.Enter<GameLoopState>();
        }
        
    }
}