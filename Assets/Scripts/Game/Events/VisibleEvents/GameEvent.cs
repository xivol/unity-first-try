using UnityEngine;
using UnityEditor;

namespace Xivol.Events
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvent : AbstractEvent<GameEventListener>, 
        IEvent<GameEventListener>
    {
        public void Raise()
        {
            Debug.Log(this.AssetName() + " is Raised");
            base.Raise((t) => t.OnEventRaised());
        }

        public static GameEvent LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<GameEvent>(PathTo(fileName));
        }
    
    }
}
