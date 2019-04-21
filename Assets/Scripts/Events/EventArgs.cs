using System;
namespace Xivol.Events
{
    public class EventArgs<T> where T: struct
    {
        public T Value;

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}
