using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�}�E�X����
public class Test_Player1 : MonoBehaviour
{
    //�U���I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject attackPrefab;
    //�R�}�I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject comaObject;
    Rigidbody2D rigid2D;
    Vector2 startPos;

    public float speed;�@                 //�R�}�̃X�s�[�h
    public static float start_time = 0f;�@//�X�^�[�g����܂ł̎���
    public static float time = 0f;        //�U����p�̃^�C�}�[
    private float Action_time = 1f;       //�U���E������o����܂ł̎���

    public static bool start = true;�@�@�@//�X�^�[�g�o���邩�̃t���O
    public static bool Action = false;    //�U���o���邩�̃t���O
    public static bool Avoidance = false; //����o���邩�̃t���O
    private bool mouseDrag = false;       //�}�E�X���h���b�N�����̃t���O
    public static bool Finish_P1 = false;  //���S��~�������̃t���O
    //�X�^�[�g�̏������o�������̃t���O
    public static bool Start_Time_P1 = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = comaObject.GetComponent<Rigidbody2D>();

        //������
        start = true;
        Action = false;
        Avoidance = false;
        Finish_P1 = false;
        start_time = 0f;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (Start_Time_P1 && Test_Player2.Start_Time_P2)
            {
                Debug.Log("���f�B�[");
                start_time += Time.deltaTime;
            }

            //�}�E�X�̓����Ɣ��Ε����ɔ��˂����
            if (InputProvider.Instance.GetFire1Down())
            {
                //�}�E�X���������|�W�V��������
                this.startPos = Input.mousePosition;

                //Start_Time_P1��true�ɂ���
                Start_Time_P1 = true;
            }
            //�R�b��������
            else if (start_time >= 3f)
            {
                //�}�E�X�𗣂����|�W�V��������
                Vector2 endPos = Input.mousePosition;
                //�}�E�X�̓����Ɣ��Ε����Ɉړ�
                KomaMove(endPos);

                start = false;
                Action = true;
                Avoidance = true;
            }
        }
        else
        {
            time += Time.deltaTime;
            Finish_P1 = false;
            Start_Time_P1 = false;
        }

        //�U�������
        if (Action_time <= time)
        {
            //�}�E�X�h���b�O�J�n
            if (InputProvider.Instance.GetFire1Down())
            {
                //�}�E�X�̃h���b�O�J�n�|�W�V��������
                this.startPos = Input.mousePosition;
                mouseDrag = true;
            }
            //�}�E�X�h���b�O�I��
            else if (InputProvider.Instance.GetFire1Up())
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
                    //�}�E�X�̃h���b�O�I���|�W�V��������
                    Vector2 endPos = Input.mousePosition;

                    //�h���b�O�J�n�|�W�V�����ƏI���|�W�V�����Ԃ̊p�x���v�Z
                    Test_Koma1.attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                    //�U���I�u�W�F�N�g�̐����ʒu���v�Z
                    Vector2 objPos = comaObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(Test_Koma1.attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(Test_Koma1.attackRad * Mathf.Deg2Rad);

                    //�U���v���n�u�����ɁA�I�u�W�F�N�g�𐶐��A
                    Test_Koma1.attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, Test_Koma1.attackRad));

                    Test_Koma1.attackStartTime = time;
                    Action = false;
                }
                //���
                else if (Input.GetKeyDown(KeyCode.Space) && Avoidance)
                {
                    Debug.Log("���");
                    //�}�E�X�̃h���b�O�I���|�W�V��������
                    Vector2 endPos = Input.mousePosition;

                    //(�h���b�O�I���|�W�V���� - �h���b�O�J�n�|�W�V����)��-�P���|���Ă���
                    KomaMove(endPos);

                    Avoidance = false;
                }
            }
        }
    }

    //����́A�l��Ԃ��Ȃ��ϐ�
    void KomaMove(Vector2 endPos)
    {
        //(���������W - ���������W)��-�P���|���Ă���
        Vector2 startDirection = -1 * (endPos - startPos).normalized;
        this.rigid2D.AddForce(startDirection * speed);
    }
}