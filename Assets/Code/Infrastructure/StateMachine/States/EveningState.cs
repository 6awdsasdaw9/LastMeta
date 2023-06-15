using System.Threading;
using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Infrastructure.StateMachine.States;
using Code.Logic.DayOfTime;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Infrastructure.StateMachine
{
    public class EveningState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EventsFacade _eventsFacade;
        private readonly GameSceneData _gameSceneData;
        private readonly GameClock _gameClock;

        private CancellationTokenSource _cts;
        
        public EveningState(GameStateMachine gameGameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameGameStateMachine;
            _eventsFacade = diContainer.Resolve<EventsFacade>();
            _gameSceneData = diContainer.Resolve<GameSceneData>();
            _gameClock = diContainer.Resolve<GameClock>();
        }

        public void Enter()
        {
            var timeParam = _gameSceneData.CurrentSceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Evening);
           
            if (!_gameSceneData.CurrentSceneParams.TimeOfDaySettings.IsEmpty && timeParam == null)
            {
                _gameStateMachine.Enter<NightState>();
                return;
            }
            
            _gameClock.SetTimeOfDay(TimeOfDay.Evening);
            _eventsFacade.TimeEvents.StartEveningEvent();

            WaitNight().Forget();
        }
        
        
        public void Exit()
        {
            _cts?.Cancel();
            _eventsFacade.TimeEvents.EndEveningEvent();
        }

        private async UniTaskVoid WaitNight()
        {
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => _gameClock.IsNightTime, cancellationToken: _cts.Token);
            _gameStateMachine.Enter<NightState>();
        }
    }
}