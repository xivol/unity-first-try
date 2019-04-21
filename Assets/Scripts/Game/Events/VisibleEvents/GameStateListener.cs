using System;
using UnityEngine;
using UnityEngine.Events;
using Xivol.StateMachine;

namespace Xivol.Events
{
    public class GameStateListener : 
        AbstractListener<GameStateEvent, GameStateListener>,
        IEventListener<GameState>
    {
        public GameState GameState;

        protected override void OnEnable()
        {
            base.OnEnable();
            GameState.EnterEvent.Register(this);
            GameState.LeaveEvent.Register(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameState.EnterEvent.Unregister(this);
            GameState.LeaveEvent.Unregister(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameState.EnterEvent.Unregister(this);
            GameState.LeaveEvent.Unregister(this);
        }

        [Serializable]
        public class SerializedEvent : UnityEvent<GameState>
        { }

        public SerializedEvent Enter;

        public SerializedEvent Leave;

        public virtual void OnEnter(GameState value)
        {
            //Debug.Log(name + ": " + value.AssetName() + " Enter");
            Enter.Invoke(value);
        }

        public virtual void OnLeave(GameState value)
        {
            //Debug.Log(name + ": " + value.AssetName() + " Exit");
            Leave.Invoke(value);
        }

        public void OnEventRaised(GameState value)
        {
            Debug.LogError("GameStateListener called without enter or exit");
        }
    }
}
