
namespace Xivol.Events 
{
    public interface IEventListener
    {
        void OnEventRaised();
    }

    public interface IEventListener<T>
    {
        void OnEventRaised(T value);
    }

    public interface IEventListener<T1, T2>
    {
        void OnEventRaised(T1 value1, T2 value2);
    }
}