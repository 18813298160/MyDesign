using System.Collections;
using SUIFW;

public class SelectHeroUIForm : BaseUIForm
{

    public void Awake()
    {
        //窗体的性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;

        //注册进入主城的事件
        RigisterButtonObjectEvent("BtnConfirm",
            p =>
            {
                OpenUIForm(ProConst.MAIN_CITY_UIFORM);
                OpenUIForm(ProConst.HERO_INFO_UIFORM);
            }

            );

        //注册返回上一个页面
        RigisterButtonObjectEvent("BtnClose",
            m => CloseUIForm()
            );
    }
}