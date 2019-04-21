using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

namespace Xivol.Events
{

    public abstract class AbstractListener<TEvent, TListener> : Core<AbstractListener<TEvent, TListener>>
       where TEvent : IAbstractEvent<TListener>
        where TListener : AbstractListener<TEvent, TListener>
    { }
    
    /// <summary>
    /// This is some kind of MAGIC! MAGIC!!!
    /// </summary>
    public abstract class SingleEventListener<TEvent, TListener> : AbstractListener<TEvent, TListener>
        where TEvent : IAbstractEvent<TListener>
        where TListener : SingleEventListener<TEvent, TListener>
    {
        public TEvent Event;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Event != null)
                Event.Register((TListener)this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (Event != null)
                Event.Unregister((TListener)this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (Event != null)
                Event.Unregister((TListener)this);
        }
    }
}