using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class old_CharacterController : GridObject
{
    private Rigidbody rb;
    private Grid grid;

    protected override void Start () {
        base.Start();
        rb = GetComponent<Rigidbody>();
        grid = LevelManager.Instance.Level.grid;
	}

    private bool grounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Update () {
        if (!grounded) return;

        var gridTarget = grid.CellForPoint(LevelManager.Instance.Cursor.transform.position);
        var cell =  grid.CellForPoint(transform.position);

        if (cell != gridTarget) {
            grounded = false;

            var direction = gridTarget - cell;
            var nextCell = cell;
            if (Mathf.Abs(direction.x)> Mathf.Abs(direction.y)) {
                nextCell += Vector2Int.right * (int)Mathf.Sign(direction.x);
                rb.velocity = BallisticTrajectory.Velocity(transform.position, grid.PointForCell(nextCell));

            } else {
                nextCell += Vector2Int.up * (int)Mathf.Sign(direction.y);
                rb.velocity = BallisticTrajectory.Velocity(transform.position, grid.PointForCell(nextCell));
            }
        }
	}
}

