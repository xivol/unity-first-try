using UnityEngine;
using UnityEditor;

namespace Xivol.Inspectable
{
    [CustomPropertyDrawer(typeof(StringReference), true)]
    public class StringReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);

            bool internalReference = property.type == typeof(InternalStringReference).ToString();
            if (!internalReference)
            {
                position.height = EditorGUIUtility.singleLineHeight;
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
            }

            if (property.isExpanded || internalReference)
            {
                var referencedList = property.FindPropertyRelative("ReferencedList");
                if (!internalReference)
                {
                    EditorGUI.indentLevel += 1;
                    position.position += new Vector2(0, EditorGUIUtility.singleLineHeight);

                    EditorGUI.PropertyField(position, referencedList, true);
                    position.position += new Vector2(0, EditorGUI.GetPropertyHeight(referencedList) +
                                                     EditorGUIUtility.standardVerticalSpacing);
                }

                StringList options = referencedList.objectReferenceValue as StringList;
                if (options != null)
                {
                    int value = property.FindPropertyRelative("_value").intValue;

                    EditorGUI.BeginChangeCheck();
                    value = EditorGUI.Popup(position, internalReference ? property.name : "Value",
                                            value, options.Values.ToArray());
                    if (EditorGUI.EndChangeCheck())
                        property.FindPropertyRelative("_value").intValue = value;
                }
                else
                {
                    var error = new GUIStyle(EditorStyles.label);
                    error.normal.textColor = Color.red;
                    EditorGUI.LabelField(position, "Value", "No list reference", error);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded && property.type != typeof(InternalStringReference).ToString())
            {
                totalHeight *= property.CountInProperty();
            }

            return totalHeight;
        }
    }
}