using UnityEngine;
using UnityEngine.UI;

public class CsvController : MonoBehaviour
{
    [SerializeField]
    public Transform CanvasTransform = null;

    [SerializeField]
    public GameObject WeaponPanel1 = null;

    [SerializeField] 
    public GameObject WeaponPanel2 = null;

    public void Start()
    { 
        foreach (CharaDatas datas in CharaManager.charaDatas)
        {
            GameObject panelObject1 = Instantiate(WeaponPanel1, CanvasTransform);
            GameObject panelObject2 = Instantiate(WeaponPanel2, CanvasTransform);
            var panelProp1 = panelObject1.GetComponent<CharaPanelProperty>();
            var panelProp2 = panelObject2.GetComponent<CharaPanelProperty>();

            panelProp1.CharaNameText.text =
                "キャラ名:" + datas.CharaName;
            panelProp1.AtackCountText.text =
                "攻撃回数: " + datas.AtackCount;
            panelProp1.AvoidanceCountText.text =
                "回避回数: " + datas.AvoidanceCount;
            panelProp1.PowerText.text =
                "威力: " + datas.Power;
            panelProp1.CharaInfoText.text =
                "能力" + datas.Charainfo;
            
            panelProp2.CharaNameText.text =
                "キャラ名:" + datas.CharaName;
            panelProp2.AtackCountText.text =
                "攻撃回数: " + datas.AtackCount;
            panelProp2.AvoidanceCountText.text =
                "回避回数: " + datas.AvoidanceCount;
            panelProp2.PowerText.text =
                "威力: " + datas.Power;
            panelProp2.CharaInfoText.text =
                "能力" + datas.Charainfo;

            // モデル生成
            var modelPrefab = Resources.Load<GameObject>(datas.PrefabsName);

            if (modelPrefab == null)
            {
                Debug.LogError($"キャラ「{datas.CharaName}」のプレハブが見つかりません: {datas.PrefabsName}");

                continue;
            }

            GameObject modelObject1 = Instantiate(modelPrefab, panelProp1.ModelParentTrf);
            GameObject modelObject2 = Instantiate(modelPrefab, panelProp2.ModelParentTrf2);

            CharaManager.charaModels.Add(modelObject1.transform);
            CharaManager.charaModels.Add(modelObject2.transform);
        }
    }

    public void Update()
    {

    }
}
