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
            //マウスの動きと反対方向に発射される
            if (Input.GetMouseButtonDown(0))
            {
                //マウスを押したポジションを代入
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //マウスを離したポジションを代入
                Vector2 endPos = Input.mousePosition;
                //(離した座標 - 押した座標)に-１を掛けている
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
            else if(Input.GetMouseButtonUp(0))
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
                    //攻撃はまだ未完成
                    Debug.Log("Attack");
                    //マウスのドラッグ終了ポジションを代入
                    Vector2 endPos = Input.mousePosition;

                    //ドラッグ開始ポジションと終了ポジション間の角度を計算
                    attackRad = Mathf.Repeat(Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * Mathf.Rad2Deg, 360);

                    //攻撃オブジェクトの生成位置を計算
                    Vector2 objPos = gameObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(attackRad * Mathf.Deg2Rad);

                    //攻撃プレハブを元に、オブジェクトを生成、
                    attackObj = Instantiate(attackPrefab, objPos, Quaternion.Euler(0.0f, 0.0f, attackRad));

                    attackStartTime = time;
                    Action = false;
                }
                //回避
                else if (Input.GetKeyDown(KeyCode.Space) && Avoidance)
                {
                    Debug.Log("Avoidance");
                    //マウスのドラッグ終了ポジションを代入
                    Vector2 endPos = Input.mousePosition;

                    //(ドラッグ終了ポジション - ドラッグ開始ポジション)に-１を掛けている
                    Vector2 startDirection = -1 * (endPos - startPos).normalized;
                    this.rigid2D.AddForce(startDirection * speed);

                    Avoidance = false;
                }
            }

            // 攻撃中
            if (attackObj is not null)
            {
                if (time - attackStartTime > attackTime)
                {
                    // 攻撃時間が経過したら、攻撃オブジェクトを破壊
                    Destroy(attackObj);
                    attackObj = null;
                }
                else
                {
                    // 攻撃オブジェクトをプレイヤーオブジェクトに合わせて移動
                    Vector2 objPos = gameObject.transform.position;
                    objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(attackRad * Mathf.Deg2Rad);
                    objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(attackRad * Mathf.Deg2Rad);
                    attackObj.transform.position = objPos;
                }
            }
        }

        //完全停止
        if (this.rigid2D.velocity.magnitude < 2.0f && this.rigid2D.velocity.magnitude != 0)
        {
            Debug.Log("止まる");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            start = true;
        }

    }

    //時間経過で減速
    void FixedUpdate()
    {
        this.rigid2D.velocity *= 0.99f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("wall"))
        {
            Debug.Log("壁に当たった");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに当たった");
            this.rigid2D.velocity *= 0.9f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "Attck_Test_P2")
        {
            Debug.Log("攻撃が当たった");
            this.rigid2D.velocity *= 0.8f;
        }
    }
}
