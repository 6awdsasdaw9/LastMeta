using System;
using System.Linq;
using Code.Audio;
using Code.Audio.AudioPath;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Code.Services.SaveServices;
using Code.UI;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private readonly SceneAudioController _sceneAudioController;
        private readonly SceneAudioPath _sceneAudioPath;
        private readonly SavedService _savedService;
        private readonly GameSceneData _gameSceneData;
        private readonly EventsFacade _eventsFacade;

        public LoadLevelState(GameStateMachine gameGameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameGameStateMachine;

            _sceneLoader = container.Resolve<SceneLoader>();
            _loadingCurtain = container.Resolve<LoadingCurtain>();

            _sceneAudioController = container.Resolve<SceneAudioController>();
            _savedService = container.Resolve<SavedService>();
            _gameSceneData = container.Resolve<GameSceneData>();
            _eventsFacade = container.Resolve<EventsFacade>();
        }

        public void Enter(string sceneName)
        {
            Logg.ColorLog("Scene name");
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
            _eventsFacade.SceneEvents.LoadSceneEvent();
        }

        private void OnLoaded()
        {
            _savedService.LoadProgress();
            _gameSceneData.Init(_savedService.SavedData);
            
            var sceneParam = _gameSceneData.CurrentSceneParams;
            if (sceneParam == null)
                return;

            TrySetSceneMusic(sceneParam);
            
            if (Enum.TryParse(_savedService.SavedData.TimeData.TimeOfDay, out TimeOfDay timeOfDay))
            {
                switch (timeOfDay)
                {
                    default:
                    case TimeOfDay.Morning:
                        _gameStateMachine.Enter<MorningState>();
                        break;
                    case TimeOfDay.Evening:
                        _gameStateMachine.Enter<EveningState>();
                        break;
                    case TimeOfDay.Night:
                        _gameStateMachine.Enter<NightState>();
                        break;
                }
            }
            else
            {
                Logg.ColorLog("LoadLevelState: can't try parse current TimeOfDay from GameLoopData", LogStyle.Warning);
            }
        }


        private void TrySetSceneMusic(SceneParams sceneParam)
        {
            Logg.ColorLog("GSM: Try Set music ");
            _sceneAudioController.ChangeSceneAudio(sceneParam.Music, sceneParam.Ambience);
        }
    }
}