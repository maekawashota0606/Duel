using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マウス操作
public class Test_Player1 : MonoBehaviour
{
    //攻撃オブジェクトのプレハブ
    [SerializeField] private GameObject attackPrefab;
    //コマオブジェクトのプレハブ
    [SerializeField] private GameObject comaObject;
    Rigidbody2D rigid2D;
    Vector2 startPos;

    public float speed;　                 //コマのスピード
    public static float start_time = 0f;　//スタートするまでの時間
    public static float time = 0f;        //攻撃専用のタイマー
    private float Action_time = 1f;       //攻撃・回避が出来るまでの時間

    public static bool start = true;　　　//スタート出来るかのフラグ
    public static bool Action = false;    //攻撃出来るかのフラグ
    public static bool Avoidance = false; //回避出来るかのフラグ
    private bool mouseDrag = false;       //マウスがドラック中かのフラグ
    public static bool Finish_P1 = false;  //完全停止したかのフラグ
    //スタートの準備が出来たかのフラグ
    public static bool Start_Time_P1 = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = comaObject.GetComponent<Rigidbody2D>();

        //初期化
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
                Debug.Log("レディー");
                start_time += Time.deltaTime;
            }

            //マウスの動きと反対方向に発射される
            if (InputProvider.Instance.GetFire1Down())
            {
                //マウスを押したポジションを代入
                this.startPos = Input.mousePosition;

                //Start_Time_P1をtrueにする
                Start_Time_P1 = true;
            }
            //３秒たったら
            else if (start_time >= 3f)
            {
                //マウスを離したポジションを代入
                Vector2 endPos = Input.mousePosition;
                //マウスの動きと反対方向に移動
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

        //攻撃＆回避
        if (Action_time <= time)
        {
            //マウスドラッグ開始
            if (InputProvider.Instance.GetFire1Down())
            {
                //マウスのドラッグ開始ポジションを代入
                this.startPos = Input.mousePosition;
                mouseDrag = true;
            }
            //マウスドラッグ終了
            else if (InputProvider.Instance.GetFire1Up())
            {
                mouseDrag = false;
            }

            //マウスドラッグ中
            //マウスの動きと反対方向に発射される
            if (mouseDrag)
            {
                //攻撃
                if (Input.GetKeyDown(KeyCode.A) && Action)
                {
                    //マウスのドラッグ終了ポジションを代入
                    Vector2 endPos = Input.mousePosition;

                    //ドラッグ開始ポジションと終了ポジション間の角度を計算
                    Test_Koma1.attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                    //攻撃オブジェクトの生成位置を計算
                    Vector2 objPos = comaObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(Test_Koma1.attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(Test_Koma1.attackRad * Mathf.Deg2Rad);

                    //攻撃プレハブを元に、オブジェクトを生成、
                    Test_Koma1.attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, Test_Koma1.attackRad));

                    Test_Koma1.attackStartTime = time;
                    Action = false;
                }
                //回避
                else if (Input.GetKeyDown(KeyCode.Space) && Avoidance)
                {
                    Debug.Log("回避");
                    //マウスのドラッグ終了ポジションを代入
                    Vector2 endPos = Input.mousePosition;

                    //(ドラッグ終了ポジション - ドラッグ開始ポジション)に-１を掛けている
                    KomaMove(endPos);

                    Avoidance = false;
                }
            }
        }
    }

    //これは、値を返さない変数
    void KomaMove(Vector2 endPos)
    {
        //(離した座標 - 押した座標)に-１を掛けている
        Vector2 startDirection = -1 * (endPos - startPos).normalized;
        this.rigid2D.AddForce(startDirection * speed);
    }
}