using UnityEngine;
using UnityEngine.Events;

namespace Xivol.Events 
{
    public class GameEventListener : 
        SingleEventListener<GameEvent, GameEventListener>,
        IEventListener
    {
        public UnityEvent Response;
   
        public virtual void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
