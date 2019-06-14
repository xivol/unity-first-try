using UnityEngine;
using System;

public static class PointUtils {
    public static int DistanceTo(this Vector2Int v, Vector2Int other)
    {
        return Math.Abs(v.x - other.x) + Math.Abs(v.y - other.y);
    }

    public static bool IsAligned(this Vector2Int v, Vector2Int p1, Vector2Int p2)
    {
        return IsHorisontalyAligned(v,p1,p2) || IsVerticalyAligned(v,p1,p2);
    }

    public static bool IsHorisontalyAligned(this Vector2Int v, Vector2Int p1, Vector2Int p2)
    {
        return p1.x == p2.x && p1.x == v.x;
    }

    public static bool IsVerticalyAligned(this Vector2Int v, Vector2Int p1, Vector2Int p2)
    {
        return p1.y == p2.y && p1.y == v.y;
    }
}
