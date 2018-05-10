using System.Collections;
using SUIFW;
using UnityEngine;

namespace DemoProject
{
	public class OperateModelForm : BaseUIForm {

        private Quaternion initialCamRot;

		public override void RegistEvent()
		{
			AddEvent("ApartUnit", OnRecieveUnitInfo);
		}

		public override void UnRegistEvent()
		{
			RemoveEvent("ApartUnit", OnRecieveUnitInfo);
		}

		private void OnRecieveUnitInfo(params object[] args)
		{
            var unit = args[0] as ModelUnit;
            if(unit != null)
            {
                
            }
		}

		void Awake () 
        {
		    //窗体性质
            CurrentUIType.UIForms_Type = UIFormType.Fixed;  //固定在主窗体上面显示
                                              
            RigisterButtonObjectEvent("ApartBtn",
                p =>
                {
                    if (GlobalObj.LabObj.activeInHierarchy) return;
                    if (initialCamRot != null)
                        CameraCtrl.instance.SetRot(initialCamRot);
	                GlobalObj.LabObj.SetActive(true);
	            }
				);

			RigisterButtonObjectEvent("StartBtn",
                p =>
                {
                    initialCamRot = CameraCtrl.instance.SetDrag();
                    GlobalObj.LabObj.SetActive(false);
                }
				);
        }
		
	}
}