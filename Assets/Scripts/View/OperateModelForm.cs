using System.Collections;
using SUIFW;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Developer.Machinery;
using DG.Tweening;

public class OperateModelForm : BaseUIForm
{

    public Text virtualText;

    private Button resetBtn;
    private Ray ray;
    private RaycastHit hit;
    private bool flag = false;
    private Transform curSelectedTrans;
    private Quaternion initialCamRot;
    private bool canApart = false;
    private MeDriver curDriver;

    private GameObject loadingObj;
    private Image loadingImage;
    private string curModelName;
    private ModelUnit unit;

    private Dictionary<Transform, CachedInfo> colliderDic;

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
        unit = args[0] as ModelUnit;
        if (unit != null)
        {
            curModelName = unit.name;
        }
    }

    void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.Fixed;  //固定在主窗体上面显示
        resetBtn = UnityHelper.GetTheChildNodeComponetScripts<Button>(gameObject, "ResetBtn");
        resetBtn.enabled = false;
        RigisterButtonObjectEvent("StartBtn", EnterFunc);
        RigisterButtonObjectEvent("ResetBtn", ResetMechanisms);
        RigisterButtonObjectEvent("SearchBtn", ShowModelDetail);
        RigisterButtonObjectEvent("CloseBtn",
        p =>
        {
            if (!GlobalObj.LabObj.activeInHierarchy)
            {
                ExitFunc();
                return;
            }
            CloseUIForm();
			CameraCtrl.instance.CorrectCamera();
            GlobalObj.LabObj.SetActive(true);
            UIManager.GetInstance().ShowUIForms(ProConst.HERO_INFO_UIFORM);
            ObjectPool.Instance.putBack(unit);
        });
        colliderDic = new Dictionary<Transform, CachedInfo>();
        loadingObj = UnityHelper.FindTheChildNode(gameObject, "LoadingObj").gameObject;
        loadingObj.SetActive(false);
        loadingImage = UnityHelper.GetTheChildNodeComponetScripts<Image>(loadingObj, "progress");
        loadingImage.fillAmount = 0;
        canApart = false;
    }

    void Update()
    {
        if (!canApart || UIManager.GetInstance().IsVisible(ProConst.PRO_DETAIL_UIFORM)) return;
        //当鼠标点击物体时，物体随鼠标一起移动；当鼠标再次点击时，放下物体。
        if (Input.GetMouseButtonDown(0))
        {
            //创建一条摄像机到屏幕的射线
            ray = GlobalObj.mainCamera.ScreenPointToRay(Input.mousePosition);
            var layer = LayerMask.NameToLayer(SysDefine.ModelLayer);
            //若碰到物体
            if (Physics.Raycast(ray, out hit, 100f, 1 << layer))
                flag = !flag;
        }

        if (flag && hit.collider)
        {
            curSelectedTrans = hit.collider.transform;
            //缓存零件信息
            if (!colliderDic.ContainsKey(hit.collider.transform))
                colliderDic.Add(hit.collider.transform, new CachedInfo(curSelectedTrans.parent, curSelectedTrans.position));

            Vector3 screenPos = GlobalObj.mainCamera.WorldToScreenPoint(curSelectedTrans.position);
            Vector3 worldPos = GlobalObj.mainCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                            Input.mousePosition.y,
                            screenPos.z)
            );
            curSelectedTrans.position = worldPos;
            curDriver = curSelectedTrans.GetMeDrive();
            if (curDriver)
                curDriver.SetDriveCapability(false);
            curSelectedTrans.SetParent(ModelMgr.instance.modelRoot);
        }
    }

    void EnterFunc(GameObject t)
    {
        StartCoroutine(LoadProgress(() =>
        {
            resetBtn.enabled = true;
            initialCamRot = CameraCtrl.instance.SetDrag();
            GlobalObj.LabObj.SetActive(false);

            canApart = true;
            if (virtualText)
                virtualText.text = "退出虚拟\n拆装环境";
            RigisterButtonObjectEvent("StartBtn", ExitFunc);
        }));

    }

    //退出虚拟拆装环境
    void ExitFunc(GameObject t = null)
    {
        if (GlobalObj.LabObj.activeInHierarchy) return;
        resetBtn.enabled = false;
        if (initialCamRot != null)
            CameraCtrl.instance.SetRot(initialCamRot);
        CameraCtrl.instance.ResetFieldView();
        GlobalObj.LabObj.SetActive(true);
        canApart = false;
        if (virtualText)
            virtualText.text = "进入虚拟\n拆装环境";
        RigisterButtonObjectEvent("StartBtn", EnterFunc);
        ResetMechanisms();
    }

    private void ShowModelDetail(GameObject obj = null)
    {
        OpenUIForm(ProConst.PRO_DETAIL_UIFORM);
        var cfg = GetModelCfg(curModelName);
        //传递数据
        DispatchEvent("Props", cfg);
    }

    private ModelMarketConfig GetModelCfg(string modelName)
    {
        ModelMarketConfig tmpCfg = null;
        MarketUIFrom.modelMarketConfigDic.TryGetValue(modelName, out tmpCfg);
        return tmpCfg;
    }

    private void ResetMechanisms(GameObject obj = null)
    {
        //重置机构零件信息
        foreach (var pair in colliderDic)
        {
            Transform trans = pair.Key;
            var driver = trans.GetMeDrive();
            if (driver)
                driver.SetDriveCapability(true);
            trans.DOMove(pair.Value.pos, 1);
            trans.SetParent(pair.Value.parent);
        }
    }

    private IEnumerator LoadProgress(Action doneCb = null)
    {
        loadingObj.SetActive(true);
        CameraCtrl.instance.CorrectCamera(unit.SelfTrans.position.z);
        int i = 0;
        while (i < 80)
        {
            i += 2;
            float progress = i / 80.0f;
            loadingImage.fillAmount = progress;
            yield return new WaitForEndOfFrame();
        }
        if (doneCb != null)
            doneCb.Invoke();
        loadingObj.SetActive(false);
    }

}

public class CachedInfo
{
    public Transform parent;
    public Vector3 pos;

    public CachedInfo(Transform p, Vector3 pos)
    {
        this.parent = p;
        this.pos = pos;
    }
}