using UnityEngine;
using System;
using System.Collections;
using Xivol.Events;

[RequireComponent(typeof(Animation))]
public class DynamicAnimation : Core<DynamicAnimation>
{
    const float turnTime = 0.25f;
    const float heightThreshold = 0.5f;

    protected Animation _animation;

    public event EventHandler<EventArgs<int>> animationDidEnd;
    public event EventHandler<EventArgs<int>> animationDidBegin;

    protected override void Awake()
    {
        base.Awake();
        _animation = GetComponent<Animation>();
    }

    #region DynamicAnimation
    public void MoveTo(Vector3 newPosition, float duration,
                              AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        AnimationClip clip = transform.localPosition.AnimatePositionTo(newPosition, duration, curveType);
        if (clip == null)
        {
            Debug.LogWarning("Nothing to animate!");
            return;
        }

        clip.legacy = true;

        _animation.AddClip(clip, clip.name);
        _animation.PlayQueued(clip.name);
    }

    public void TurnTo(Vector3 lookDirection, float duration, AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        var target = Quaternion.LookRotation(lookDirection);

        AnimationClip clip = transform.rotation.AnimateRotationTo(target, duration, curveType);
        if (clip == null)
        {
            Debug.LogWarning("Nothing to animate!");
            return;
        }

        clip.legacy = true;

        _animation.AddClip(clip, clip.name);
        _animation.PlayQueued(clip.name);
    }

    public void LookAt(Vector3 targetPosition, float duration,
                              bool excludeHeight = true,
                              AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        var currentPosition = transform.position;
        if (excludeHeight)
        {
            targetPosition.y = 0;
            currentPosition.y = 0;
        }
        var direction = (targetPosition - currentPosition);

        TurnTo(direction, duration, curveType);
    }

    public void JumpTo(Vector3 newPosition, float duration, float jumpHeight = 0,
                              AnimationCurveType curveType = AnimationCurveType.Linear)
    {
        AnimationClip clip = transform.localPosition.AnimateJumpTo(newPosition, duration, jumpHeight, curveType);
        if (clip == null)
        {
            Debug.LogWarning("Nothing to animate!");
            return;
        }
        clip.legacy = true;

        _animation.AddClip(clip, clip.name);
        _animation.PlayQueued(clip.name);
    }

    public virtual IEnumerator AnimateTraversalTo(Vector3 newPosition, float speed, TraversalType type = TraversalType.Walk)
    {
        float distance = (newPosition - transform.localPosition).magnitude;
        float time = distance / speed;

        LookAt(newPosition, turnTime);
        if (Mathf.Abs((transform.localPosition - newPosition).y) > heightThreshold)
            JumpTo(newPosition, time);
        else
            MoveTo(newPosition, time);

        yield return new WaitForSeconds(turnTime + time);
    }

    //public IEnumerable WaitForAnimationToEnd(){
    //    do {
    //        yield return null;
    //    } while (_animation.isPlaying);
    //}
    #endregion

    #region AnimationEvents
    public static string OnDynamicAnimationBeginCallback = "OnDynamicAnimationBegin";
    public virtual void OnDynamicAnimationBegin(int animationType)
    {
        if (animationDidBegin != null)
            animationDidBegin(this, new EventArgs<int>(animationType));
        print("Animation Begin " + (DynamicAnimationProperty)animationType);
    }

    public static string OnDynamicAnimationEndCallback = "OnDynamicAnimationEnd";
    public virtual void OnDynamicAnimationEnd(int animationType)
    {
        if (animationDidEnd != null)
            animationDidEnd(this, new EventArgs<int>(animationType));
        print(name + " Animation End " + (DynamicAnimationProperty)animationType);
    }
    #endregion
}
