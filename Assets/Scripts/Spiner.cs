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
    private bool _isHitWall = false;

    private void Start()
    {
        // test
        _direction = _direction.normalized;
    }


    private void Update()
    {
        if (CheckSpeed())
        {
            Move();
            Deceleration();
        }
        else
            return;
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
    /// 壁に当たった場合に呼ばれる
    /// </summary>
    /// <param name="isRef"></param>
    public void HitWall()
    {
        _isHitWall = true;
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
    }

    /// <summary>
    /// 壁に当たった時などの反射
    /// </summary>
    public void Reflect(Vector3 normal)
    {
        ChangeDirection(Vector3.Reflect(_direction, normal));

        // 減速？
    }

    public void ChangeDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }
}
