
using System;

namespace Xivol.Events
{
    public class  FloatListener : ParametrisedListener<FloatEvent, FloatListener, float>
    { 
        [Serializable]
        public new class SerializedEvent :
            ParametrisedListener<FloatEvent, FloatListener, float>.SerializedEvent
        { }

        public SerializedEvent Response;

        public override void OnEventRaised(float value)
        {
            Response.Invoke(value);
        }
    }
}