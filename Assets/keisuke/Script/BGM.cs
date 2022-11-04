using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public  ResultScnenMnager.BGM bgm;
    // Start is called before the first frame update
    void Start()
    {
        ResultScnenMnager.instance.PlayBGM(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
