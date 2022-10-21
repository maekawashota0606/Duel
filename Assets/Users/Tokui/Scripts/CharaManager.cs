using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    public static List<CharaDatas> weaponDatas;
    public static List<Transform>   weaponModels;

    public static CharaDatas SelectWeapon;

    public void Start()
    {
        weaponDatas  = new List<CharaDatas>();
        weaponModels = new List<Transform>();
        SelectWeapon = null;
    }

    public void Update()
    {

    }
}
