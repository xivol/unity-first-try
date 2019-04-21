using System;
using UnityEngine;
using UnityEngine.Events;
using Xivol.Events;

namespace Xivol.Input
{
    public class Vector3InputAxisListener : InputAxisListener
    {
        public Vector3 Direction = Vector3.zero;

        [Serializable]
        public class SerializedEvent : UnityEvent<Vector3> { }

        public SerializedEvent ValueChanged;
        public SerializedEvent RawValueChanged;

        public override void OnValueChanged(float value)
        {
            ValueChanged.Invoke(Direction * value);
        }

        public override void OnRawValueChanged(float value)
        {
            RawValueChanged.Invoke(Direction * value);
        }
    }
}