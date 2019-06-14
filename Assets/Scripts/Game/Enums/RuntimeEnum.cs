using UnityEngine;
using System;
using System.Collections.Generic;

namespace Xivol.Enums
{
    public abstract class RuntimeEnum : CoreAsset<RuntimeEnum>
    {
        protected static Dictionary<int, HashSet<RuntimeEnum>> everything = new Dictionary<int, HashSet<RuntimeEnum>>();

        public virtual void OnEnable()
        {
            int type = this.GetType().GetHashCode();
            if (everything.ContainsKey(type))
            {
                if (everything[type].Contains(this))
                    throw new ArgumentException("Value with " + this.GetInstanceID() + " of " + this.GetType() + " is already in enum");
                else
                    everything[type].Add(this);
            }
            else
            {
                everything[type] = new HashSet<RuntimeEnum>();
                everything[type].Add(this);
            }
        }

        public virtual void OnDestroy()
        {
            int type = this.GetType().GetHashCode();
            everything[type].Remove(this);
        }

        public static T[] GetValues<T>() where T: RuntimeEnum
        {
            int type = typeof(T).GetHashCode();
            if (!everything.ContainsKey(type))
                return null;

            var result = new T[everything[type].Count];
            int i = 0;
            foreach(var value in everything[type])
            {
                result[i++] = value as T;
            }
            return result;
        } 
    }
}