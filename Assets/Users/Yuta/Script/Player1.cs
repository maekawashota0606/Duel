using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Vector2 startPos;
    [SerializeField] private float speed;
    public bool start = true;
    public bool Avoidance = true;
    private float time = 0f;
    //������\�܂ł̎���
    private float Avoidance_time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        start = true;�@//�X�^�[�g�o���邩
        Avoidance = false;�@//����o���邩
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ��̏���
        if (start)
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
                Avoidance = true;
            }
        }

        //����������o����܂ł̎��Ԃ��v��
        if (Avoidance)
        {
            time += Time.deltaTime;
        }

        //����̏���
        if (Avoidance && Avoidance_time <= time)
        {
            //�}�E�X�̓����Ɣ��Ε����ɔ��˂����
            if (Input.GetMouseButtonDown(0))
            {
                //�}�E�X���������|�W�V��������
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //�}�E�X�𗣂����|�W�V��������
                Vector2 endPos = Input.mousePosition;

                //(���������W - ���������W)��-�P���|���Ă���
                Vector2 startDirection = -1 * (endPos - startPos).normalized;
                this.rigid2D.AddForce(startDirection * speed);

                Avoidance = false;
            }
        }

        //�L���������ȉ��ɂȂ����������m���A�L�������~
        if (this.rigid2D.velocity.magnitude < 2.0f && this.rigid2D.velocity.magnitude != 0)
        {
            Debug.Log("�~�܂�");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            start = true;
            Avoidance = true;

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
            //Debug.Log("�ǂɓ�������");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("�v���C���[�ɓ�������");
            this.rigid2D.velocity *= 0.9f;
        }
    }
}
