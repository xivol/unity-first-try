using System;
using UnityEngine.Events;
using Xivol.Events;

namespace Xivol.Input
{
    public interface IValueChangeResponsive<T>
    {
        void OnValueChanged(T value);
        void OnRawValueChanged(T value);
    }

    public abstract class InputAxisListener : 
        SingleEventListener<InputAxisEvent, InputAxisListener>,
        IValueChangeResponsive<float>
    {
        public abstract void OnValueChanged(float value);
        //{
        //    ValueChanged.Invoke(value);
        //}

        public abstract void OnRawValueChanged(float value);
        //{
        //    RawValueChanged.Invoke(value);
        //}
    }
}