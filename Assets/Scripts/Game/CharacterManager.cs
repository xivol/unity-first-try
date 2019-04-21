using UnityEngine;
using System;
namespace Xivol
{
    public class CharacterManager : Core<CharacterManager>
    {
        private Actor actor;

        public UnityEngine.Events.UnityEvent AnimationDidEnd;
        public UnityEngine.Events.UnityEvent AnimationDidStart;

        public GameObject CurrentActor
        {
            get
            {
                return actor.gameObject;
            }

            set
            {
                if (actor != null)
                {
                    actor.traversalDidStart -= OnAnimationDidStart;
                    actor.traversalDidEnd   -= OnAnimationDidEnd;
                }
                if (value == null)
                {
                    actor = null;
                    return;
                }
                actor = value.GetComponent<Actor>();
                actor.traversalDidStart += OnAnimationDidStart;
                actor.traversalDidEnd   += OnAnimationDidEnd;
            }
        }

        protected void OnAnimationDidStart(object sender, EventArgs args)
        {
            AnimationDidStart.Invoke();
        }

        protected void OnAnimationDidEnd(object sender, EventArgs args)
        {
            AnimationDidEnd.Invoke();
        }

        public void MoveTo(Vector2Int target)
        {
            if (actor != null)
            {
                actor.MoveTo(target);
            }
        }
    }
}