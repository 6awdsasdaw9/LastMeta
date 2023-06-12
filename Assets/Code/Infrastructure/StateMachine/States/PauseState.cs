using Code.Infrastructure.GlobalEvents;
using Code.Infrastructure.StateMachine.States;
using Zenject;

namespace Code.Infrastructure.StateMachine
{
    public class PauseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneEvents _sceneEvents;

        public PauseState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _sceneEvents = container.Resolve<SceneEvents>();
        }

        public void Enter()
        {
            _sceneEvents.StartPauseEvent();
            //TODO заглушить музыку. остановить движение
        }

        public void Exit()
        {
            _sceneEvents.StopPauseEvent();
            //TODO вернуть музыку. продолжить движение
        }
    }
}