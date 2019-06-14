using UnityEngine;

namespace Xivol.Events
{
    [CreateAssetMenu(menuName = "Events/Float Event")]
    public class FloatEvent : ParametrisedEvent<FloatListener, float>
    {
        public static FloatEvent LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<FloatEvent>(PathTo(fileName));
        }
    }
}
