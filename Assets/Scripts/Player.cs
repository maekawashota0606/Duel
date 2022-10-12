using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Spiner _mySpiner = null;
    private Vector3 origin = Vector3.zero;
    private Vector3 end = Vector3.zero;


    private void Update()
    {
        // TODO:コントローラー対応
        if (InputProvider.Instance.GetFire1Down())
            origin = Input.mousePosition;
        else if (InputProvider.Instance.GetFire1Up())
        {
            end = Input.mousePosition;
            CalAngleZ(origin, end);
            _mySpiner.ChangeDirection(origin - end);
        }
    }


    /// <summary>
    /// マウスをフリックした際の角度を求める(いる？)
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private float CalAngleZ(Vector3 origin, Vector3 end)
    {
        Vector3 distance = end - origin;
        float z = Mathf.Atan2(distance.x, distance.y) * Mathf.Rad2Deg;

        // マイナスの角度を解決
        z += 360;
        z %= 360;
        Debug.Log(z);

        return z;
    }

}
