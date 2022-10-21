using UnityEngine;
using UnityEngine.UI;

public class CharaPanelProperty : MonoBehaviour
{
    [SerializeField, Header("キャラ名のテキスト")]
    private Text charaNameText;

    [SerializeField, Header("キャラ説明のテキスト")]
    private Text charaInfoText;

    [SerializeField, Header("武器モデルを配置する親オブジェクト")]
    private Transform modelParentTrf;

    public Text      CharaNameText => charaInfoText;
    public Transform ModelParentTrf => modelParentTrf;
    public Text      CharaInfoText => charaInfoText;
}
