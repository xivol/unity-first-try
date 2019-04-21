using System;
using UnityEngine.Events;
using Xivol.Events;

namespace Xivol.Values
{
    public abstract class AbstractValueListener<TValue, TListener, T> :
        Core< AbstractValueListener<TValue, TListener, T> >,
        IEventListener<T>, 
        IEventListener<T, T>
        where TValue : AbstractValue<TListener, T>
        where TListener : AbstractValueListener<TValue, TListener, T>
    {
        public TValue Value;

        protected override void OnEnable()
        {
            base.OnEnable();
            Value.ValueChanged.Register(this as TListener);
            Value.ValueHistory.Register(this as TListener);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Value.ValueChanged.Unregister(this as TListener);
            Value.ValueHistory.Unregister(this as TListener);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Value.ValueChanged.Unregister(this as TListener);
            Value.ValueHistory.Unregister(this as TListener);
        }

        [Serializable]
        public class SerializedSingleEvent : UnityEvent<T>
        { }

        public abstract void OnEventRaised(T value);

        [Serializable]
        public class SerializedDoubleEvent : UnityEvent<T, T>
        { }

        public abstract void OnEventRaised(T value1, T value2);
    }
}
