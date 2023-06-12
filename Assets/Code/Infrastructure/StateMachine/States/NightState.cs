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
        private readonly TimeEvents _timeEvents;
        private readonly GameSceneData _gameSceneData;
        private readonly GameClock _gameClock;

        private CancellationTokenSource _cts;
        
        public NightState(GameStateMachine gameGameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameGameStateMachine;
            _timeEvents = diContainer.Resolve<TimeEvents>();
            _gameSceneData = diContainer.Resolve<GameSceneData>();
            _gameClock = diContainer.Resolve<GameClock>();
        }

        public void Enter()
        {
            var timeParam = _gameSceneData.SceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Night);
           
            if (!_gameSceneData.SceneParams.TimeOfDaySettings.IsEmpty && timeParam == null)
            {
                _gameStateMachine.Enter<MorningState>();
                return;
            }
            
            _timeEvents.StartNightEvent();
            
            WaitMorning().Forget();
        }
        
        
        public void Exit()
        {
            _cts?.Cancel();
            _timeEvents.EndNightEvent();
        }

        private async UniTaskVoid WaitMorning()
        {
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => _gameClock.IsMorningTime, cancellationToken: _cts.Token);
            _gameStateMachine.Enter<MorningState>();
        }
    }
}