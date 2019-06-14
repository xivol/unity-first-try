using System;
using System.Collections.Generic;
using UnityEngine;
using Xivol;

namespace Xivol.AI
{   
    public class CharacterQueue : ObjectQueue<Actor>
    {
        [Serializable]
        public new class SerializedEvent : ObjectQueue<Actor>.SerializedEvent
        { }
        public SerializedEvent CharacterDequeued;

        public override void Pop()
        {
            base.Pop();
            CharacterDequeued.Invoke(current);
        }
    }
}
