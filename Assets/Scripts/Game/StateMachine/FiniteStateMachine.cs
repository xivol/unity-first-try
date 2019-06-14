using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Xivol.Events;

namespace Xivol.StateMachine
{
    public class FiniteStateMachine : Core<FiniteStateMachine>
    {
        public GameState InitialState;
        public UnityEvent StateChange;

        protected GameState _currentState;
        public virtual GameState CurrentState
        {
            get { return _currentState; }
            set { Transition(value); }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _currentState = InitialState;
        }

        protected bool _inTransition = false;

        protected virtual void Transition(GameState value)
        {
            if (_currentState == value || _inTransition)
                return;

            _inTransition = true;

            if (_currentState != null)
                _currentState.Leave();

            _currentState = value;

            if (_currentState != null)
                _currentState.Enter();

            _inTransition = false;

            StateChange.Invoke();
        }
    }
}
