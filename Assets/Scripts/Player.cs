using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _charaNum = 0;
    private Spiner _mySpiner = null;
    private Vector3 origin = Vector3.zero;
    private Vector3 end = Vector3.zero;
    private bool _isReady = false;


    #region getter, setter
    public int GetCharaNum()
    {
        return _charaNum;
    }

    public void SetCharaNum(int num)
    {
        _charaNum = num;
    }

    public Spiner GetSpiner()
    {
        return _mySpiner;
    }

    public void SetSpiner(Spiner spiner)
    {
        _mySpiner = spiner;
    }

    public bool GetIsReady()
    {
        return _isReady;
    }

    public void SetIsReady(bool isReady)
    {
        _isReady = isReady;
    }
    #endregion


    public void MyUpdate()
    {

    }

    public bool Flick()
    {
        bool isCompleted = false;

        // TODO:コントローラー対応
        if (InputProvider.Instance.GetFire1Down())
            origin = Input.mousePosition;
        else if (InputProvider.Instance.GetFire1Up())
        {
            end = Input.mousePosition;
            //CalAngleZ(origin, end);
            _mySpiner.ChangeDirection(origin - end);

            isCompleted = true;
        }

        return isCompleted;
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
