using UnityEngine;

public class MyPhysics : SingletonMonoBehaviour<MyPhysics>
{
    public static bool IsHitCircleAndCircle(Vector3 v1, Vector3 v2, float r1, float r2)
    {
        float a = v1.x - v2.x;
        float b = v1.y - v2.y;
        float c = a * a + b * b;
        float d = (r1 + r2) * (r1 + r2);

        if (d >= c)
        {
            //Debug.Log("hit");
            return true;
        }
        else
            return false;
    }
}
