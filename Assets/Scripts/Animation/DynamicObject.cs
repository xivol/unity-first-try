using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Xivol.Events;

public enum TraversalType { None, Walk, Hop, Fly, Teleport }

[RequireComponent(typeof(DynamicAnimation))]
public class DynamicObject : GridObject, IDynamicActor
{
    public float speed = 2; // (m/s)
    public event EventHandler traversalDidStart;
    public event EventHandler traversalDidEnd;

    protected DynamicAnimation _dynAnimator;
    public DynamicAnimation dynAnimator
    {
        get { return _dynAnimator; }
    }

    public virtual void OnAnimationDidBegin(object sender, EventArgs<int> animationType) {}

    public virtual void OnAnimationDidEnd(object sender, EventArgs<int> animationType)
    {
        var type = (DynamicAnimationType)animationType.Value;

        switch (type.property)
        {
            case DynamicAnimationProperty.Position:
                StickToGrid();
                break;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _dynAnimator = GetComponent<DynamicAnimation>();
    }

	protected override void OnEnable()
	{
        base.OnEnable();
        _dynAnimator.animationDidEnd += OnAnimationDidEnd;
        _dynAnimator.animationDidEnd += OnAnimationDidBegin;
	}

	protected override void OnDisable()
	{
        base.OnDisable();
        _dynAnimator.animationDidEnd -= OnAnimationDidEnd;
        _dynAnimator.animationDidEnd -= OnAnimationDidBegin;
	}

    protected virtual IEnumerator Traverse(LinkedList<Vector2Int> path)
    {
        if (traversalDidStart != null) 
            traversalDidStart(this, EventArgs.Empty);
        
        foreach (var step in path)
        {
            Vector3 target = levelController.grid.PointForCell(step);

            yield return _dynAnimator.AnimateTraversalTo(target, speed);
        }

        if(traversalDidEnd != null)
            traversalDidEnd(this, EventArgs.Empty);
    }


}
