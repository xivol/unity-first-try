using UnityEngine;
using System;

public class GridMeshGenerator
{
    [Flags]
    enum QuadFaceDirection { Up = 1, Left = 2, Back = 4, Right = 8, Forward = 16, Down = 32 }

    public Mesh mesh { get; private set; }

    readonly Vector3[] vertices;
    readonly int[] indices;
    readonly Vector2[] uv;

    readonly Grid grid;

    readonly int quadsPerCell;
    readonly QuadFaceDirection directions;

    const int VerticesPerQuad = 4;
    const int IndicesPerQuad = 6;

    public GridMeshGenerator(Grid grid, int quadsPerCell = 3)
    {
        this.grid = grid;
        this.quadsPerCell = Math.Max(1, Math.Min(quadsPerCell, 6));
        this.directions = (QuadFaceDirection)((1 << this.quadsPerCell) - 1);

        this.vertices = new Vector3[grid.cells * this.quadsPerCell * VerticesPerQuad];
        this.indices = new int[grid.cells * this.quadsPerCell * IndicesPerQuad];
        this.uv = new Vector2[vertices.Length];

        GenerateMesh();
        //SizeF scale = new SizeF(1f / gridSize.width * cellSize.width, 1f / gridSize.height * cellSize.height);
        //float scaleY = Mathf.Min(1f / scale.width, 1f / scale.height);
    }

    private void AppendQuad(int cellX, int cellY, QuadFaceDirection dir, int vIndex, int iIndex)
    {

        float x1 = cellX * grid.cellSize.width;
        float x2 = (cellX + 1) * grid.cellSize.width;
        float z1 = cellY * grid.cellSize.height;
        float z2 = (cellY + 1) * grid.cellSize.height;
        float y1 = grid.Height(cellX, cellY);
        float y2 = 0;

        switch (dir)
        {
            case QuadFaceDirection.Up:
                vertices[vIndex + 0] = new Vector3(x1, y1, z1);
                vertices[vIndex + 1] = new Vector3(x2, y1, z1);
                vertices[vIndex + 2] = new Vector3(x1, y1, z2);
                vertices[vIndex + 3] = new Vector3(x2, y1, z2);
                break;
            case QuadFaceDirection.Left:
                if ((directions & QuadFaceDirection.Right) != QuadFaceDirection.Right && cellX != 0)
                    y2 = grid.Height(cellX - 1, cellY);
                vertices[vIndex + 0] = new Vector3(x1, y2, z1);
                vertices[vIndex + 1] = new Vector3(x1, y1, z1);
                vertices[vIndex + 2] = new Vector3(x1, y2, z2);
                vertices[vIndex + 3] = new Vector3(x1, y1, z2);
                break;
            case QuadFaceDirection.Back:
                if ((directions & QuadFaceDirection.Forward) != QuadFaceDirection.Forward && cellY != 0)
                    y2 = grid.Height(cellX, cellY - 1);
                vertices[vIndex + 0] = new Vector3(x1, y2, z1);
                vertices[vIndex + 1] = new Vector3(x2, y2, z1);
                vertices[vIndex + 2] = new Vector3(x1, y1, z1);
                vertices[vIndex + 3] = new Vector3(x2, y1, z1);
                break;
            case QuadFaceDirection.Right:
                if ((directions & QuadFaceDirection.Left) != QuadFaceDirection.Left && cellX != grid.size.width - 1 )
                    y2 = grid.Height(cellX + 1, cellY);
                vertices[vIndex + 0] = new Vector3(x2, y1, z1);
                vertices[vIndex + 1] = new Vector3(x2, y2, z1);
                vertices[vIndex + 2] = new Vector3(x2, y1, z2);
                vertices[vIndex + 3] = new Vector3(x2, y2, z2);
                break;
            case QuadFaceDirection.Forward:
                if ((directions & QuadFaceDirection.Back) != QuadFaceDirection.Back && cellY != grid.size.height - 1)
                    y2 = grid.Height(cellX, cellY + 1);
                vertices[vIndex + 0] = new Vector3(x1, y1, z2);
                vertices[vIndex + 1] = new Vector3(x2, y1, z2);
                vertices[vIndex + 2] = new Vector3(x1, y2, z2);
                vertices[vIndex + 3] = new Vector3(x2, y2, z2);
                break;
            case QuadFaceDirection.Down:
                vertices[vIndex + 0] = new Vector3(x2, y2, z1);
                vertices[vIndex + 1] = new Vector3(x1, y2, z1);
                vertices[vIndex + 2] = new Vector3(x2, y2, z2);
                vertices[vIndex + 3] = new Vector3(x1, y2, z2);
                break;
        }

        indices[iIndex + 0] = vIndex;
        indices[iIndex + 1] = vIndex + 2;
        indices[iIndex + 2] = vIndex + 1;

        indices[iIndex + 3] = vIndex + 2;
        indices[iIndex + 4] = vIndex + 3;
        indices[iIndex + 5] = vIndex + 1;

        uv[vIndex + 0] = new Vector2(0, 0);
        uv[vIndex + 1] = new Vector2(1, 0);
        uv[vIndex + 2] = new Vector2(0, 1);
        uv[vIndex + 3] = new Vector2(1, 1);
    }

    private void GenerateMesh() 
    {
        mesh = new Mesh();
        for (int i = 0; i < grid.size.width; ++i)
            for (int j = 0; j < grid.size.height; ++j)
            {
                // Quads
                int vrt = (i * grid.size.width + j) * VerticesPerQuad * quadsPerCell;
                // Triangles
                int ind = (i * grid.size.width + j) * IndicesPerQuad * quadsPerCell;

                for (int q = 0; q < quadsPerCell; ++q) {
                    AppendQuad(i, j,(QuadFaceDirection)(1<<q), vrt, ind);
                    vrt += VerticesPerQuad;
                    ind += IndicesPerQuad;
                }
            }

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        mesh.name = "mainGridMesh";
    }

}
