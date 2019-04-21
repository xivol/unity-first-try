using System;
using UnityEngine;
using Xivol.Events;

namespace Xivol.Values
{
    [CreateAssetMenu(menuName = "Values/Vector2Int Value")]
    public class Vector2IntValue : Vector2IntEvent
    {
        public new static readonly string DefaultAssetsFolder = "Values";

        protected Vector2Int _value;
        public Vector2Int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_value.Equals(value))
                {
                    Vector2Int old = _value;
                    _value = value;

                    Raise(_value);
                }
            }
        }

        #region PathBuilder
        protected new class PathBuilder : Vector2IntEvent.PathBuilder
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

        public static new Vector2IntValue LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<Vector2IntValue>(PathTo(fileName));
        }
    }
}
//AbstractValue<Vector2IntValueListener, Vector2Int>
//    {

//    }
//}
