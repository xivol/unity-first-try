using UnityEngine;
using System;

public enum AnimationCurveType
{
    // TODO: https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
    Linear = 1,
    EaseInOut = 2
}

public enum DynamicAnimationProperty
{
    Position = 0x01,
    Rotation = 0x02,
    EulerAngles = 0x04,
    Scale = 0x08
}

public static class DynamicAnimationPropertyExtension
{
    public static string Name(this DynamicAnimationProperty type)
    {
        switch (type)
        {
            case DynamicAnimationProperty.Position:
                return "localPosition";
            case DynamicAnimationProperty.Rotation:
                return "localRotation";
            case DynamicAnimationProperty.Scale:
                return "localScale";
            case DynamicAnimationProperty.EulerAngles:
                return "localEulerAngles";
        }
        return null;
    }
}

public enum DynamicAnimationAction
{
    Default = 0x00FF,
    Jump = 0x01FF,
}

public struct DynamicAnimationType
{
    public DynamicAnimationProperty property;
    public DynamicAnimationAction action;

    public DynamicAnimationType(DynamicAnimationProperty prop, DynamicAnimationAction act)
    {
        property = prop;
        action = act;
    }

    public override string ToString()
    {
        return property.ToString() + action.ToString();
    }

    //
    public static DynamicAnimationType Jump =
        new DynamicAnimationType(DynamicAnimationProperty.Position, DynamicAnimationAction.Jump);
    //
    public static DynamicAnimationType Position =
        new DynamicAnimationType(DynamicAnimationProperty.Position, DynamicAnimationAction.Default);
    //
    public static DynamicAnimationType Rotation =
        new DynamicAnimationType(DynamicAnimationProperty.Rotation, DynamicAnimationAction.Default);
    //
    public static DynamicAnimationType EulerAngles =
        new DynamicAnimationType(DynamicAnimationProperty.EulerAngles, DynamicAnimationAction.Default);
    //
    public static DynamicAnimationType Scale =
        new DynamicAnimationType(DynamicAnimationProperty.Scale, DynamicAnimationAction.Default);
    //
    public static implicit operator int(DynamicAnimationType t)
    {
        return (int)t.property & (int)t.action;
    }
    //
    public static explicit operator DynamicAnimationType(int t)
    {
        DynamicAnimationType type;
        type.property = (DynamicAnimationProperty)(t & 0xFF);
        type.action = (DynamicAnimationAction)((t | 0xFF) & 0xFFFF);
        return type;
    }
}
