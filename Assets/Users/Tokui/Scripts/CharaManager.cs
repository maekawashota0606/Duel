using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    public static List<CharaDatas> charaDatas;
    public static List<Transform>   charaModels;

    public static CharaDatas SelectChara;

    public void Start()
    {
        charaDatas  = new List<CharaDatas>();
        charaModels = new List<Transform>();
        SelectChara = null;
    }

    public void Update()
    {

    }
}
