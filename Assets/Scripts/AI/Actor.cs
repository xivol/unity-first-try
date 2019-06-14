using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : DynamicObject
{
    public int movementRange;
    public int jumpHeight;

    public Pathfinder pathfinder 
    { 
        get 
        {
            StickToGrid();
            return new Pathfinder(this, levelController);
        }
    }

	public float PathTroughCost(GridObject other) 
    {
        var a = other as Actor;
        return other is Actor ? 1 : 0;
    }

    public virtual bool MoveTo(Vector2Int target)
    {
        var path = pathfinder.FindPath(target);
        if (path == null) return false;

        StartCoroutine(Traverse(path));
        return true;
    }
}
