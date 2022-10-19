using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public static void SpinersCollision(List<Spiner> spiners)
    {
        // ëçìñÇΩÇËÇ≈åvéZ
        for(int i = 0; i < spiners.Count; i++)
        {
            for (int j = 1; j < spiners.Count; j++)
            {
                Spiner s1 = spiners[i];
                Spiner s2 = spiners[j];

                if (MyPhysics.IsHitCircleAndCircle(s1.gameObject.transform.position, s2.gameObject.transform.position, s1.length, s2.length))
                {
                    s1.ReflectSpiner();
                    s2.ReflectSpiner();
                }
            }
        }
    }
}
