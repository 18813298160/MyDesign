using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;

namespace DemoProject
{
    public class MarketUI2From : BaseUIForm
    {
        private static Dictionary<string, ModelMarketConfig> modelMarket2ConfigDic;
		private static ConfigReader<ModelMarketConfig> modelMarket2Config;

        private Transform content;

		void Awake ()
        {
		    //窗体性质
		    CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
		    CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
		    CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

            //注册按钮事件：退出
            RigisterButtonObjectEvent("Btn_Close",
                  
                  P=>
                  {
                      CloseUIForm();
                  }        
                );

            content = UnityHelper.FindTheChildNode(gameObject, "content");
            foreach(Transform child in content.GetComponentsInChildren<Transform>())
            {
                var cfg = GetModelName(child.name);
                if(cfg != null)
                {
                    RigisterButtonObjectEvent(child.name,
                    P =>
                    {
                        ObjectPool.Instance.takeOne(cfg.modelName, false);
                        CloseUIForm();
	                }
	                );
                }

			}
        }

        private ModelMarketConfig tmpCfg = null;
        private ModelMarketConfig GetModelName(string btnName)
		{
            tmpCfg = null;
			modelMarket2ConfigDic.TryGetValue(btnName, out tmpCfg);
            return tmpCfg;
		}

        public static void InitCfg()
        {
            modelMarket2Config = new ConfigReader<ModelMarketConfig>(SysDefine.UimodelMarket2Cfg);
			modelMarket2ConfigDic = modelMarket2Config.LoadConfig();
        }
		
	}
}