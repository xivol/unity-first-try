using UnityEngine;

public class BallisticTrajectory
{
    public static float heightMul = 6f;
    public static float heightSignMul = 0.25f;

    public static Vector3 Velocity(Vector3 start, Vector3 end, float startAngle)
    {
        //https://answers.unity.com/questions/148399/shooting-a-cannonball.html

        Vector3 direction = end - start;
        float height = direction.y;
        direction.y = 0;

        float distance = direction.magnitude;
        direction.y = distance * Mathf.Tan(startAngle);
        distance += height / Mathf.Tan(startAngle);

        return direction.normalized * Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * startAngle));
    }

    public static Vector3 Velocity(Vector3 start, Vector3 end) 
    {
        Vector3 direction = end - start;
        float height = heightMul * Mathf.Sqrt(Mathf.Abs(direction.y)) *
                       (1 - heightSignMul + Mathf.Sign(direction.y) * heightSignMul );
        direction.y = 0;

        float distance = direction.magnitude;
        float angle = Mathf.Atan2(height, distance);
        direction.y = distance * Mathf.Tan(angle);
        //distance += height / Mathf.Tan(angle);
        return direction.normalized * Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));
    }

    public static Vector3 Lerp(Vector3 start, Vector3 end, float t, float jumpHeight = 1)
    {
        float x = Mathf.Lerp(start.x, end.x, t);
        float z = Mathf.Lerp(start.z, end.z, t);

        // parabola params
        float top_y = Mathf.Max(start.y, end.y) + jumpHeight;
        float top_t = 0.5f; 
        float dy = start.y - end.y;
        if (!Mathf.Approximately(dy,0)) 
        {
            top_t = - top_y + start.y + Mathf.Sqrt((top_y - start.y) * (top_y -  end.y));
            top_t /= dy;
        }
        float width = (top_y - start.y) / top_t / top_t;

        float y = - width * (t - top_t) * (t - top_t) + top_y;

        return new Vector3(x, y, z);
    }

    // 
    Vector3 start, end;

    public BallisticTrajectory(Vector3 start, Vector3 end, float jumpHeight = 1)
    {
        // parabola params
        topY = Mathf.Max(start.y, end.y) + jumpHeight;
        topT = 0.5f;
        float dy = start.y - end.y;
        if (!Mathf.Approximately(dy, 0))
        {
            topT = -topY + start.y + Mathf.Sqrt((topY - start.y) * (topY - end.y));
            topT /= dy;
        }
        width = (topY - start.y) / topT / topT;

        this.start = start;
        this.end = end;
    }

    //
    float topT, topY, width;

    public float Evaluate(float t)
    {
        return -width * (t - topT) * (t - topT) + topY;
    }

    public Vector3 EvaluatePosition(float t) 
    {
        float x = Mathf.Lerp(start.x, end.x, t);
        float z = Mathf.Lerp(start.z, end.z, t);
        float y = Evaluate(t);

        return new Vector3(x, y, z);
    }
}