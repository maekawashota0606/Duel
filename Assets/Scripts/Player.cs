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
        // TODO:�R���g���[���[�Ή�
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
    /// �}�E�X���t���b�N�����ۂ̊p�x�����߂�(����H)
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private float CalAngleZ(Vector3 origin, Vector3 end)
    {
        Vector3 distance = end - origin;
        float z = Mathf.Atan2(distance.x, distance.y) * Mathf.Rad2Deg;

        // �}�C�i�X�̊p�x������
        z += 360;
        z %= 360;
        Debug.Log(z);

        return z;
    }

}
