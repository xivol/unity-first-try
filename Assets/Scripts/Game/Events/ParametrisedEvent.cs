using UnityEngine;

namespace Xivol.Events
{
    public class ParametrisedEvent<TListener, T> : AbstractEvent<TListener>, IEvent<TListener, T>
        where TListener : Component, IEventListener<T>
    {
        public void Raise(T value)
        {
            base.Raise((t) => t.OnEventRaised(value));
        }
    }


    public class ParametrisedEvent<TListener,T1,T2> : AbstractEvent<TListener>, IEvent<TListener,T1, T2>
        where TListener : Component, IEventListener<T1,T2>
    {
        public void Raise(T1 value1, T2 value2)
        {
            base.Raise((t) => t.OnEventRaised(value1, value2));
        }
    }
}
