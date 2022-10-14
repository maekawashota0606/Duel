using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seniyobidasi : MonoBehaviour
{
    GameObject ManageObject;
    Sceneseni fadeManager;
    public GameObject butoon;
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
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    //SceneFadeManagerの中のフェードアウト開始関数を呼び出し
        //    fadeManager.fadeOutStart(0, 0, 0, 0, "main");
        //}
    }
    public void OnClick()
    {
        fadeManager.fadeOutStart(0, 0, 0, 0, "SampleScene");
        Destroy(butoon);
    }
}
