using UnityEngine;
using System.Collections.Generic;

namespace Xivol.Events
{   
    public interface IAbstractEvent<TListener>
    {
        IEnumerable<TListener> Listeners();
        void Register(TListener listener);
        void Unregister(TListener listener);
    }

    public interface IEvent<TListener> : IAbstractEvent<TListener>
    {
        void Raise();
    }

    public interface IEvent<TListener, T> : IAbstractEvent<TListener>
        where TListener : IEventListener<T>
    {
        void Raise(T value);
    }

    public interface IEvent<TListener, T1, T2> : IAbstractEvent<TListener>
        where TListener : IEventListener<T1, T2>
    {
        void Raise(T1 value1, T2 value2);
    }
}