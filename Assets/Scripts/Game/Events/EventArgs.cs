using System;
namespace Xivol.Events
{
    public class EventArgs<T> : EventArgs where T : struct
    {
        public T Value;

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}
