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
            //マウスの動きと反対方向に発射される
            //Input.GetMouseButtonDown(0)
            if (InputProvider.Instance.GetFire1Down())
            {
                //マウスを押したポジションを代入
                this.startPos = Input.mousePosition;
            }
            //Input.GetMouseButtonUp(0)
            else if (InputProvider.Instance.GetFire1Up())
            {
                //マウスを離したポジションを代入
                Vector2 endPos = Input.mousePosition;
                //マウスの動きと反対方向に移動
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

        //攻撃＆回避
        if (Action_time <= time)
        {
            //マウスドラッグ開始
            if (Input.GetMouseButtonDown(0))
            {
                //マウスのドラッグ開始ポジションを代入
                this.startPos = Input.mousePosition;
                mouseDrag = true;
            }
            //マウスドラッグ終了
            else if (Input.GetMouseButtonUp(0))
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
                    Test_Koma.attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                    //攻撃オブジェクトの生成位置を計算
                    Vector2 objPos = comaObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(Test_Koma.attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(Test_Koma.attackRad * Mathf.Deg2Rad);

                    //攻撃プレハブを元に、オブジェクトを生成、
                    Test_Koma.attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, Test_Koma.attackRad));

                    Test_Koma.attackStartTime = time;
                    Action = false;
                }
                //回避
                else if (Input.GetKeyDown(KeyCode.Space) && Avoidance)
                {
                    //マウスのドラッグ終了ポジションを代入
                    Vector2 endPos = Input.mousePosition;

                    //(ドラッグ終了ポジション - ドラッグ開始ポジション)に-１を掛けている
                    Test_KomaMove(endPos);

                    Avoidance = false;
                }
            }
        }
    }

    //これは、値を返さない変数
    void Test_KomaMove(Vector2 endPos)
    {
        //(離した座標 - 押した座標)に-１を掛けている
        Vector2 startDirection = -1 * (endPos - startPos).normalized;
        this.rigid2D.AddForce(startDirection * speed);
    }
}