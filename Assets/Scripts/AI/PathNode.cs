using UnityEngine;
using System;

public class PathNode : IComparable<PathNode>
{
    public Vector2Int position { get; private set; }
    public float height { get; private set; }
    public float weight { get; private set; }

    public bool isWalkable { get { return weight != 0; } }

    public PathNode parent;

    public PathNode(Vector2Int pos, float height, float weight) : this(pos.x, pos.y, height, weight)
    {}

    public PathNode(int x, int y, float height, float weight)
    {
        position = new Vector2Int(x, y);
        this.height = height;
        this.weight = weight;

        startCost = 0;
        endCost = 0;
    }

    public float startCost;
    public float endCost;
    public float fullCost { get { return startCost + endCost; } }

    public float DistanceTo(PathNode other)
    {
        int dstX = Mathf.Abs(position.x - other.position.x);
        int dstY = Mathf.Abs(position.y - other.position.y);

        // 10 ???
        // 14 ???

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public int CompareTo(PathNode other)
    {
        float delta = fullCost - other.fullCost;
        if (Mathf.Approximately(delta, 0))
            delta = endCost - other.endCost;
        return (int)Mathf.Sign(delta);
    }
}
