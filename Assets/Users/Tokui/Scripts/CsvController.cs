using UnityEngine;
using UnityEngine.UI;

public class CsvController : MonoBehaviour
{
    [SerializeField]
    public Transform CanvasTransform = null;

    [SerializeField]
    public GameObject WeaponPanel = null;

    public void Start()
    {
        foreach (CharaDatas datas in CharaManager.charaDatas)
        {
            GameObject panelObject = Instantiate(WeaponPanel, CanvasTransform);
            var        panelProp   = panelObject.GetComponent<CharaPanelProperty>();

            panelProp.CharaNameText.text =
                "キャラ名\n" + datas.CharaName+
                "\n攻撃回数: " +datas.AtackCount +
                "\n回避回数: " + datas.AvoidanceCount +
                "\n威力: " + datas.Power +
                "\n能力:" + datas.Charainfo;

            panelProp.CharaInfoText.text =
                "説明\n" + datas.Charainfo;

            // モデル生成
            var modelPrefab = Resources.Load<GameObject>(datas.PrefabsName);

            if (modelPrefab == null)
            {
                Debug.LogError($"キャラ「{datas.CharaName}」のプレハブが見つかりません: {datas.PrefabsName}");

                continue;
            }

            GameObject modelObject = Instantiate(modelPrefab, panelProp.ModelParentTrf);

            CharaManager.charaModels.Add(modelObject.transform);
        }
    }

    public void Update()
    {

    }
}
