using SUIFW;

public class MainCityUIForm : BaseUIForm
{
    public void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;

        //事件注册
        RigisterButtonObjectEvent("BtnMarket",
            p => OpenUIForm(ProConst.MARKET_UIFORM)
            );

    }

}