using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Koma1 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    [SerializeField] private GameObject attackPrefab;
    public static GameObject attackObj = null;
    public static float attackRad = 0f;
    private float attackTime = 0.5f;
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
            if (Test_Player1.time - attackStartTime > attackTime)
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
            Debug.Log("完全停止(P1)");
            Test_Player1.Ready_P1 = true;

            if (Test_Player2.Ready_P2)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Test_Player1.start = true;
                Test_Player1.Action = false;
                Test_Player1.Avoidance = false;

                Test_Player1.time = 0f;
                Test_Player1.start_time = 0f;
            }
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
            Debug.Log("壁に当たった(Koma)");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに当たった(Koma)");
            this.rigid2D.velocity *= 0.9f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "Attck_Test_P2(Clone)")
        {
            Debug.Log("攻撃が当たった(Koma)");
            this.rigid2D.velocity *= 0.8f;
        }
    }
}
