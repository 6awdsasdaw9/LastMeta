using Code.Audio;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Services;
using Code.Services.SaveServices;
using DG.Tweening;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly SavedService _savedService;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = container.Resolve<SceneLoader>();
            _savedService = container.Resolve<SavedService>();

            var audio = container.Resolve<SceneAudioController>();
            audio.InitSnapshot(container.Resolve<ScenesConfig>().PauseSnapshot.Path);
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.Scenes.Initial.ToString(), onLoaded: EnterLoadLevel);
            //DOTween.SetTweensCapacity(2000, 100);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _savedService.LoadData();
      
            _gameStateMachine.Enter<LoadLevelState, string>(_savedService.SavedData.CurrentScene);
        }
    }
}