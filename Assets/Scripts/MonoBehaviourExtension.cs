using UnityEngine;

public static class MonoBehaviourExtension
{
    public static string ScriptName(this MonoBehaviour mb) { return mb.name + ":" + mb.GetType(); }

    public static GameObject FindDisabledObject(this MonoBehaviour mb, string objectName)
    {
        UnityEngine.Object[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach (UnityEngine.Object o in all)
        {
            if (o.name == objectName)
                return (GameObject)o;
        }
        return null;
    }
}
