using System;
using UnityEngine;
using UnityEngine.Events;

namespace Xivol.Events
{
    public class GameObjectListener :
        ParametrisedListener<GameObjectEvent, GameObjectListener, GameObject>
    {
        [Serializable]
        public new class SerializedEvent :
            ParametrisedListener<GameObjectEvent, GameObjectListener, GameObject>.SerializedEvent
        { }

        public SerializedEvent Response;

        public override void OnEventRaised(GameObject value)
        {
            Response.Invoke(value);
        }
    }
}
