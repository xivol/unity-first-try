using UnityEngine;
using UnityEngine.Events;
using Xivol.Events;

namespace Xivol.StateMachine
{
    [RequireComponent(typeof(GameManager))]
    public class Transition : SingleEventListener<AbstractEvent<Transition>, Transition>,
        IEventListener
    {
        public GameState From;
        public GameState To;
        //public BoolValue Condition;
        public UnityEvent Response;

        protected GameManager GM;

        protected override void OnEnable()
        {
            base.OnEnable();
            GM = GetComponent<GameManager>();
            //GM.Register(From);
            //GM.Register(To);
            //GM.AddTransition(From, To);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
