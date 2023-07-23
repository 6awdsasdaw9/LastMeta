using Code.Infrastructure.GlobalEvents;
using Zenject;

namespace Code.Infrastructure.StateMachine.States
{
    public class PauseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EventsFacade _eventsFacade;

        public PauseState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _eventsFacade = container.Resolve<EventsFacade>();
        }

        public void Enter()
        {
            _eventsFacade.SceneEvents.StartPauseEvent();
            //TODO заглушить музыку. остановить движение
        }

        public void Exit()
        {
            _eventsFacade.SceneEvents.StopPauseEvent();
            //TODO вернуть музыку. продолжить движение
        }
    }
}