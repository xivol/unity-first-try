using UnityEngine;
using System;

[Serializable]
public class Grid
{
    protected Size m_size;
    public Size size { get { return m_size; } }

    protected SizeF m_cellSize;
    public SizeF cellSize { get{ return m_cellSize; } }

    public int cells { get { return size.width * size.height; } }
    public int vertices { get { return (size.width + 1) * (size.height + 1); } }

    protected float[,] m_heightMap;

    public Grid(Size gridSize, SizeF cellSize)
    {
        m_size = gridSize;
        m_cellSize = cellSize;
    }

    public Grid(float[,] heightMap, SizeF cellSize)
    {
        m_size = new Size(heightMap.GetLength(0), heightMap.GetLength(1));
        m_heightMap = heightMap;
        m_cellSize = cellSize;
    }

    public float Height(int x, int y)
    {
        if (m_heightMap != null)
            return m_heightMap[x, y];

        float scaleY = Mathf.Min(size.width * cellSize.width, size.height * cellSize.height);
        return Mathf.Round(Mathf.PerlinNoise((float)x / size.width, (float)y / size.height) * scaleY);
        //return 0.5f * scaleY;
    }

    public float Height(Vector2Int cell)
    {
        return Height(cell.x, cell.y);
    }

    public Vector2Int CellForPoint(float x, float z)
    {
        return new Vector2Int((int)Mathf.Floor(x / cellSize.width), (int)Mathf.Floor(z / cellSize.height));
    }

    public Vector2Int CellForPoint(Vector3 point)
    {
        return CellForPoint(point.x, point.z);
    }

    public Vector3 PointForCell(int i, int j)
    {
        float x = (i + 0.5f) * cellSize.width;
        float z = (j + 0.5f) * cellSize.width;
        return new Vector3(x, Height(i, j), z);
    }

    public Vector3 PointForCell(Vector2Int cell)
    {
        return PointForCell(cell.x, cell.y);
    }

    public Vector3[] GetAllPositions()
    {
        Vector3[] result = new Vector3[size.count];
        for (int i = 0; i < size.width; ++i)
            for (int j = 0; j < size.height; ++j)
                result[i * size.width + j] = PointForCell(i, j);
        return result;
    }
}

[System.Serializable]
public class EditableGrid : Grid 
{
    public EditableGrid() : base(Size.unit, SizeF.unit) 
    {
        m_heightMap = new float[1, 1];
    }

    public EditableGrid(Size gridSize, SizeF cellSize) : base(gridSize, cellSize)
    {
        m_heightMap = new float[gridSize.width, gridSize.height];
    }

    public EditableGrid(float[,] heightMap, SizeF cellSize) : base(heightMap, cellSize)
    {}

    public void SetGridSize(Size gridSize) 
    {
        m_size = gridSize;
    }

    public void SetHeight(int x, int y, float value)
    {
        m_heightMap[x, y] = value;
    }
}