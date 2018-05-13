using System.Collections;
using SUIFW;
using UnityEngine;

public class StartProject : MonoBehaviour
{

    void Start()
    {
        GlobalObj.Init();
        GuideManager.instance.Init();
        ModelMgr.InitCfg();
        MarketUIFrom.InitCfg();
        MarketUI2From.InitCfg();
        UiModelView.instance.Init();
        //加载登陆窗体
        UIManager.GetInstance().ShowUIForms(ProConst.LOGON_FROMS);
	}

    void Update()
    {
        ModelMgr.instance.Update();
    }

}