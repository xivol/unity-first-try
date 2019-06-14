using UnityEngine;
using UnityEditor;
using Xivol.Events;

namespace Xivol.Inspectable
{

    public static class AbstractEventExstension
    {
        public static void PingListeners<T>(this IAbstractEvent<T> @event) where T : Component
        {
            foreach (var listener in @event.Listeners())
                EditorGUIUtility.PingObject(listener.gameObject);
        }

    }

    public class AbstractEventInspector<TEvent, TListener> : Editor, IInspectable<TEvent>
        where TEvent : Object, IAbstractEvent<TListener>
        where TListener : Component

    {
        protected bool pinged = false;

        public virtual void OnInspectorGUI(TEvent myEvent)
        {
            if (!pinged)
            {
                myEvent.PingListeners();
                pinged = true;
            }
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            serializedObject.Update();
            OnInspectorGUI(target as TEvent);
            serializedObject.ApplyModifiedProperties();
        }
    }
}