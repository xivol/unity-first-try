using UnityEngine;
using System.Collections;

public enum PathType {
    Movement, Attack
}

public static class PathRenderTypeExtension {
    public static Color Color(this PathType pathRenderType)
    {
        switch (pathRenderType) 
        {
            case PathType.Movement: return new Color(0, 0, 1, 0.5f);  
            case PathType.Attack: return new Color(1, 0, 0, 0.5f);
        }
        return UnityEngine.Color.magenta;
    }
}

[RequireComponent(typeof(MeshFilter))]
public class PathRenderer : Core<PathRenderer>
{
    public Material material;
    public float elevation = 0.1f;

    Matrix4x4[] _cellTransforms;
    MaterialPropertyBlock _cellProperties;
    Mesh _mesh;

    protected override void Awake()
    {
        base.Awake();
        _cellProperties = new MaterialPropertyBlock();
        _mesh = GetComponent<MeshFilter>().mesh;
    }

	protected override void Start()
	{
        base.Start();
        transform.localPosition = Vector3.zero;
        _cellTransforms = GetTransforms(LevelManager.Instance.Level.grid);

        material.enableInstancing = true;
	}

	protected virtual void Update()
    {
        Graphics.DrawMeshInstanced(_mesh, 0, material, _cellTransforms, _cellTransforms.Length, _cellProperties);
    }

    public void SetupMovement(GameObject actor)
    {
        //Debug.Log(actor);
        if (actor != null)
        {
            SetPathing(actor.GetComponent<Actor>().pathfinder);
        }
    }

    public void SetPathing(Pathfinder pathfinder, PathType pathType = PathType.Movement)
    {
        _cellProperties.SetFloatArray("_Enabled", pathfinder.ReachabilityList());

        material.SetColor("_Color", pathType.Color());

        //float[] enabled = new float[_cellTransforms.Length];
        //for (int i = 0; i < enabled.Length; ++i)
        //    enabled[i] = 1;
        //Vector4[] colors = new Vector4[_cellTransforms.Length];
        //for (int i = 0; i < colors.Length; ++i)
        //    colors[i] = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.5f);
        //_cellProperties.SetVectorArray("_Color", colors);
    }

    private Matrix4x4[] GetTransforms(Grid grid)
    {
        Vector3[] positions = grid.GetAllPositions();
        Matrix4x4[] result = new Matrix4x4[positions.Length];
        for (int i = 0; i < result.Length; ++i)
            result[i] =  Matrix4x4.Translate(positions[i] + Vector3.up * elevation) * 
                                  transform.localToWorldMatrix;
        return result;
    }

}
