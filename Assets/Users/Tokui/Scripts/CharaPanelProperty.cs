using UnityEngine;
using UnityEngine.UI;

public class CharaPanelProperty : MonoBehaviour
{
    [SerializeField, Header("キャラ名のテキスト")]
    private Text charaNameText;
    [SerializeField, Header("キャラ名のテキスト")]
    private Text charaNameText2;

    [SerializeField, Header("キャラ説明のテキスト")]
    private Text charaInfoText;
    [SerializeField, Header("キャラ説明のテキスト")]
    private Text charaInfoText2;

    [SerializeField, Header("攻撃範囲を配置する親オブジェクト")]
    private Transform modelParentTrf;
    [SerializeField, Header("攻撃範囲を配置する親オブジェクト")]
    private Transform modelParentTrf2;

    public Text      CharaNameText => charaInfoText;
    public Text      CharaNameText2 => charaInfoText2;
    public Transform ModelParentTrf => modelParentTrf;
    public Transform ModelParentTrf2 => modelParentTrf2;
    public Text      CharaInfoText => charaInfoText;
    public Text      CharaInfoText2 => charaInfoText2;
}
