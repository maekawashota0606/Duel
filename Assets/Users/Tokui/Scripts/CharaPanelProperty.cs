using UnityEngine;
using UnityEngine.UI;

public class CharaPanelProperty : MonoBehaviour
{
    [SerializeField, Header("キャラ名のテキスト")]
    private Text charaNameText;
    [SerializeField, Header("キャラ名のテキスト")]
    private Text charaNameText2;

    [SerializeField, Header("攻撃回数のテキスト1")]
    private Text charaInfoText1_1;
    [SerializeField, Header("攻撃回数のテキスト2")]
    private Text charaInfoText1_2;
    
    [SerializeField, Header("回避回数のテキスト1")]
    private Text charaInfoText2_1;
    [SerializeField, Header("回避回数のテキスト2")]
    private Text charaInfoText2_2;
    
    [SerializeField, Header("威力のテキスト1")]
    private Text charaInfoText3_1;
    [SerializeField, Header("威力のテキスト2")]
    private Text charaInfoText3_2;
    
    [SerializeField, Header("能力のテキスト1")]
    private Text charaInfoText4_1;
    [SerializeField, Header("能力のテキスト2")]
    private Text charaInfoText4_2;

    [SerializeField, Header("攻撃範囲を配置する親オブジェクト")]
    private Transform modelParentTrf;
    [SerializeField, Header("攻撃範囲を配置する親オブジェクト")]
    private Transform modelParentTrf2;

    #region Text

    public Text CharaNameText => charaInfoText1_1;
    public Text CharaNameText2 => charaInfoText1_2;
    
    
    
    public Text AtackCountText => charaInfoText1_1;
    public Text AtackCountText2 => charaInfoText1_2;

    public Text AvoidanceCountText => charaInfoText2_1;
    public Text AvoidanceCountText2 => charaInfoText2_2;
    
    public Text PowerText => charaInfoText3_1;
    public Text PowerText2 => charaInfoText3_2;
    
    public Text CharaInfoText => charaInfoText4_1;
    public Text CharaInfoText2 => charaInfoText4_2;

    #endregion
    
    public Transform ModelParentTrf => modelParentTrf;
    public Transform ModelParentTrf2 => modelParentTrf2;
    
}
