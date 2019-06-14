using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Xivol.Input
{
    public class NewInputManager : Singleton<NewInputManager>
    {
        Dictionary<string, InputAxisEvent> axes = new Dictionary<string, InputAxisEvent>();

		protected void Update()
		{
            foreach (var axis in axes)
            {
                axis.Value.Value = UnityEngine.Input.GetAxis(axis.Key);
                axis.Value.RawValue = UnityEngine.Input.GetAxisRaw(axis.Key);

                //if (UnityEngine.Input.GetButtonUp(axis.Key))
                //    axis.Value.RaiseButtonUp();

                //if (UnityEngine.Input.GetButtonDown(axis.Key))
                    //axis.Value.RaiseButtonDown();

            }
		}

        protected override void Initialize() 
        {
            var asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            var inputManager = new SerializedObject(asset);
            var axisArray = inputManager.FindProperty("m_Axes");

            if (axisArray.arraySize == 0)
                Debug.LogError("No axes in InputManager!");

            HashSet<string> names = new HashSet<string>();
            foreach (SerializedProperty axis in axisArray)
            {
                var axisName = axis.FindPropertyRelative("m_Name").stringValue;
                names.Add(axisName);
            }

            RegenerateAxes(names);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void RegenerateAxes(HashSet<string> names) 
        {
            var oldAssets = Directory.GetFiles(Application.dataPath.Remove(Application.dataPath.Length - 6) +
                                               InputAxisEvent.PathTo());
            foreach (var file in oldAssets)
            {
                var fileName = Path.GetFileName(file);
                if (!fileName.EndsWith(".asset")) continue;

                string axisName = fileName.Substring(0, fileName.IndexOf("Axis"));
                if (!names.Contains(axisName))
                {
                    AssetDatabase.DeleteAsset(InputAxisEvent.PathTo(fileName));
                }
                else 
                {
                    axes[axisName] = InputAxisEvent.LoadOrMake(fileName);
                    names.Remove(axisName);
                }
            }

            // newAssets
            foreach (string name in names)
            {
                var axis = InputAxisEvent.LoadOrMake(name + "Axis");
                AssetDatabase.CreateAsset(axis, InputAxisEvent.PathTo(name + "Axis" + ".asset"));
                axes[name] = axis;
                Debug.LogWarning("New InputAxis Created: " + name + "Axis");
            }

        }

    }
}