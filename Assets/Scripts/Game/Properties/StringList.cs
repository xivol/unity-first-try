using UnityEngine;
using System;

[CreateAssetMenu(menuName ="Properties/StringList")]
public class StringList : PropertyList<string>
{
    public static StringList LoadOrMake(string fileName)
    {
        return ScriptableObjectUtils.LoadOrMake<StringList>(PathTo(fileName));
    }
}


[Serializable]
public class StringReference
{
    public StringList ReferencedList;

    [SerializeField]
    protected int _value;

    public string Value 
    {
        get
        {
            return ReferencedList.Values[_value];
        }
        set
        {
            int i = ReferencedList.Values.IndexOf(value);
            if (i != -1) 
                _value = i;
            else
                throw new ArgumentException("No value " + value + " in StringList");
        }
    }

    public StringReference(StringList list, int index = 0) 
    {
        ReferencedList = list;
        _value = index;
    }
}

[Serializable]
public class InternalStringReference : StringReference
{
    public InternalStringReference(StringList list, int index = 0) : base(list, index) 
    {}
}