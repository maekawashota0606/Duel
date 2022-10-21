using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Vector2 startPos;
    [SerializeField]private float speed;
    public bool start = true;
    public bool Action = false;
    public bool Avoidance = false;
    private float time = 0f;
    private float Action_time = 1f;
    [SerializeField] private GameObject attackPrefab;
    private bool mouseDrag = false;
    private GameObject attackObj = null;
    private float attackRad = 0f;
    private float attackTime = 0.5f;
    private float attackStartTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        start = true;
        Action = false;
        Avoidance = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            //�}�E�X�̓����Ɣ��Ε����ɔ��˂����
            if (Input.GetMouseButtonDown(0))
            {
                //�}�E�X���������|�W�V��������
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //�}�E�X�𗣂����|�W�V��������
                Vector2 endPos = Input.mousePosition;
                //(���������W - ���������W)��-�P���|���Ă���
                Vector2 startDirection = -1 * (endPos - startPos).normalized;
                this.rigid2D.AddForce(startDirection * speed);

                start = false;
                Action = true;
                Avoidance = true;
            }
        }
        else
        {
            time += Time.deltaTime;
        }

        //�U�������
        if (Action_time <= time)
        {
            //�}�E�X�h���b�O�J�n
            if (Input.GetMouseButtonDown(0))
            {
                //�}�E�X�̃h���b�O�J�n�|�W�V��������
                this.startPos = Input.mousePosition;
                mouseDrag = true;
            }
            //�}�E�X�h���b�O�I��
            else if(Input.GetMouseButtonUp(0))
            {
                mouseDrag = false;
            }

            //�}�E�X�h���b�O��
            //�}�E�X�̓����Ɣ��Ε����ɔ��˂����
            if (mouseDrag)
            {
                //�U��
                if (Input.GetKeyDown(KeyCode.A) && Action)
                {
                    //�U���͂܂�������
                    Debug.Log("Attack");
                    //�}�E�X�̃h���b�O�I���|�W�V��������
                    Vector2 endPos = Input.mousePosition;

                    //�h���b�O�J�n�|�W�V�����ƏI���|�W�V�����Ԃ̊p�x���v�Z
                    attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                    //�U���I�u�W�F�N�g�̐����ʒu���v�Z
                    Vector2 objPos = gameObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(attackRad * Mathf.Deg2Rad);

                    //�U���v���n�u�����ɁA�I�u�W�F�N�g�𐶐��A
                    attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, attackRad));

                    attackStartTime = time;
                    Action = false;
                }
                //���
                else if (Input.GetKeyDown(KeyCode.Space) && Avoidance)
                {
                    Debug.Log("Avoidance");
                    //�}�E�X�̃h���b�O�I���|�W�V��������
                    Vector2 endPos = Input.mousePosition;

                    //(�h���b�O�I���|�W�V���� - �h���b�O�J�n�|�W�V����)��-�P���|���Ă���
                    Vector2 startDirection = -1 * (endPos - startPos).normalized;
                    this.rigid2D.AddForce(startDirection * speed);

                    Avoidance = false;
                }
            }

            // �U����
            if (attackObj is not null)
            {
                if (time - attackStartTime > attackTime)
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
                }
            }
        }

        //���S��~
        if (this.rigid2D.velocity.magnitude < 2.0f && this.rigid2D.velocity.magnitude != 0)
        {
            Debug.Log("�~�܂�");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            start = true;
        }

    }

    //���Ԍo�߂Ō���
    void FixedUpdate()
    {
        this.rigid2D.velocity *= 0.99f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("wall"))
        {
            Debug.Log("�ǂɓ�������");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���C���[�ɓ�������");
            this.rigid2D.velocity *= 0.9f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "Attck_Test_P2")
        {
            Debug.Log("�U������������");
            this.rigid2D.velocity *= 0.8f;
        }
    }
}
