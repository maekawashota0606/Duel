using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Vector2 startPos;
    [SerializeField] private float speed;
    public bool start = true;
    public bool Avoidance = true;
    private float time = 0f;
    //回避が可能までの時間
    private float Avoidance_time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        start = true;　//スタート出来るか
        Avoidance = false;　//回避出来るか
    }

    // Update is called once per frame
    void Update()
    {
        //移動の処理
        if (start)
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
                Avoidance = true;
            }
        }

        //回避が発動出来るまでの時間を計測
        if (Avoidance)
        {
            time += Time.deltaTime;
        }

        //回避の処理
        if (Avoidance && Avoidance_time <= time)
        {
            //マウスの動きと反対方向に発射される
            if (Input.GetMouseButtonDown(0))
            {
                //マウスを押したポジションを代入
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //マウスを離したポジションを代入
                Vector2 endPos = Input.mousePosition;

                //(離した座標 - 押した座標)に-１を掛けている
                Vector2 startDirection = -1 * (endPos - startPos).normalized;
                this.rigid2D.AddForce(startDirection * speed);

                Avoidance = false;
            }
        }

        //キャラが一定以下になった事を検知し、キャラを停止
        if (this.rigid2D.velocity.magnitude < 2.0f && this.rigid2D.velocity.magnitude != 0)
        {
            Debug.Log("止まる");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            start = true;
            Avoidance = true;

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
            //Debug.Log("壁に当たった");
            this.rigid2D.velocity *= 0.95f;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("プレイヤーに当たった");
            this.rigid2D.velocity *= 0.9f;
        }
    }
}
