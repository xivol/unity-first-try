using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;
using Xivol.Events;

namespace Xivol.StateMachine
{
    [RequireComponent(typeof(FiniteStateMachine))]
    public class Transition : GameEventListener
    {
        public GameState From;
        public GameState To;

        protected FiniteStateMachine FSM;

        protected override void OnEnable()
        {
            base.OnEnable();
            FSM = GetComponent<FiniteStateMachine>();
        }

        public override void OnEventRaised()
        {
            if (FSM.CurrentState == From)
            {
                FSM.CurrentState = To;
                base.OnEventRaised();
            }
            else
                Debug.LogWarning("Trying to transition " + 
                    From.name + " -> " + 
                    To.name + " but current state is " + 
                    FSM.CurrentState.name);
        }
    }
}
