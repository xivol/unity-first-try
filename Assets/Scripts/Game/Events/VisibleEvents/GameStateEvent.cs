using UnityEngine;
using Xivol.StateMachine;

namespace Xivol.Events
{
    [CreateAssetMenu(menuName = "Events/GameState Event")]
    public class  GameStateEvent : AbstractEvent<GameStateListener>, IEvent<GameStateListener, GameState>
    {
        public static GameStateEvent LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<GameStateEvent>(PathTo(fileName));
        }

        public void Raise(GameState state)
        {
            if (this == state.EnterEvent)
                base.Raise((t) => t.OnEnter(state));
            else if (this == state.LeaveEvent)
                base.Raise((t) => t.OnLeave(state));
            else
                base.Raise((t) => t.OnEventRaised(state));
        }
    }
}
