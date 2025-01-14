using System;
using System.Collections.Generic;
using Code.Infrastructure.StateMachine.States;
using Zenject;

namespace Code.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(DiContainer container)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this,container),
                [typeof(LoadLevelState)] = new LoadLevelState(this, container),
                [typeof(MorningState)] = new MorningState(this,container),
                [typeof(EveningState)] = new EveningState(this,container),
                [typeof(NightState)] = new NightState(this,container),
                [typeof(PauseState)] = new PauseState(this,container),
                
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();

            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}