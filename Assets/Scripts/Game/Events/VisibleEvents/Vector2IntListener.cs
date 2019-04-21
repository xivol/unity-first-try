using System;
using UnityEngine;
using UnityEngine.Events;

namespace Xivol.Events
{
    public class Vector2IntListener : 
        ParametrisedListener<Vector2IntEvent, Vector2IntListener, Vector2Int>
    {
        [Serializable]
        public new class SerializedEvent : 
            ParametrisedListener<Vector2IntEvent, Vector2IntListener, Vector2Int>.SerializedEvent 
        { }

        public SerializedEvent Response;

        public override void OnEventRaised(Vector2Int value)
        {
            Response.Invoke(value);
        }
    }
}
