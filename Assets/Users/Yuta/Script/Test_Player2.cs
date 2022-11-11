using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�R���g���[���[����
public class Test_Player2 : MonoBehaviour
{
    //�U���I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject attackPrefab;
    //�R�}�I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject comaObject;
    Rigidbody2D rigid2D;
    Vector2 startPos;

    private float speed;�@                 //�R�}�̃X�s�[�h
    public static float start_time_P2 = 0f;   //�X�^�[�g����܂ł̎���
    public static float time = 0f;         //�U����p�̃^�C�}�[
    private float Action_time = 1f;        //�U���E������o����܂ł̎���

    public static bool start = true;       //�X�^�[�g�o���邩�̃t���O
    public static bool Action = false;     //�U���o���邩�̃t���O
    public static bool Avoidance = false;  //����o���邩�̃t���O
    public static bool Finish_P2 = false;  //���S��~�������̃t���O
    //�X�^�[�g�̏������o�������̃t���O
    public static bool Start_Time_P2 = false;

    private float h;
    private float v;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = comaObject.GetComponent<Rigidbody2D>();
        
        //������
        start = true;
        Action = false;
        Avoidance = false;
        Finish_P2 = false;
        start_time_P2 = 0f;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //�W���C�X�e�B�b�N�̍��E
        h = Input.GetAxis("Horizontal_Test");
        //�W���C�X�e�B�b�N�̏㉺
        v = Input.GetAxis("Vertical_Test");

        if (start)
        {
            Start_Time_P2 = true;

            if (Start_Time_P2)
            {
                if (Time.frameCount % 1000 == 0)
                {
                    start_time_P2 += 1f;
                    Debug.Log(start_time_P2);
                }
            }

            //h��v�ɒl�������Ă���
            if (h != 0 || v != 0)
            {
                //Start_Time_P2��true�ɂ���
                Start_Time_P2 = true;
            }

            //�����|�W�V����
            this.startPos = new Vector2(0, 0);

            //�R�b��������
            if (start_time_P2 >= 3f)
            {
                //h��v�ɒl�������Ă���
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
            Finish_P2 = false;
            Start_Time_P2 = false;
        }

        //�U�������
        if (Action_time <= time)
        {
            //�����|�W�V����
            this.startPos = new Vector2(0, 0);

            //�U��           Ps4�́��{�^����
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
            //���             Ps4�́~�{�^����
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

        //v��h���Q��ƃ��[�g�Ōv�Z�i�X�e�B�b�N�̌X�����v�Z�j
        float d = Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(v, 2));

        //0�`0.25��������
        if (d >= 0f && d <= 0.25f)
        {
            speed = 25000f;
        }
        //0.25�`0.5��������
        if (d >= 0.25f && d <= 0.5f)
        {
            speed = 50000f;
        }
        //0.5�`0.75��������
        if (d >= 0.5f && d <= 0.75f)
        {
            speed = 75000f;
        }
        //0.75�`1��������
        if (d >= 0.75f && d <= 1f)
        {
            speed = 100000f;
        }
        this.rigid2D.AddForce(startDirection * speed);
    }
}
