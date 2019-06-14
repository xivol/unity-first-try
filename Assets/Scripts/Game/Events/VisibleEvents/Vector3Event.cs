using UnityEngine;

namespace Xivol.Events
{
    [CreateAssetMenu(menuName = "Events/Vector3 Event")]
    public class Vector3Event : ParametrisedEvent<Vector3Listener, Vector3>
    {
        public static Vector3Event LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<Vector3Event>(PathTo(fileName));
        }
    }
}
