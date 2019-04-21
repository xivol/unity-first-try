using System;
using UnityEngine;
using Xivol.Events;

namespace Xivol.Values
{


    public abstract class AbstractValue<TListener, T> : 
        CoreAsset<AbstractValue<TListener, T>>
        where TListener : Component, IEventListener<T>, IEventListener<T,T>
    {
        public new static readonly string DefaultAssetsFolder = "Values";

        public class ValueChangedEvent : ParametrisedEvent<TListener, T>
        { }
        public ValueChangedEvent ValueChanged { get; private set; }

        public class ValueHistoryEvent : ParametrisedEvent<TListener, T, T>
        { }
        public ValueHistoryEvent ValueHistory { get; private set; }

        public override void OnEnable()
        {
            base.OnEnable();
            ValueChanged = CreateInstance<ValueChangedEvent>();
            ValueHistory = CreateInstance<ValueHistoryEvent>();
        }

        protected T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_value.Equals(value))
                {
                    T old = _value;
                    _value = value;

                    ValueChanged.Raise(_value);
                    ValueHistory.Raise(old, _value);
                }
            }
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