using UnityEngine;
using UnityEngine.Events;
using System;

namespace Xivol.Events
{
    public abstract class ParametrisedListener<TEvent, TListener, T> : 
        SingleEventListener<TEvent, TListener>,
        IEventListener<T>
        where TEvent : AbstractEvent<TListener>
        where TListener : SingleEventListener<TEvent, TListener>
    {
        [Serializable]
        public class SerializedEvent : UnityEvent<T> { }

        //public SerializedEvent Response;

        public abstract void OnEventRaised(T value);
        //{
        //    Response.Invoke(value);
        //}

        public virtual void Test(T value)
        {
            Debug.Log("Test for: " + value);
        }
    }

    //public abstract class ParametrisedListener<TEvent, T1, T2> :
    //    SingleEventListener<AbstractEvent<ParametrisedListener<T1, T2>>, ParametrisedListener<T1, T2>>,
    //    IEventListener<T1, T2>
    //{
    //    [Serializable]
    //    public class SerializedEvent : UnityEvent<T1, T2> { }

    //    public SerializedEvent Response;

    //    public virtual void OnEventRaised(T1 value1, T2 value2)
    //    {
    //        Response.Invoke(value1, value2);
    //    }

    //    public virtual void Test(T1 value1, T2 value2)
    //    {
    //        Debug.Log("Test for: " + value1 + ", " + value2);
    //    }
    //}
}