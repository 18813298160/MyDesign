using System.Collections;
using SUIFW;
using UnityEngine;

namespace DemoProject
{
	public class StartProject : MonoBehaviour {

		void Start ()
        {
            GlobalObj.Init();
            ModelMgr.InitCfg();
            MarketUIFrom.InitCfg();
            UiModelView.instance.Init();
            //加载登陆窗体
            UIManager.GetInstance().ShowUIForms(ProConst.LOGON_FROMS);
		}

        void Update()
        {
            ModelMgr.instance.Update();
        }

    }
}