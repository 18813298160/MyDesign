using System.Collections;
using SUIFW;
using UnityEngine;
using UnityEngine.UI;
using Developer.Machinery;

public class PropDetailUIForm : BaseUIForm
{
    public Text TxtName;
    public Text introText;
    private RawImage image;
    private DragUiObj dragScript;

    public override void RegistEvent()
    {
        AddEvent("Props", OnRecievePropsInfo);
    }

    public override void UnRegistEvent()
    {
        RemoveEvent("Props", OnRecievePropsInfo);
    }

    void Awake()
    {
        //窗体的性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;

        /* 按钮的注册  */
        RigisterButtonObjectEvent("BtnClose",
        p =>
        {
            CloseUIForm();
        });
        image = UnityHelper.GetTheChildNodeComponetScripts<RawImage>(gameObject, "RawImage");
        dragScript = image.gameObject.GetComponent<DragUiObj>();
        if (UiModelView.instance.Rt)
            image.texture = UiModelView.instance.Rt;
        else
            Debug.LogError("no tex");

    }//Awake_end

    private void OnRecievePropsInfo(params object[] args)
    {
        ModelMarketConfig cfg = args[0] as ModelMarketConfig;
        if (cfg != null)
        {

            TxtName.text = TxtName ? cfg.meName : "";
            dragScript.SetModelType(cfg.modelName);
            introText.text = introText ? cfg.intro : "TODO";
            InitCtrlBtns();
        }
    }

    private void InitCtrlBtns()
    {
        var diver = dragScript.dragObject.GetComponent<MeDriver>();
        bool isSingle = diver.IsSingle();
        var modelUnit = diver.unit;
        GameObject btnAuto = UnityHelper.FindTheChildNode(gameObject, "Btn3").gameObject;
        GameObject btnPause = UnityHelper.FindTheChildNode(gameObject, "Btn4").gameObject;
        btnAuto.SetActive(isSingle);
        btnPause.SetActive(isSingle);
        if (!isSingle) return;

        RigisterButtonObjectEvent("Btn3",
        p =>
        {
            modelUnit.StartTimer(2, true);
        });
        RigisterButtonObjectEvent("Btn4",
        p =>
        {
            modelUnit.PauseTimer();
        });
    }

}