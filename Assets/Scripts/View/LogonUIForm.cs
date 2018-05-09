using System.Collections;
using SUIFW;
using UnityEngine.UI;

namespace DemoProject
{
    public class LogonUIForm : BaseUIForm
    {
        public Text TxtLogonName;                           //登陆名称
        public Text TxtLogonNameByBtn;                      //登陆名称(Button)

        public void Awake()
        {
            //定义本窗体的性质(默认数值，可以不写)
            base.CurrentUIType.UIForms_Type = UIFormType.Normal;
            base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
            base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;



            RigisterButtonObjectEvent("Btn_OK", 
                  p =>
                  {
                      CloseUIForm();
                      UIManager.GetInstance().ShowUIForms(ProConst.HERO_INFO_UIFORM);
				  }

                //p=>OpenUIForm(ProConst.SELECT_HERO_FORM)
                );
        }

        public void Start()
        {
            //string strDisplayInfo = LauguageMgr.GetInstance().ShowText("LogonSystem");

            if (TxtLogonName)
            {
                TxtLogonName.text = Show("LogonSystem");
            }
            if (TxtLogonNameByBtn)
            {
                TxtLogonNameByBtn.text = Show("LogonSystem");
            }
        }

    }
}