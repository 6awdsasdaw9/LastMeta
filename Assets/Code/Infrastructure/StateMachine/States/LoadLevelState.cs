using System.Linq;
using Code.Audio;
using Code.Audio.AudioPath;
using Code.PresentationModel;
using Code.Services;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private readonly SceneAudioController _sceneAudioController;
        private readonly SceneAudioPath _sceneAudioPath;
        private readonly SavedService _savedService;

        public LoadLevelState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _stateMachine = gameStateMachine;

            _sceneLoader = container.Resolve<SceneLoader>();
            _loadingCurtain = container.Resolve<LoadingCurtain>();

            _sceneAudioController = container.Resolve<SceneAudioController>();
            _sceneAudioPath = container.Resolve<SceneAudioPath>();
            _savedService = container.Resolve<SavedService>();
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
            SetSceneMusic(sceneName);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
            _savedService.LoadProgress();
        }


        private void SetSceneMusic(string sceneName)
        {
            SceneAudioData audioData =
                _sceneAudioPath.SceneAudioData.FirstOrDefault(d => d.Scene.ToString() == sceneName);
          
            if (audioData == null)
                return;

            if (audioData.Ambience.IsNull)
            {
                _sceneAudioController.StopAmbience();
            }
            else if (!_sceneAudioController.IsCurrentAmbienceEvent(audioData.Ambience))
            {
                _sceneAudioController.StopAmbience();
                _sceneAudioController.SetAmbienceEvent(audioData.Ambience);
                _sceneAudioController.PlayAmbience();
            }
           
            if (!_sceneAudioController.IsCurrentMusicEvent(audioData.Music))
            {
                _sceneAudioController.StopMusic();
                _sceneAudioController.SetMusicEvent(audioData.Music);
                _sceneAudioController.PlayMusic();
            }
        }
    }
}