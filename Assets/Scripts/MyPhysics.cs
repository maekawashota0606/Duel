using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : SingletonMonoBehaviour<MyPhysics>
{
    [SerializeField]
    private Spiner _spiner_1 = null;
    [SerializeField]
    private Spiner _spiner_2 = null;
    [SerializeField]
    public List<Wall> _walls = new List<Wall>();

    
    private void Update()
    {
        IsHitSpinerAndSpiner();
    }

    public void IsHitSpinerAndSpiner()
    {
        IsHitCircleAndCircle(_spiner_1.gameObject.transform.position, _spiner_2.gameObject.transform.position, _spiner_1.length, _spiner_2.length);
    }


    public static bool IsHitCircleAndCircle(Vector3 v1, Vector3 v2, float r1, float r2)
    {
        float a = v1.x - v2.x;
        float b = v1.y - v2.y;
        float c = a * a + b * b;
        float d = (r1 + r2) * (r1 + r2);

        if (d >= c)
        {
            Debug.Log("hit");
            return true;
        }
        else
            return false;
    }
}
