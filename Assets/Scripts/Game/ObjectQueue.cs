using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xivol
{
    public abstract class ObjectQueue<T> : Core<ObjectQueue<T>>
        where T : Component
    {
        protected Queue<GameObject> q = new Queue<GameObject>();

        [Serializable]
        public class SerializedEvent : UnityEngine.Events.UnityEvent<GameObject>
        { }

        protected GameObject current;

        public bool Cycled = true;

        protected override void OnEnable()
        {
            base.OnEnable();
            foreach (var obj in FindObjectsOfType(typeof(T)))
                q.Enqueue((obj as T).gameObject);
        }

        public virtual void Pop()
        {
            if (Cycled && current != null) // back in line
            {
                q.Enqueue(current);
                Debug.Log(name + ": " + current + " Push");
            }

            current = q.Dequeue();
            Debug.Log(name + ": " + current + " Pop");
        }
    }
}