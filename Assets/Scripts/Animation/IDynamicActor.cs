using UnityEngine;
using Xivol.Events;

public interface IDynamicActor
{
    DynamicAnimation dynAnimator { get; }
    void OnAnimationDidEnd(object sender, EventArgs<int> animationType);
    void OnAnimationDidBegin(object sender, EventArgs<int> animationType);
}

public static class DynamicActorExtension
{
    public static void JumpTo(this IDynamicActor actor, Vector3 newPosition, float duration, float jumpHeight = 0,
                             AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        actor.dynAnimator.JumpTo(newPosition, duration, jumpHeight, curveType);
    }

    public static void LookAt(this IDynamicActor actor, Vector3 targetPosition, float duration, bool excludeHeight = true,
                              AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        actor.dynAnimator.LookAt(targetPosition, duration, excludeHeight, curveType);
    }

    public static void TurnTo(this IDynamicActor actor, Vector3 lookDirection, float duration, 
                              AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        actor.dynAnimator.TurnTo(lookDirection, duration, curveType);
    }

    public static void MoveTo(this IDynamicActor actor, Vector3 newPosition, float duration,
                              AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        actor.dynAnimator.MoveTo(newPosition, duration, curveType);
    }
}
