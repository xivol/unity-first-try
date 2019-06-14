using System;
using UnityEngine;
using Xivol.Events;

namespace Xivol.Values
{
    [CreateAssetMenu(menuName = "Values/Vector3 Value")]
    public class Vector3Value : Vector3Event
    {
        public new static readonly string DefaultAssetsFolder = "Values";

        protected Vector3 _value;
        public Vector3 Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_value.Equals(value))
                {
                    Vector3 old = _value;
                    _value = value;

                    Raise(_value);
                }
            }
        }

        #region PathBuilder
        protected new class PathBuilder : Vector3Event.PathBuilder
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

        public static new  Vector3Value LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<Vector3Value>(PathTo(fileName));
        }
    }
}
//AbstractValue<Vector3ValueListener, Vector3>
//    {

//    }
//}
