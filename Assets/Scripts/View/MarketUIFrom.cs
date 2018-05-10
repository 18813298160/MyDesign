using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;

namespace DemoProject
{
    public class MarketUIFrom : BaseUIForm
    {
        private static Dictionary<string, ModelMarketConfig> modelMarketConfigDic;
		private static ConfigReader<ModelMarketConfig> modelMarketConfig;

        private Transform content;

		void Awake ()
        {
		    //窗体性质
		    CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
		    CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
		    CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

            //注册按钮事件：退出
            RigisterButtonObjectEvent("Btn_Close",
                P=> CloseUIForm()                
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
					    //打开子窗体
					    OpenUIForm(ProConst.PRO_DETAIL_UIFORM);

					    //传递数据
                        DispatchEvent("Props", cfg);
	                }
	                );
                }

			}
        }

        private ModelMarketConfig tmpCfg = null;
        private ModelMarketConfig GetModelName(string btnName)
		{
            tmpCfg = null;
			modelMarketConfigDic.TryGetValue(btnName, out tmpCfg);
            return tmpCfg;
		}

        public static void InitCfg()
        {
            modelMarketConfig = new ConfigReader<ModelMarketConfig>(SysDefine.UimodelMarketCfg);
			modelMarketConfigDic = modelMarketConfig.LoadConfig();
        }
		
	}
}