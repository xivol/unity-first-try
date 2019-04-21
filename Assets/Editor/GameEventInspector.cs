using UnityEngine;
using UnityEditor;
using Xivol.Events;

namespace Xivol.Inspectable
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventInspector : AbstractEventInspector<GameEvent, GameEventListener>
    {
        public override void OnInspectorGUI(GameEvent myEvent)
        {
            base.OnInspectorGUI(myEvent);

            if (GUILayout.Button("Raise"))
            {
                myEvent.Raise();
            }
        }
    }

    [CustomEditor(typeof(Vector3Event))]
    public class Vector3GameEventInspector : ParametrisedEventInspector<Vector3Event, Vector3Listener, Vector3>
    {
        protected override Vector3 RenderField(Vector3 value)
        {
            return EditorGUILayout.Vector3Field("Value", value);
        }
    }
}