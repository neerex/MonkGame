using System;
using System.Collections.Generic;
using MainGame.Utilities;

namespace MainGame.Infrastructure.StateMachine.Core
{
    public abstract class StateMachineBase
    {
        protected Dictionary<Type, IExitable> States = new();
        private IExitable _activeState;

        protected void RegisterState<TState>(TState state) where TState : class, IExitable
        {
            Type type = typeof(TState);
            States[type] = state;
            
            Logger.Log($"State {state} added. States count: {States.Count}");
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitable
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        } 

        private TState GetState<TState>() where TState : class, IExitable
        {
            Type type = typeof(TState);
            return States[type] as TState;
        }
    }
}