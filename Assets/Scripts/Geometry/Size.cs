using UnityEngine;
using System;

[System.Serializable]
public struct SizeF
{
    public static SizeF unit = new SizeF(1, 1);

    public float width, height;

    public SizeF(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

    public float square 
    {
        get { return width * height; }
    }

    public static explicit operator SizeF(Vector2 vec)
    {
        return new SizeF(vec.x, vec.y);
    }

    public static explicit operator Vector2(SizeF s)
    {
        return new Vector2(s.width, s.height);
    }
}

[System.Serializable]
public struct Size
{
    public static Size unit = new Size(1, 1);

    public int width, height;

    public Size(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public int count
    {
        get { return width * height; }
    }

    public static explicit operator Size(Vector2 vec)
    {
        return new Size(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y));
    }

    public static explicit operator Vector2(Size s)
    {
        return new Vector2(s.width, s.height);
    }
}