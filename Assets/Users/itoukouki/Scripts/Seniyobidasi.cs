using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Seniyobidasi : MonoBehaviour
{
    GameObject ManageObject;
    Sceneseni fadeManager;
    public GameObject butoon;
    public Text karitext;
    // Start is called before the first frame update
    void Start()
    {
        //SceneFadeManagerがアタッチされているオブジェクトを取得
        ManageObject = GameObject.Find("ManageObject");
        //オブジェクトの中のSceneFadeManagerを取得
        fadeManager = ManageObject.GetComponent<Sceneseni>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Attack_Test"))
        //{
        //    //SceneFadeManagerの中のフェードアウト開始関数を呼び出し
        //    fadeManager.fadeOutStart(0, 0, 0, 0, "SampleScene");
        //    Destroy(butoon);
        //    Destroy(karitext);
        //}
    }
    public void OnClick()
    {
        fadeManager.fadeOutStart(0, 0, 0, 0, "SampleScene");
        Destroy(butoon);
        Destroy(karitext);
    }
}
