using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player2 : MonoBehaviour
{
    //コントローラー操作

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
                Debug.Log("レディー");
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
                    //スティックのポジションを代入
                    Vector2 endPos = new Vector2(h, v);
                    //スティックの動きと反対方向に移動
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

        //攻撃＆回避
        if (Action_time <= time)
        {
            this.startPos = new Vector2(0, 0);

            //攻撃
            if (Input.GetButtonDown("Attack_Test") && Action)
            {
                //マウスのドラッグ終了ポジションを代入
                Vector2 endPos = new Vector2(h, v);

                //ドラッグ開始ポジションと終了ポジション間の角度を計算
                Test_Koma2.attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                //攻撃オブジェクトの生成位置を計算
                Vector2 objPos = comaObject.transform.position;
                objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(Test_Koma2.attackRad * Mathf.Deg2Rad);
                objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(Test_Koma2.attackRad * Mathf.Deg2Rad);

                //攻撃プレハブを元に、オブジェクトを生成、
                Test_Koma2.attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, Test_Koma2.attackRad));

                Test_Koma2.attackStartTime = time;
                Action = false;
            }
            //回避
            if (Input.GetButtonDown("Avoidance_Test") && Avoidance)
            {
                //マウスのドラッグ終了ポジションを代入
                Vector2 endPos = new Vector2(h, v);

                KomaMove(endPos);

                Avoidance = false;
            }
        }
    }

    //これは、値を返さない変数
    void KomaMove(Vector2 endPos)
    {
        //(離した座標 - 押した座標)に-１を掛けている
        Vector2 startDirection = -1 * (endPos - startPos).normalized;

        //vとhを２乗とルートで計算
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
