using UnityEngine;

namespace Xivol.Events 
{
    [CreateAssetMenu(menuName = "Events/Vector2Int Event")]
    public class Vector2IntEvent : ParametrisedEvent<Vector2IntListener, Vector2Int>
    {
        public static Vector2IntEvent LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<Vector2IntEvent>(PathTo(fileName));
        }
    }
}
