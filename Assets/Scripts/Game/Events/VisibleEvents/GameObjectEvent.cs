using UnityEngine;

namespace Xivol.Events
{
    [CreateAssetMenu(menuName = "Events/GameObject Event")]
    public class GameObjectEvent : ParametrisedEvent<GameObjectListener, GameObject>
    {
        public static GameObjectEvent LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<GameObjectEvent>(PathTo(fileName));
        }
    }
}
