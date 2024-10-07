using System;
using System.Collections.Generic;

namespace ShootEmUp.View
{
    public class Fsm
    {
        private State _curentState;
        private Dictionary<Type, State> _states = new();

        public void SetState<T>() where T : State
        {
            var type = typeof(T);

            if (_curentState != null && _curentState.GetType() == type)
                return;

            if (_states.TryGetValue(type, out var nextState))
            {
                _curentState?.Exit();
                _curentState = nextState;
                _curentState.Enter();
            }
            else
            {
                throw new InvalidOperationException();
            }

        }

        public Fsm BindState(State state)
        {
            _states.TryAdd(state.GetType(), state);
            return this;
        }

        public void Update()
        => _curentState.Update();
    }
}
