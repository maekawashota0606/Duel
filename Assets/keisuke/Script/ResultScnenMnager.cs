using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ResultScnenMnager;

public class ResultScnenMnager : MonoBehaviour
{
    //スピーカーの役割
    [SerializeField] AudioSource bgmAudioSource = default;
    [SerializeField] AudioSource seAudioSource = default;

    //曲(BGM)を入れる
    [SerializeField] AudioClip[] bgmList = default;
    public enum BGM
    {
        TitleBGM,
        GaneBGM,
        ClearBGM,
        GaneOverBGM,
    };

    //効果音(se)を入れる
    [SerializeField] AudioClip[] seList = default;
    public enum SE
    {
        TitleSE,
        GaneSE,
        ClearSE,
        GaneOverSE,
    };

    //どこからでも実装できるようにする＆シングルトン
    public static ResultScnenMnager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//シーンが変更しても破壊されなくなる
        }
        else
        {
            Destroy(gameObject);//同じもの(ResultScnenMnager)が既にある場合は破壊する
        }
    }

    public void PlayBGM(BGM bgm)
    {
        int nunber = (int)bgm;//列拳型のBGMを整数に変換
        bgmAudioSource.clip = bgmList[nunber];//BGMをセットする
        bgmAudioSource.Play();//再生する
    }

    public void PlaySE(BGM se)
    {
        int nunber = (int)se;//列拳型のSEを整数に変換
        AudioClip clip = seList[nunber];//効果音をセットする
        seAudioSource.PlayOneShot(clip);//1度再生する
    }
}
