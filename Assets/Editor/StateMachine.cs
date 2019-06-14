using UnityEngine;
using UnityEditor;
using Xivol.StateMachine;
using Xivol.Events;

namespace Xivol.Inspectable
{
    [CustomEditor(typeof(Transition))]
    public class TransitionInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            Transition t = (Transition)target;

            serializedObject.Update();

            t.From = (GameState)EditorGUILayout.ObjectField("From State",
                t.From, typeof(GameState), true);

            t.To = (GameState)EditorGUILayout.ObjectField("To State",
                t.To, typeof(GameState), true);

            t.Event = (GameEvent)EditorGUILayout.ObjectField("Triggered By",
                t.Event, typeof(GameEvent), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Response"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(GameState))]
    public class GameStateInspector : Editor, IInspectable<GameState>
    {
        protected bool pinged = false;
        public virtual void OnInspectorGUI(GameState state)
        {
            if (!pinged)
            { 
                state.EnterEvent.PingListeners();
                state.LeaveEvent.PingListeners();
                pinged = true;
            }

            if (GUILayout.Button("Enter"))
            {
                state.Enter();
            }

            if (GUILayout.Button("Leave"))
            {
                state.Leave();
            }
        }

        public override void OnInspectorGUI()
        {
            GameState state = (GameState) target;

            OnInspectorGUI(state);
        }
    }
}