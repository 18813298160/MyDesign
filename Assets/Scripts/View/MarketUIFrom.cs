using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SUIFW;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketUIFrom : BaseUIForm
{
    public static Dictionary<string, ModelMarketConfig> modelMarketConfigDic { get; private set; }
    public static ConfigReader<ModelMarketConfig> modelMarketConfig { get; private set; }

    private Transform content;

	public static void InitCfg()
	{
		modelMarketConfig = new ConfigReader<ModelMarketConfig>(SysDefine.UimodelMarketCfg);
		modelMarketConfigDic = modelMarketConfig.LoadConfig();
	}

    public override void ShowTween()
    {
        base.ShowTween();
        var canvasGroup = gameObject.AddOrGetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 1);
    }

    void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

        //注册按钮事件：退出
        RigisterButtonObjectEvent("Btn_Close",
            P => CloseUIForm()
            );

        content = UnityHelper.FindTheChildNode(gameObject, "content");

        foreach (Transform child in content.GetComponentsInChildren<Transform>())
        {
            var cfg = GetModelCfg(child.name);
            if (cfg != null)
            {
                RigisterButtonObjectEvent(child.name,
                P =>
                {
                        //打开子窗体
                        OpenUIForm(ProConst.PRO_DETAIL_UIFORM);

                        //传递数据
                        DispatchEvent("Props", cfg);
                }
                );
                Transform showTextTrans = UnityHelper.FindTheChildNode(child.gameObject, "TextShow");
                if (showTextTrans)
                {
                    showTextTrans.GetComponent<Text>().text = cfg.meName;
                    EventTriggerListener.Get(child).onEnter = obj =>
                    {
                        showTextTrans.gameObject.SetActive(true);
                    };
                    EventTriggerListener.Get(child).onExit = obj =>
                    {
	                    showTextTrans.gameObject.SetActive(false);
                    };
                }
            }

        }
    }

    private ModelMarketConfig tmpCfg = null;
    private ModelMarketConfig GetModelCfg(string btnName)
    {
        tmpCfg = null;
        modelMarketConfigDic.TryGetValue(btnName, out tmpCfg);
        return tmpCfg;
    }

}