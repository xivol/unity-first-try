using UnityEngine;
using UnityEditor.Animations;
using System;
using System.Collections.Generic;
using System.Collections;

public enum AnimationTrigger
{
    Run, Walk, Stand
}

[RequireComponent(typeof(Animator))]
public class DynamicAnimator : DynamicAnimation
{
    protected Animator _animator;
    [SerializeField]
    protected Dictionary<string, int> _triggers;
    [SerializeField, HideInInspector]
    protected StringList _animationTriggerNames;

    public InternalStringReference StandingTrigger;
    public InternalStringReference WalkingTrigger;
    public InternalStringReference JumpingTrigger;

    public float speed
    {
        get { return _animator.speed; }

        set { _animator.speed = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        //InitAnimatorStates();
    }

	protected virtual void OnValidate()
	{
        _animator = GetComponent<Animator>();
        InitAnimatorStates();
	}

	#region Triggers
	protected void InitAnimatorStates()
    {
        var ac = _animator.runtimeAnimatorController as AnimatorController;
        //foreach (var layer in _ac.layers)
        //{
        //    print(layer.name);
        //    foreach (var state in layer.stateMachine.states)
        //        print(state.state.name + ":" + state.state.nameHash);
        //}
        _triggers = new Dictionary<string, int>();
        _animationTriggerNames = StringList.CreateInstance<StringList>();
        foreach (var parameter in ac.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                try
                {
                    //var trig = (AnimationTrigger)Enum.Parse(typeof(AnimationTrigger), parameter.name);
                    _triggers[parameter.name] = parameter.nameHash;
                    _animationTriggerNames.Values.Add(parameter.name);
                }
                catch
                {
                    Debug.LogError("Parameter " + parameter.name + " is not in AnimationTrigger enum");
                }
            }
        }
        StandingTrigger.ReferencedList = _animationTriggerNames;
        WalkingTrigger.ReferencedList = _animationTriggerNames;
        JumpingTrigger.ReferencedList = _animationTriggerNames;
    }

    protected void TriggerAnimation(string trig)
    {
        _animator.SetTrigger(_triggers[trig]);
    }

    protected void ResetAnimation(string trig)
    {
        _animator.ResetTrigger(_triggers[trig]);
    }

    protected void ResetAllAnimationTriggers()
    {
        foreach (var trig in _animationTriggerNames.Values)
            _animator.ResetTrigger(_triggers[trig]);
    }
	#endregion

	public override IEnumerator AnimateTraversalTo(Vector3 newPosition, float speed, TraversalType type = TraversalType.Walk)
	{
        this.speed = speed;
        return base.AnimateTraversalTo(newPosition, speed, type);
	}

	#region AnimationEvents
	public override void OnDynamicAnimationBegin(int animationType)
    {
        ResetAllAnimationTriggers();
        var type = (DynamicAnimationType)animationType;
        switch (type.property)
        {
            case DynamicAnimationProperty.Position:
                switch (type.action)
                {
                    case DynamicAnimationAction.Default:
                        TriggerAnimation(WalkingTrigger.Value);
                        break;
                    case DynamicAnimationAction.Jump:
                        TriggerAnimation(JumpingTrigger.Value);
                        break;
                }
                break;
        }

        base.OnDynamicAnimationBegin(animationType);
    }

    public override void OnDynamicAnimationEnd(int animationType)
    {
        ResetAllAnimationTriggers();
        var type = (DynamicAnimationType)animationType;
        switch (type.property)
        {
            case DynamicAnimationProperty.Position:
                TriggerAnimation(StandingTrigger.Value);
                break;
        }

        base.OnDynamicAnimationEnd(animationType);
    }
    #endregion
}
