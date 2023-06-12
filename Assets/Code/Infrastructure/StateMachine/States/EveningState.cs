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
        private readonly TimeEvents _timeEvents;
        private readonly GameSceneData _gameSceneData;
        private readonly GameClock _gameClock;

        private CancellationTokenSource _cts;
        
        public EveningState(GameStateMachine gameGameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameGameStateMachine;
            _timeEvents = diContainer.Resolve<TimeEvents>();
            _gameSceneData = diContainer.Resolve<GameSceneData>();
            _gameClock = diContainer.Resolve<GameClock>();
        }

        public void Enter()
        {
            var timeParam = _gameSceneData.SceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Evening);
           
            if (!_gameSceneData.SceneParams.TimeOfDaySettings.IsEmpty && timeParam == null)
            {
                _gameStateMachine.Enter<NightState>();
                return;
            }
            
            _timeEvents.StartEveningEvent();

            WaitNight().Forget();
        }
        
        
        public void Exit()
        {
            _cts?.Cancel();
            _timeEvents.EndEveningEvent();
        }

        private async UniTaskVoid WaitNight()
        {
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => _gameClock.IsNightTime, cancellationToken: _cts.Token);
            _gameStateMachine.Enter<NightState>();
        }
    }
}