using System;
using UnityEngine;
using UnityEngine.Events;

public class GridObject : Core<GridObject>
{
    public Vector2Int position { get; protected set; }
    public Vector3 facing { get; protected set; }

    protected LevelController levelController;

    public GameObject Level; 
    //{ 
    //    get { return levelController.gameObject; }
    //    set
    //    {
    //        levelController = value.GetComponent<LevelController>();
    //    }
    //}

    public bool IsBlockingPath = true;

    [Serializable]
    public class SerializedEvent : UnityEvent<Vector2Int>
    { }

    public SerializedEvent SettledOnGrid;

    protected override void Start()
    {
        base.Start();
        levelController = Level.GetComponent<LevelController>();
        StickToGrid();
    }

    public void StickToGrid()
    {
        position = levelController.grid.CellForPoint(transform.position);
        var dir = (transform.forward);
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            facing = Vector3.right * Mathf.Sign(dir.x);
        else 
            facing = Vector3.forward * Mathf.Sign(dir.z);
        Place(position);
    }

    public virtual void Place(Vector2Int target) 
    {
        if (levelController.IsOccupied(position))
            levelController.obstacles[position.x, position.y] = null;
        
        position = target;

        if (!levelController.IsOccupied(target))
            levelController.obstacles[target.x, target.y] = this;
        else
            Debug.LogError("Attempt to occupy non empty cell: " + target.ToString());
        
        Settle();
    }

    public virtual void Settle() {
        transform.localPosition = levelController.grid.PointForCell(position);
        transform.localRotation = Quaternion.LookRotation(facing);
        SettledOnGrid.Invoke(position);
    }
}
