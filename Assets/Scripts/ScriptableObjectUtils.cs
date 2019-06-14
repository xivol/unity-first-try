using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public static class ScriptableObjectUtils
{
    public static string ScriptName(this ScriptableObject so)
    {
        return so.GetType().ToString();
    }

    public static string AssetName(this ScriptableObject so)
    {
        return Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(so.GetInstanceID()));
    }

    public static T LoadOrMake<T>(string path) where T : ScriptableObject
    {
        return LoadAsset<T>(path) ?? ScriptableObject.CreateInstance<T>();
    }

    public static T LoadAsset<T>(string path) where T : ScriptableObject
    {
        if (!path.StartsWith("Assets", StringComparison.InvariantCulture))
            path = "Assets/" + path;
        if (!path.EndsWith(".asset", StringComparison.InvariantCulture))
            path += ".asset";
        
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }

    public static void CreateAssetAtPath(ScriptableObject so, string path, bool overwrite = false)
    {
        var currentPath = AssetDatabase.GetAssetPath(so);
        if (!overwrite && currentPath != "") return;
        if (overwrite && currentPath != "")
            AssetDatabase.DeleteAsset(currentPath);

        if (!path.StartsWith("Assets", StringComparison.InvariantCulture))
            path = "Assets/" + path;
        if (!path.EndsWith(".asset", StringComparison.InvariantCulture))
            path += ".asset";

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);
        AssetDatabase.CreateAsset(so, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void DeleteAsset(string path) 
    {
        if (!path.StartsWith("Assets", StringComparison.InvariantCulture))
            path = "Assets/" + path;
        if (!path.EndsWith(".asset", StringComparison.InvariantCulture))
            path += ".asset";
        
        AssetDatabase.DeleteAsset(path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();    
    }
}
