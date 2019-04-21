using System;
using UnityEngine.Events;
using Xivol.Events;

namespace Xivol.Input
{
    public class FloatInputAxisListener : InputAxisListener
    {
        [Serializable]
        public class SerializedEvent : UnityEvent<float> { }

        public float Scale = 1.0f;

        public SerializedEvent ValueChanged;
        public SerializedEvent RawValueChanged;

        public override void OnValueChanged(float value)
        {
            ValueChanged.Invoke(Scale * value);
        }

        public override void OnRawValueChanged(float value)
        {
            RawValueChanged.Invoke(Scale * value);
        }
    }
}