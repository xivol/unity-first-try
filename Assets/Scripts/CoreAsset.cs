using System.IO;
using UnityEngine;
using UnityEditor;

public abstract class CoreAsset<T> : ScriptableObject where T : ScriptableObject
{
    public static readonly string DefaultAssetsFolder = "Scriptables";

    #region PathBuilder
    protected class PathBuilder {
        protected string path = "Assets/" + DefaultAssetsFolder + "/";

        public PathBuilder(string subFolder = null)
        {
            if (subFolder != null)
                path += subFolder + "/";
        }

        public virtual string PathTo(string fileName = null)
        {
            if (Path.GetPathRoot(fileName) == "Assets")
                return fileName;
            
            if (fileName == null)
                return path;
            return path + fileName;
        }
    }

    public static string PathTo(string fileName = null)
    {
        return new PathBuilder().PathTo(fileName);
    }
    #endregion

    public virtual void CreateAsset(string fileName, bool overwrite = false)
    {
        string path = PathTo(fileName);

        if (fileName == null)
            fileName = "New" + this.GetType();

        path += fileName;

        ScriptableObjectUtils.CreateAssetAtPath(this, path, overwrite);
    }

    public virtual void OnEnable()
    {
        var msg = this.AssetName();
        if (!string.IsNullOrEmpty(msg) && Application.isPlaying)
            Debug.Log("Enabling " + this.ScriptName());
    }

    public virtual void OnDisable()
    {
        var msg = this.AssetName();
        if (!string.IsNullOrEmpty(msg) && Application.isPlaying)
            Debug.Log("Disabling " + this.ScriptName());
    }
}