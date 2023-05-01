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
        
        public LoadLevelState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = container.Resolve<SceneLoader>();
            _loadingCurtain = container.Resolve<LoadingCurtain>();
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
            _stateMachine.Enter<GameLoopState>();
        }
        
    }
}