using UnityEngine;
using UnityEditor;
using Xivol.Events;
using Xivol.Input;

namespace Xivol.Inspectable
{
    [CustomEditor(typeof(InputAxisEvent))]
    public class InputAxisEventInspector : AbstractEventInspector<InputAxisEvent, InputAxisListener>
    {
        public override void OnInspectorGUI(InputAxisEvent myEvent)
        {
            base.OnInspectorGUI(myEvent);

            myEvent.Value = EditorGUILayout.FloatField("Value", myEvent.Value);
            myEvent.RawValue = EditorGUILayout.FloatField("RawValue", myEvent.RawValue);
        }
    }
}