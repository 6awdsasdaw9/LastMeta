using System.Threading;
using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.DayOfTime;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class MorningState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly TimeEvents _timeEvents;
        private readonly GameSceneData _gameSceneData;
        private readonly GameClock _gameClock;

        private CancellationTokenSource _cts;
        
        public MorningState(GameStateMachine gameGameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameGameStateMachine;
            _timeEvents = diContainer.Resolve<TimeEvents>();
            _gameSceneData = diContainer.Resolve<GameSceneData>();
            _gameClock = diContainer.Resolve<GameClock>();
        }

        public void Enter()
        {
            var timeParam = _gameSceneData.SceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Morning);
           
            if (!_gameSceneData.SceneParams.TimeOfDaySettings.IsEmpty && timeParam == null)
            {
                _gameStateMachine.Enter<EveningState>();
                return;
            }
            
            _timeEvents.StartMorningEvent();
            
            WaitEvening().Forget();
        }
        
        
        public void Exit()
        {
            _cts?.Cancel();
            _timeEvents.EndMorningEvent();
        }

        private async UniTaskVoid WaitEvening()
        {
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => _gameClock.IsEveningTime, cancellationToken: _cts.Token);
            _gameStateMachine.Enter<EveningState>();
        }
    }
}