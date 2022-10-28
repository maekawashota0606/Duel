using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_dummy : MonoBehaviour
{
    Rigidbody2D rigid2D;
    [SerializeField] private GameObject attackPrefab;
    public static GameObject attackObj = null;
    public static float attackRad = 0f;
    private float attackTime = 0.5f;
    public static float attackStartTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �U���� �ړ�
        if (attackObj is not null)
        {
            if (Test_Player.time - attackStartTime > attackTime)
            {
                // �U�����Ԃ��o�߂�����A�U���I�u�W�F�N�g��j��
                Destroy(attackObj);
                attackObj = null;
            }
            else
            {
                // �U���I�u�W�F�N�g���v���C���[�I�u�W�F�N�g�ɍ��킹�Ĉړ�
                Vector2 objPos = gameObject.transform.position;
                objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(attackRad * Mathf.Deg2Rad);
                objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(attackRad * Mathf.Deg2Rad);
                attackObj.transform.position = objPos;
                //attackObj.transform.position = GetAttackObjPos();
            }
        }

        //���S��~
        if (this.rigid2D.velocity.magnitude < 2.0f && this.rigid2D.velocity.magnitude != 0)
        {
            Debug.Log("�~�܂�(dummy)");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Test_Player.start = true;
            Test_Player.Action = false;
            Test_Player.Avoidance = false;
        }
    }

    //���Ԍo�߂Ō���
    void FixedUpdate()
    {
        this.rigid2D.velocity *= 0.99f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Debug.Log("�ǂɓ�������(dummy)");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���C���[�ɓ�������(dummy)");
            this.rigid2D.velocity *= 0.9f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "Attck_Test_P1(Clone)")
        {
            Debug.Log("�U������������(dummy)");
            this.rigid2D.velocity *= 0.8f;
        }
    }
}
