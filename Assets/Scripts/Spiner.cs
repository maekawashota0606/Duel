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
    [SerializeField, Header("��~�Ƃ݂Ȃ����x")]
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
    /// �ړ��������w��
    /// </summary>
    /// <param name="dir"></param>
    public void SetDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }
    
    /// <summary>
    /// �ǂɓ��������ꍇ�ɌĂ΂��
    /// </summary>
    /// <param name="isRef"></param>
    public void HitWall()
    {
        _isHitWall = true;
    }

    /// <summary>
    /// ���̑��x�ȉ��Ȃ��~������
    /// </summary>
    /// <returns></returns>
    private bool CheckSpeed()
    {
        if (_speed < _stopThreshold)
        {
            _speed = 0;
            Debug.Log($"{gameObject.name}��~");
            return false;
        }

        return true;
    }

    /// <summary>
    /// �����v�Z
    /// </summary>
    private void Deceleration()
    {
        _speed -= _speed * (1 - _decelerationRatio) * Time.deltaTime;
    }

    /// <summary>
    /// �X�s�[�h�ƃx�N�g���ɉ����Ĉړ�
    /// </summary>
    private void Move()
    {
        transform.Translate(_direction * (float)_speed * Time.deltaTime);
    }

    /// <summary>
    /// �ǂɓ����������Ȃǂ̔���
    /// </summary>
    public void Reflect(Vector3 normal)
    {
        ChangeDirection(Vector3.Reflect(_direction, normal));

        // �����H
    }

    public void ChangeDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }
}
