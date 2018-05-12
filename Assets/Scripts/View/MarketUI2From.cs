using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SUIFW;

public class MarketUI2From : BaseUIForm
{
    private static Dictionary<string, ModelMarket2Config> modelMarket2ConfigDic;
    private static ConfigReader<ModelMarket2Config> modelMarket2Config;

    private Transform content;
    private ModelUnit curUnit;

	public static void InitCfg()
	{
		modelMarket2Config = new ConfigReader<ModelMarket2Config>(SysDefine.UimodelMarket2Cfg);
		modelMarket2ConfigDic = modelMarket2Config.LoadConfig();
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

              P =>
              {
                  CloseUIForm();
              }
            );

        content = UnityHelper.FindTheChildNode(gameObject, "content");
        foreach (Transform child in content.GetComponentsInChildren<Transform>())
        {
            var cfg = GetModelName(child.name);
            if (cfg != null)
            {
                RigisterButtonObjectEvent(child.name,
                P =>
                {
                    Vector3 pos = new Vector3(cfg.posX, cfg.posY, cfg.posZ);
                    curUnit = ObjectPool.Instance.takeOne(cfg.modelName, false, pos, 0.2f, true);
                    CloseUIForm();
                    CloseUIForm(ProConst.HERO_INFO_UIFORM);
                    OpenUIForm(ProConst.OPERATEMODELFORM);
                    DispatchEvent("ApartUnit", curUnit, cfg.title);
                }
                );
            }

        }
    }

    private ModelMarket2Config tmpCfg = null;
    private ModelMarket2Config GetModelName(string btnName)
    {
        tmpCfg = null;
        modelMarket2ConfigDic.TryGetValue(btnName, out tmpCfg);
        return tmpCfg;
    }

}