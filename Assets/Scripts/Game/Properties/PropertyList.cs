using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PropertyList<T> : CoreAsset<PropertyList<T>>
{
    public List<T> Values = new List<T>();

    public new static readonly string DefaultAssetsFolder = "Properties";

    #region PathBuilder
    protected new class PathBuilder: CoreAsset<PropertyList<T>>.PathBuilder
    {
        public PathBuilder():base(DefaultAssetsFolder) 
        {}
    }

    public new static string PathTo(string fileName = null)
    {
        return new PathBuilder().PathTo(fileName);
    }
    #endregion

}