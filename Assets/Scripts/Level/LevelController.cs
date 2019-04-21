using UnityEngine;
using Xivol.Events;

[ExecuteInEditMode] 
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class LevelController : Core<LevelController>
{
    public Grid grid { get; private set; }

    public SizeF cellSize = new SizeF(1,1);
    public Size gridSize = new Size(10,10);
    public int sidesForCell = 3;

	protected override void OnEnable()
	{
        base.OnEnable();
        obstacles = new GridObject[gridSize.width,gridSize.height];
        grid = new Grid(gridSize, cellSize);
        RebuildGrid();
	}

    protected void RebuildGrid()
    {
        var mesh =  new GridMeshGenerator(grid, sidesForCell).mesh;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public GridObject[,] obstacles;

    public bool IsOccupied(Vector2Int cell)
    {
        return IsOccupied(cell.x, cell.y);
    }

    public bool IsOccupied(int x, int y)
    {
        if (obstacles[x, y] == null)
            return false;

        return obstacles[x, y].IsBlockingPath;
    }

    public float PathTroughCost(Vector2Int cell)
    {
        return PathTroughCost(cell.x, cell.y);
    }

    public float PathTroughCost(int x, int y)
    {
        if (x < 0 || x >= grid.size.width ||
            y < 0 || y >= grid.size.height)
            return 0;

        return IsOccupied(x, y) ? 0 : 1;
    }

    public float[,] Map()
    {
        float[,] map = new float[grid.size.width, grid.size.height];
        for (int i = 0; i<grid.size.width; ++i)
            for (int j = 0; j<grid.size.height; ++j)
        {
                map[i, j] = PathTroughCost(i, j);
        }
        return map;
    }

}
