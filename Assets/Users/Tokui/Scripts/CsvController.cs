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
                "キャラ名\n" + datas.CharaName+
                "\n攻撃回数: " +datas.AtackCount +
                "\n回避回数: " + datas.AvoidanceCount +
                "\n威力: " + datas.Power +
                "\n能力:" + datas.Charainfo;
            panelProp1.CharaInfoText.text =
                "説明\n" + datas.Charainfo;
            
            panelProp2.CharaNameText2.text =
                "キャラ名\n" + datas.CharaName+
                "\n攻撃回数: " +datas.AtackCount +
                "\n回避回数: " + datas.AvoidanceCount +
                "\n威力: " + datas.Power +
                "\n能力:" + datas.Charainfo;
            panelProp2.CharaInfoText2.text =
                "説明\n" + datas.Charainfo;

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
