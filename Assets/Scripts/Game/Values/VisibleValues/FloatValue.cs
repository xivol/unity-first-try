using System;
using UnityEngine;
using Xivol.Events;

namespace Xivol.Values
{
    [CreateAssetMenu(menuName = "Values/Float Value")]
    public class FloatValue : FloatEvent
    {
        public new static readonly string DefaultAssetsFolder = "Values";

        protected float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_value.Equals(value))
                {
                    float old = _value;
                    _value = value;

                    Raise(_value);
                }
            }
        }

        #region PathBuilder
        protected new class PathBuilder : FloatEvent.PathBuilder
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
    //public class FloatValue : AbstractValue<FloatValueListener, float>
    //{
    //    public static new FloatValue LoadOrMake(string fileName)
    //    {
    //        return ScriptableObjectUtils.LoadOrMake<FloatValue>(PathTo(fileName));
    //    }
    //}
}
