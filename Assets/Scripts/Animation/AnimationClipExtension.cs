using UnityEngine;
using System;
using System.Collections.Generic;

public static class AnimationClipExtension
{
    public static AnimationClip AnimateTo(this Vector3 from, Vector3 to, float duration, DynamicAnimationType type,
                                          AnimationCurveType curve = AnimationCurveType.Linear,
                                          WrapMode wrapMode = WrapMode.Once)
    {
        if (from == to) return null;
        var aCurve = new AnimationCurve[3];

        switch (curve)
        {
            case AnimationCurveType.EaseInOut:
                aCurve[0] = AnimationCurve.EaseInOut(0, from.x, duration, to.x);
                aCurve[1] = AnimationCurve.EaseInOut(0, from.y, duration, to.y);
                aCurve[2] = AnimationCurve.EaseInOut(0, from.z, duration, to.z);
                break;
            default:
                aCurve[0] = AnimationCurve.Linear(0, from.x, duration, to.x);
                aCurve[1] = AnimationCurve.Linear(0, from.y, duration, to.y);
                aCurve[2] = AnimationCurve.Linear(0, from.z, duration, to.z);
                break;
        }

        var clip = new AnimationClip();
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".x", aCurve[0]);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".y", aCurve[1]);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".z", aCurve[2]);

        clip.wrapMode = wrapMode;
        clip.name = type.ToString();

        var onAnimationEnd = new AnimationEvent
        {
            time = duration,
            functionName = DynamicAnimation.OnDynamicAnimationEndCallback,
            intParameter = (int)type

        };
        clip.AddEvent(onAnimationEnd);

        var onAnimationBegin = new AnimationEvent
        {
            time = 0,
            functionName = DynamicAnimation.OnDynamicAnimationBeginCallback,
            intParameter = (int)type

        };
        clip.AddEvent(onAnimationBegin);

        return clip;
    }

    public static AnimationClip AnimatePositionTo(this Vector3 from, Vector3 to, float duration,
                                             AnimationCurveType curve = AnimationCurveType.Linear,
                                             WrapMode wrapMode = WrapMode.Once)
    {
        return AnimateTo(from, to, duration, DynamicAnimationType.Position, curve, wrapMode);
    }

    public static AnimationClip AnimateScaleTo(this Vector3 from, Vector3 to, float duration,
                                         AnimationCurveType curve = AnimationCurveType.Linear,
                                         WrapMode wrapMode = WrapMode.Once)
    {
        return AnimateTo(from, to, duration, DynamicAnimationType.Scale, curve, wrapMode);
    }

    public static AnimationClip AnimateEulerAnglesTo(this Vector3 from, Vector3 to, float duration,
                                         AnimationCurveType curve = AnimationCurveType.Linear,
                                         WrapMode wrapMode = WrapMode.Once)
    {
        return AnimateTo(from, to, duration, DynamicAnimationType.EulerAngles, curve, wrapMode);
    }

    public static AnimationClip AnimateJumpTo(this Vector3 from, Vector3 to, float duration, float jumpHeight = 0,
                                             AnimationCurveType curve = AnimationCurveType.Linear,
                                             WrapMode wrapMode = WrapMode.Once)
    {
        if (from == to) return null;
        var aCurve = new AnimationCurve[3];

        switch (curve)
        {
            case AnimationCurveType.EaseInOut:
                aCurve[0] = AnimationCurve.EaseInOut(0, from.x, duration, to.x);
                aCurve[1] = AnimationCurve.EaseInOut(0, 0, 1, 1);
                aCurve[2] = AnimationCurve.EaseInOut(0, from.z, duration, to.z);
                break;
            default:
                aCurve[0] = AnimationCurve.Linear(0, from.x, duration, to.x);
                aCurve[1] = AnimationCurve.Linear(0, 0, 1, 1);
                aCurve[2] = AnimationCurve.Linear(0, from.z, duration, to.z);
                break;
        }

        float steps = (to - from).magnitude * 5;
        float jumpH = jumpHeight == 0 ? (to - from).magnitude / 2 : jumpHeight;

        var traj = new BallisticTrajectory(from, to, jumpH);
        var keyFrames = new AnimationCurve();
        for (float t = 0; t<1; t+= 1/steps )
            keyFrames.AddKey(new Keyframe(duration * aCurve[1].Evaluate(t), 
                                          traj.Evaluate(aCurve[1].Evaluate(t))));
        keyFrames.AddKey(new Keyframe(duration, to.y));

        var clip = new AnimationClip();
        var type = DynamicAnimationType.Jump;

        clip.SetCurve("", typeof(Transform), type.property.Name() + ".x", aCurve[0]);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".y", keyFrames);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".z", aCurve[2]);

        clip.wrapMode = wrapMode;
        clip.name = type.ToString();

        var onAnimationEnd = new AnimationEvent
        {
            time = duration,
            functionName = DynamicAnimation.OnDynamicAnimationEndCallback,
            intParameter = (int)type

        };
        clip.AddEvent(onAnimationEnd);

        var onAnimationBegin = new AnimationEvent
        {
            time = 0,
            functionName = DynamicAnimation.OnDynamicAnimationBeginCallback,
            intParameter = (int)type

        };
        clip.AddEvent(onAnimationBegin);

        return clip;
    }

    public static AnimationClip AnimateRotationTo(this Quaternion from, Quaternion to, float duration,
                                             AnimationCurveType curve = AnimationCurveType.Linear,
                                             WrapMode wrapMode = WrapMode.Once)
    {
        if (from == to) return null;
        var aCurve = new AnimationCurve[4];
        aCurve[0] = new AnimationCurve();
        aCurve[1] = new AnimationCurve();
        aCurve[2] = new AnimationCurve();
        aCurve[3] = new AnimationCurve();

        float steps = Quaternion.Angle(from, to);

        for (float t = 0; t < 1; t += 1 / steps)
        {
            var q = Quaternion.Slerp(from, to, t);
            aCurve[0].AddKey(new Keyframe(t * duration, q.x));
            aCurve[1].AddKey(new Keyframe(t * duration, q.y));
            aCurve[2].AddKey(new Keyframe(t * duration, q.z));
            aCurve[3].AddKey(new Keyframe(t * duration, q.w));
        }
        aCurve[0].AddKey(new Keyframe(duration, to.x));
        aCurve[1].AddKey(new Keyframe(duration, to.y));
        aCurve[2].AddKey(new Keyframe(duration, to.z));
        aCurve[3].AddKey(new Keyframe(duration, to.w));

        var clip = new AnimationClip();
        var type = DynamicAnimationType.Rotation;

        clip.SetCurve("", typeof(Transform), type.property.Name() + ".x", aCurve[0]);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".y", aCurve[1]);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".z", aCurve[2]);
        clip.SetCurve("", typeof(Transform), type.property.Name() + ".w", aCurve[3]);

        clip.EnsureQuaternionContinuity();
        clip.wrapMode = wrapMode;
        clip.name = type.ToString();

        var onAnimationEnd = new AnimationEvent
        {
            time = duration,
            functionName = DynamicAnimation.OnDynamicAnimationEndCallback,
            intParameter = (int)type

        };
        clip.AddEvent(onAnimationEnd);

        var onAnimationBegin = new AnimationEvent
        {
            time = 0,
            functionName = DynamicAnimation.OnDynamicAnimationBeginCallback,
            intParameter = (int)type

        };
        clip.AddEvent(onAnimationBegin);

        return clip;
    }

}