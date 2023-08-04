using System.Threading;
using Code.Audio;
using Code.Audio.AudioSystem;
using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Services.GameTime;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class MorningState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EventsFacade _eventsFacade;
        private readonly GameSceneData _gameSceneData;
        private readonly GameClock _gameClock;

        private CancellationTokenSource _cts;
        private readonly SceneAudioController _sceneAudioController;

        public MorningState(GameStateMachine gameGameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameGameStateMachine;
            _eventsFacade = diContainer.Resolve<EventsFacade>();
            _gameSceneData = diContainer.Resolve<GameSceneData>();
            _gameClock = diContainer.Resolve<GameClock>();
            _sceneAudioController = diContainer.Resolve<SceneAudioController>();
        }

        public void Enter()
        {
            var timeParam = _gameSceneData.CurrentSceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Morning);
            _sceneAudioController.ChangeNightParam(false);
            
            if (!_gameSceneData.CurrentSceneParams.TimeOfDaySettings.IsEmpty && timeParam == null)
            {
                _gameStateMachine.Enter<EveningState>();
                return;
            }
            
            _gameClock.SetTimeOfDay(TimeOfDay.Morning);
            _eventsFacade.TimeEvents.StartMorningEvent();
            WaitEvening().Forget();
        }
        
        
        public void Exit()
        {
            _cts?.Cancel();
            _eventsFacade.TimeEvents.EndMorningEvent();
        }

        private async UniTaskVoid WaitEvening()
        {
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => _gameClock.IsEveningTime, cancellationToken: _cts.Token);
            _gameStateMachine.Enter<EveningState>();
        }
    }
}