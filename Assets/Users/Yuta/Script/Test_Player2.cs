using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player2 : MonoBehaviour
{
    //�R���g���[���[����

    Rigidbody2D rigid2D;
    Vector2 startPos;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private GameObject comaObject;

    private float speed;
    public static float start_time = 0f;
    public static float time = 0f;
    private float Action_time = 1f;

    public static bool start = true;
    public static bool Action = false;
    public static bool Avoidance = false;
    public static bool Ready_P2 = false;
    public static bool Start_Time_P2 = false;

    private float h;
    private float v;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = comaObject.GetComponent<Rigidbody2D>();
        start = true;
        Action = false;
        Avoidance = false;
        Ready_P2 = false;

        start_time = 0f;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal_Test");
        v = Input.GetAxis("Vertical_Test");

        if (start)
        {
            this.startPos = new Vector2(0, 0);

            if (Start_Time_P2 && Test_Player1.Start_Time_P1)
            {
                Debug.Log("���f�B�[");
                start_time += Time.deltaTime;
            }

            if (h != 0 || v != 0)
            {
                Start_Time_P2 = true;
            }

            if (start_time >= 3f)
            {
                if (h != 0 || v != 0)
                {
                    //�X�e�B�b�N�̃|�W�V��������
                    Vector2 endPos = new Vector2(h, v);
                    //�X�e�B�b�N�̓����Ɣ��Ε����Ɉړ�
                    KomaMove(endPos);

                    start = false;
                    Action = true;
                    Avoidance = true;
                }
            }
        }
        else
        {
            time += Time.deltaTime;
            Ready_P2 = false;
            Start_Time_P2 = false;
        }

        //�U�������
        if (Action_time <= time)
        {
            this.startPos = new Vector2(0, 0);

            //�U��
            if (Input.GetButtonDown("Attack_Test") && Action)
            {
                //�}�E�X�̃h���b�O�I���|�W�V��������
                Vector2 endPos = new Vector2(h, v);

                //�h���b�O�J�n�|�W�V�����ƏI���|�W�V�����Ԃ̊p�x���v�Z
                Test_Koma2.attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                //�U���I�u�W�F�N�g�̐����ʒu���v�Z
                Vector2 objPos = comaObject.transform.position;
                objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(Test_Koma2.attackRad * Mathf.Deg2Rad);
                objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(Test_Koma2.attackRad * Mathf.Deg2Rad);

                //�U���v���n�u�����ɁA�I�u�W�F�N�g�𐶐��A
                Test_Koma2.attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, Test_Koma2.attackRad));

                Test_Koma2.attackStartTime = time;
                Action = false;
            }
            //���
            if (Input.GetButtonDown("Avoidance_Test") && Avoidance)
            {
                //�}�E�X�̃h���b�O�I���|�W�V��������
                Vector2 endPos = new Vector2(h, v);

                KomaMove(endPos);

                Avoidance = false;
            }
        }
    }

    //����́A�l��Ԃ��Ȃ��ϐ�
    void KomaMove(Vector2 endPos)
    {
        //(���������W - ���������W)��-�P���|���Ă���
        Vector2 startDirection = -1 * (endPos - startPos).normalized;

        //v��h���Q��ƃ��[�g�Ōv�Z
        float d = Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(v, 2));

        if (d >= 0f && d <= 0.25f)
        {
            speed = 25000f;
        }
        if (d >= 0.25f && d <= 0.5f)
        {
            speed = 50000f;
        }
        if (d >= 0.5f && d <= 0.75f)
        {
            speed = 75000f;
        }
        if (d >= 0.75f && d <= 1f)
        {
            speed = 100000f;
        }
        this.rigid2D.AddForce(startDirection * speed);
    }
}
