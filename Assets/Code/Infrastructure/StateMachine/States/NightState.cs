using System.Threading;
using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Infrastructure.StateMachine.States;
using Code.Logic.DayOfTime;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Infrastructure.StateMachine
{
    public class NightState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EventsFacade _eventsFacade;
        private readonly GameSceneData _gameSceneData;
        private readonly GameClock _gameClock;

        private CancellationTokenSource _cts;
        
        public NightState(GameStateMachine gameGameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameGameStateMachine;
            _eventsFacade = diContainer.Resolve<EventsFacade>();
            _gameSceneData = diContainer.Resolve<GameSceneData>();
            _gameClock = diContainer.Resolve<GameClock>();
        }

        public void Enter()
        {
            var timeParam = _gameSceneData.CurrentSceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Night);
           
            if (!_gameSceneData.CurrentSceneParams.TimeOfDaySettings.IsEmpty && timeParam == null)
            {
                _gameStateMachine.Enter<MorningState>();
                return;
            }
            
            _gameClock.SetTimeOfDay(TimeOfDay.Night);
            _eventsFacade.TimeEvents.StartNightEvent();
            
            WaitMorning().Forget();
        }
        
        
        public void Exit()
        {
            _cts?.Cancel();
            _eventsFacade.TimeEvents.EndNightEvent();
        }

        private async UniTaskVoid WaitMorning()
        {
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => _gameClock.IsMorningTime, cancellationToken: _cts.Token);
            _gameStateMachine.Enter<MorningState>();
        }
    }
}