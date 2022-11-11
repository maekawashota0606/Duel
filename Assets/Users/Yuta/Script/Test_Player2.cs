using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コントローラー操作
public class Test_Player2 : MonoBehaviour
{
    //攻撃オブジェクトのプレハブ
    [SerializeField] private GameObject attackPrefab;
    //コマオブジェクトのプレハブ
    [SerializeField] private GameObject comaObject;
    Rigidbody2D rigid2D;
    Vector2 startPos;

    private float speed;　                 //コマのスピード
    public static float start_time_P2 = 0f;   //スタートするまでの時間
    public static float time = 0f;         //攻撃専用のタイマー
    private float Action_time = 1f;        //攻撃・回避が出来るまでの時間

    public static bool start = true;       //スタート出来るかのフラグ
    public static bool Action = false;     //攻撃出来るかのフラグ
    public static bool Avoidance = false;  //回避出来るかのフラグ
    public static bool Finish_P2 = false;  //完全停止したかのフラグ
    //スタートの準備が出来たかのフラグ
    public static bool Start_Time_P2 = false;

    private float h;
    private float v;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = comaObject.GetComponent<Rigidbody2D>();
        
        //初期化
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
        //ジョイスティックの左右
        h = Input.GetAxis("Horizontal_Test");
        //ジョイスティックの上下
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

            //hかvに値が入ってたら
            if (h != 0 || v != 0)
            {
                //Start_Time_P2をtrueにする
                Start_Time_P2 = true;
            }

            //初期ポジション
            this.startPos = new Vector2(0, 0);

            //３秒たったら
            if (start_time_P2 >= 3f)
            {
                //hかvに値が入ってたら
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
            Finish_P2 = false;
            Start_Time_P2 = false;
        }

        //攻撃＆回避
        if (Action_time <= time)
        {
            //初期ポジション
            this.startPos = new Vector2(0, 0);

            //攻撃           Ps4の○ボタン↓
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
            //回避             Ps4の×ボタン↓
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

        //vとhを２乗とルートで計算（スティックの傾きを計算）
        float d = Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(v, 2));

        //0～0.25だったら
        if (d >= 0f && d <= 0.25f)
        {
            speed = 25000f;
        }
        //0.25～0.5だったら
        if (d >= 0.25f && d <= 0.5f)
        {
            speed = 50000f;
        }
        //0.5～0.75だったら
        if (d >= 0.5f && d <= 0.75f)
        {
            speed = 75000f;
        }
        //0.75～1だったら
        if (d >= 0.75f && d <= 1f)
        {
            speed = 100000f;
        }
        this.rigid2D.AddForce(startDirection * speed);
    }
}
