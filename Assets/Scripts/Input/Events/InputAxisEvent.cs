using UnityEngine;
using Xivol.Events;

namespace Xivol.Input
{
    [CreateAssetMenu(menuName = "Events/InputAxisEvent")]
    public class InputAxisEvent : AbstractEvent<InputAxisListener>
    {
        public new static readonly string DefaultAssetsFolder = "InputAxes";

        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaiseValueChanged(_value);
                }
            }
        }

        protected void RaiseValueChanged(float value)
        {
            base.Raise((t) => t.OnValueChanged(value));
        }

        private float _rawValue;
        public float RawValue
        {
            get
            {
                return _rawValue;
            }
            set
            {
                if (_rawValue != value)
                {
                    _rawValue = value;
                    RaiseRawValueChanged(_rawValue);
                }
            }
        }

        protected void RaiseRawValueChanged(float value)
        {
            base.Raise((t) => t.OnRawValueChanged(value));
        }

        #region PathBuilder
        protected new class PathBuilder : GameEvent.PathBuilder
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

        public static InputAxisEvent LoadOrMake(string fileName)
        {
            return ScriptableObjectUtils.LoadOrMake<InputAxisEvent>(PathTo(fileName));
        }
    }
}
