using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private double _speed = 0;
    [SerializeField]
    private double _decelerationRatio = 0;
    [SerializeField]
    private Vector3 _direction = Vector3.zero;
    [SerializeField, Header("停止とみなす速度")]
    private float _stopThreshold = 0;
    [SerializeField]
    public  float length = 120;


    public void MyUpdate()
    {
        if (CheckSpeed())
        {
            // 最終的に移動前に壁判定したい
            Move();
            //
            if(MyPhysics.Instance.IsHitSpinerAndSpiner)
        }
    }

    /// <summary>
    /// 移動方向を指定
    /// </summary>
    /// <param name="dir"></param>
    public void SetDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }

    /// <summary>
    /// 一定の速度以下なら停止させる
    /// </summary>
    /// <returns></returns>
    private bool CheckSpeed()
    {
        if (_speed < _stopThreshold)
        {
            _speed = 0;
            Debug.Log($"{gameObject.name}停止");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 減速計算
    /// </summary>
    private void Deceleration()
    {
        _speed -= _speed * (1 - _decelerationRatio) * Time.deltaTime;
    }

    /// <summary>
    /// スピードとベクトルに応じて移動
    /// </summary>
    private void Move()
    {
        transform.Translate(_direction * (float)_speed * Time.deltaTime);
        Deceleration();
    }

    /// <summary>
    /// 壁に当たった時などの反射
    /// </summary>
    public void ReflectWall(Vector3 normal)
    {
        // 法線を参照して反射
        ChangeDirection(Vector3.Reflect(_direction, normal));

        // 減速
    }

    /// <summary>
    /// 他のコマに当たった時の反射
    /// </summary>
    public void ReflectSpiner()
    {
        // とりあえず反転
        ChangeDirection(_direction * -1);

        // 減速
    }

    public void ChangeDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }
}
