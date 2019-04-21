using System;
using UnityEngine;
using Xivol.Events;

namespace Xivol.Values
{
    [CreateAssetMenu(menuName = "Values/GameObject Value")]
    public class GameObjectValue : GameObjectEvent
    {
        public new static readonly string DefaultAssetsFolder = "Values";

        protected GameObject _value;
        public GameObject Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    //GameObject old = _value;
                    _value = value;

                    Raise(_value);
                }
            }
        }

        #region PathBuilder
        protected new class PathBuilder : GameObjectEvent.PathBuilder
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
    //public class GameObjectValue : AbstractValue<GameObjectValueListener, GameObject>
    //{
    //    public static GameObjectValue LoadOrMake(string fileName)
    //    {
    //        return ScriptableObjectUtils.LoadOrMake<GameObjectValue>(PathTo(fileName));
    //    }
    //}
}