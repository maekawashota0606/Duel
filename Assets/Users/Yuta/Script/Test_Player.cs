using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Vector2 startPos;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private GameObject comaObject;

    public float speed;
    public static float time = 0f;
    private float Action_time = 1f;

    public static bool start = true;
    public static bool Action = false;
    public static bool Avoidance = false;
    private bool mouseDrag = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = comaObject.GetComponent<Rigidbody2D>();
        start = true;
        Action = false;
        Avoidance = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            //�}�E�X�̓����Ɣ��Ε����ɔ��˂����
            //Input.GetMouseButtonDown(0)
            if (InputProvider.Instance.GetFire1Down())
            {
                //�}�E�X���������|�W�V��������
                this.startPos = Input.mousePosition;
            }
            //Input.GetMouseButtonUp(0)
            else if (InputProvider.Instance.GetFire1Up())
            {
                //�}�E�X�𗣂����|�W�V��������
                Vector2 endPos = Input.mousePosition;
                //�}�E�X�̓����Ɣ��Ε����Ɉړ�
                Test_KomaMove(endPos);

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
            else if (Input.GetMouseButtonUp(0))
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
                    Test_Koma.attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                    //�U���I�u�W�F�N�g�̐����ʒu���v�Z
                    Vector2 objPos = comaObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(Test_Koma.attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(Test_Koma.attackRad * Mathf.Deg2Rad);

                    //�U���v���n�u�����ɁA�I�u�W�F�N�g�𐶐��A
                    Test_Koma.attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, Test_Koma.attackRad));

                    Test_Koma.attackStartTime = time;
                    Action = false;
                }
                //���
                else if (Input.GetKeyDown(KeyCode.Space) && Avoidance)
                {
                    //�}�E�X�̃h���b�O�I���|�W�V��������
                    Vector2 endPos = Input.mousePosition;

                    //(�h���b�O�I���|�W�V���� - �h���b�O�J�n�|�W�V����)��-�P���|���Ă���
                    Test_KomaMove(endPos);

                    Avoidance = false;
                }
            }
        }
    }

    //����́A�l��Ԃ��Ȃ��ϐ�
    void Test_KomaMove(Vector2 endPos)
    {
        //(���������W - ���������W)��-�P���|���Ă���
        Vector2 startDirection = -1 * (endPos - startPos).normalized;
        this.rigid2D.AddForce(startDirection * speed);
    }
}