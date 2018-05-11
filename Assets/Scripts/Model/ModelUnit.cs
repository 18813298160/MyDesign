using UnityEngine;
using SUIFW;
using Developer.Machinery;

public class ModelUnit
{
    private GameObject m_GameObj;
    private Transform m_Trans;
    private Transform m_UiRoot;
    private MeDriver m_Driver;

    public bool isUiModel;
    public Timer SelfTimer { get; private set; }
    public GameObject SelfGameObj { get { return m_GameObj; } }
    public Transform SelfTrans { get { return m_Trans; } }
    public Transform UiRoot { get { return m_UiRoot; } }
    public string name { get; private set; }

    public void SetModelObj(GameObject model, string name)
    {
        m_GameObj = model;
        m_Trans = model.transform;
        this.name = name;
        m_Driver = m_Trans.AddOrGetComponent<MeDriver>();
        m_Driver.SetUnit(this);
    }

    public void SetName(string name)
    {
        this.name = name;
        m_Driver.modelName = name;
    }

    public Transform SetSelfPosSize(Vector3 pos, float scaleFactor, bool setPos = false)
    {
        return m_Trans.SetPosition(pos, setPos).SetScale(scaleFactor);
    }

    public void CreateUiRoot(Transform baseRoot)
    {

    }

    public void SetUiModel(bool setPos = false, float scaleFactor = 1)
    {
        SetLayer(LayerMask.NameToLayer(SysDefine.UiModelLayer));
        string tmpName = name;
        if (tmpName.EndsWith("Ui"))
            tmpName = tmpName.Substring(0, tmpName.Length - 2);
        bool needResetRot = false;
        m_Trans.SetPosition(ModelMgr.GetPosAndRotInfo(tmpName, out needResetRot), true).SetScale(scaleFactor);
        if (needResetRot)
            m_Trans.rotation = Quaternion.identity;
        if (m_Trans.GetComponent<Rigidbody>() != null)
            m_Trans.GetComponent<Rigidbody>().useGravity = false;
        isUiModel = true;
    }

    public void SetLayer(int layer)
    {
        foreach (var trans in m_GameObj.GetComponentsInChildren<Transform>())
        {
            trans.gameObject.layer = layer;
        }
    }

    /// <summary>
    /// Default timer usage
    /// </summary>
    /// <param name="duration">Duration.</param>
    /// <param name="positive">If set to <c>true</c> positive.</param>
    public void StartTimer(float duration, bool positive = true)
    {
        if (SelfTimer != null)
        {
            if (SelfTimer.IsPause())
            {
                SelfTimer.Continue();
                return;
            }
        }
        StartTimer(duration, null, (args) => DriveMechanism(positive), null);
    }

    public void PauseTimer()
    {
        SelfTimer.Pause();
    }

    public void StartTimer(float duration, Timer.CallBack startCb = null, Timer.CallBack updateCb = null, Timer.CallBack endCb = null)
    {
        if (SelfTimer == null)
            SelfTimer = new Timer(duration);
        float process = SelfTimer.IsStart();
        if (process != -1) return;
        //TODO 延时开启定时器
        SelfTimer.SetDuration(duration);
        SelfTimer.OnUpdate = updateCb;
        SelfTimer.OnEnd = endCb;
        SelfTimer.Start();
        if (startCb != null)
            startCb.Invoke();
    }


    public void DestoryUiRoot()
    {
        if (m_UiRoot != null)
        {
            Object.Destroy(m_UiRoot.gameObject);
            m_UiRoot = null;
        }
    }

    public void Update()
    {

    }

    public void DriveMechanism(bool positive = true)
    {
        if (m_Driver == null)
        {
            Debug.LogError("no driver");
            return;
        }
        m_Driver.DriveMechanism(positive);
    }

    public void Destory()
    {
        if (m_GameObj != null)
        {
            Object.Destroy(m_GameObj);
            m_GameObj = null;
            m_Trans = null;
        }
    }

}
