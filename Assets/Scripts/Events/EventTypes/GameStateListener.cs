using System;
using UnityEngine;

namespace Xivol.Events
{
    public abstract class Vector3Listener : 
       ParametrisedListener<Vector3Event, Vector3Listener, Vector3>
    {
        [Serializable]
        public new class SerializedEvent : 
            ParametrisedListener<Vector3Event, Vector3Listener, Vector3>.SerializedEvent 
        { }

        public SerializedEvent Response;

        public override void OnEventRaised(Vector3 value)
        {
            Response.Invoke(value);
        }
    }
}
