using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

namespace Xivol.Events 
{
    public abstract class AbstractEvent<T> : CoreAsset<AbstractEvent<T>>, IAbstractEvent<T>
    {
        public new static readonly string DefaultAssetsFolder = "Events";

        protected List<T> listeners = new List<T>();

        public IEnumerable<T> Listeners()
        {
            for (int i = listeners.Count - 1; i >= 0; i -= 1)
            {
                //Debug.Log("" + i);
                yield return listeners[i];
            }
        }

        public void Register(T listener)
        {
            listeners.Add(listener);
        }

        public void Unregister(T listener)
        {
            listeners.Remove(listener);
        }

        protected void Raise(Action<T> callback)
        {
            for (int i = listeners.Count - 1; i >= 0; i -= 1)
                callback(listeners[i]);
        }

        #region PathBuilder
        protected new class PathBuilder : CoreAsset<AbstractEvent<T>>.PathBuilder
        {
            public PathBuilder(string subFolder = null) : base(DefaultAssetsFolder)
            {
                if (subFolder != null)
                    path += subFolder + "/";
            }
        }

        public new static string PathTo(string fileName = null)
        {
            return new PathBuilder().PathTo(fileName);
        }
        #endregion
    }
}
