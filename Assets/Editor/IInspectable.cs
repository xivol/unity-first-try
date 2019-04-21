using System;
namespace Xivol.Inspectable
{
    public interface IInspectable<T>
    {
        void OnInspectorGUI(T val);
    }
}
