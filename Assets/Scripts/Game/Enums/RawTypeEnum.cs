using UnityEngine;

namespace Xivol.Enums
{
    public class RawTypeEnum<T> : RuntimeEnum where T : System.IEquatable<T>
    {
        [SerializeField]
        public T Value;

		public override bool Equals(object other)
		{
            var otherEnum = other as RawTypeEnum<T>;
            if (otherEnum == null) return false;

            return otherEnum.Value.Equals(Value);
		}

		public override int GetHashCode()
		{
            return Value.GetHashCode();
		}
	}

    public abstract class StringEnum : RawTypeEnum<string>
    { }

    public abstract class IntegerEnum : RawTypeEnum<int>
    { }

    public abstract class FloatEnum : RawTypeEnum<float>
    { }
}
