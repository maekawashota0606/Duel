using UnityEngine;
using UnityEngine.UI;

public class CharaSelectionProperty : SingletonMonoBehaviour<CharaSelectionProperty>
{
    [SerializeField]
    private ScrollSnapSelector snapSelector;

    public static ScrollSnapSelector SnapSelector => Instance.snapSelector;
    
    [SerializeField]
    private ScrollSnapSelector snapSelector2;

    public static ScrollSnapSelector SnapSelector2 => Instance.snapSelector;
}
