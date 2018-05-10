using SUIFW;

public class HeroInfoUIForm : BaseUIForm {


	void Awake () 
    {
	    //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.Fixed;  //固定在主窗体上面显示

		RigisterButtonObjectEvent("BtnItem1 (2)",
        p =>
        {
            OpenUIForm(ProConst.MARKET_UIFORM);
		});
		RigisterButtonObjectEvent("BtnItem1 (1)",
        p =>
        {
            OpenUIForm(ProConst.MARKET_UI2FORM);
        });
    }
	
}