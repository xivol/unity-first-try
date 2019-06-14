using System;
using System.Collections.Generic;

namespace Xivol.Events
{
    public abstract class MultipleEventListener<TEvent, TListener> : 
        AbstractListener<TEvent, TListener>
        where TEvent : IAbstractEvent<TListener>
        where TListener : MultipleEventListener<TEvent, TListener>
    {
        [Serializable]
        public class SerializedEventsList : List<TEvent> { }

        public SerializedEventsList Events = new SerializedEventsList();

        protected override void OnEnable()
        {
            base.OnEnable();
            foreach(var _event in Events)
                _event.Register((TListener)this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            foreach (var _event in Events)
                _event.Unregister((TListener)this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (var _event in Events)
                _event.Unregister((TListener)this);
        }
    }
}