using UnityEngine;
using UnityEditor;
using Xivol.Events;

namespace Xivol.Inspectable
{
    public abstract class ParametrisedEventInspector<TEvent, TListener, T> : 
        AbstractEventInspector<TEvent, TListener>
        where TEvent : Object, IEvent<TListener, T>
        where TListener : Component, IEventListener<T>
    {
        protected T value;

        protected abstract T RenderField(T value);

        public override void OnInspectorGUI(TEvent myEvent)
        {
            base.OnInspectorGUI(myEvent);

            value = RenderField(value);

            if (GUILayout.Button("Raise"))
            {
                myEvent.Raise(value);
            }
        }
    }
}