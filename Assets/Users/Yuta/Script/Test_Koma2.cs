using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Koma2 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    //攻撃オブジェクトのプレハブ
    [SerializeField] private GameObject attackPrefab;
    public static GameObject attackObj = null;
    //攻撃時の角度
    public static float attackRad = 0f;
    //攻撃出来る時間
    private float attackTime = 0.5f;
    //攻撃を始めた時間
    public static float attackStartTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃中 移動
        if (attackObj is not null)
        {
            //攻撃出来る時間を上回ったら
            if (Test_Player2.time - attackStartTime > attackTime)
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

        //完全停止
        if (this.rigid2D.velocity.magnitude < 2.0f && this.rigid2D.velocity.magnitude != 0)
        {
            Debug.Log("完全停止(P2)");
            Test_Player2.Finish_P2 = true;

            //P1が完全停止したら中の処理を行う
            if (Test_Player1.Finish_P1)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Test_Player2.start = true;
                Test_Player2.Action = false;
                Test_Player2.Avoidance = false;

                Test_Player2.time = 0f;
                Test_Player2.start_time = 0f;
            }
        }

        if (Attack.Offset_P2)
        {
            Debug.Log("呼ばれた");
            this.rigid2D.velocity *= 0.9f;
            Attack.Offset_P2 = false;
        }
    }

    //時間経過で減速
    void FixedUpdate()
    {
        this.rigid2D.velocity *= 0.99f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Debug.Log("壁に当たった(dummy)");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに当たった(dummy)");
            this.rigid2D.velocity *= 0.9f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "Attck_Test_P1(Clone)")
        {
            Debug.Log("攻撃が当たった(dummy)");
            this.rigid2D.velocity *= 0.8f;
        }
    }
}
