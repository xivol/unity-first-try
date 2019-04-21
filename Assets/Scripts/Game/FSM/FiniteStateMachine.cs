using UnityEngine;
using System;
using System.Collections.Generic;
using Xivol.Events;

namespace Xivol.StateMachine
{
    public class Condition { public bool IsTrue { get; set; } }
    public class FiniteStateMachine : Core<FiniteStateMachine>
    {
        [Serializable]
        public class GameStatesList: List<GameState> { }

        public GameStatesList GameStates;

        public Dictionary<GameState, Dictionary<GameState, Condition>> Condition;



        public GameStateEvent OnStateEnter;
        public GameStateEvent OnStateExit;


       //private GameStateEvent OnStateChange;
    }
}
