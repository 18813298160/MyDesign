using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SUIFW;
using Developer.Machinery;

public class ModelMgr
{

	private Dictionary<string, string> modelPathDic;
	private Dictionary<int, ModelUnit> modelObjDic;

    private static Dictionary<string, ModelConfig> modelConfigDic;
    private static ConfigReader<ModelConfig> modelConfig;

    public static ModelMgr instance
    {
        get
        {
            return Single<ModelMgr>.instance;
        }
    }

    public ModelMgr()
    {
        InitPathData();
        modelObjDic = new Dictionary<int, ModelUnit>();
    }

    public static void InitCfg()
    {
		modelConfig = new ConfigReader<ModelConfig>(SysDefine.UimodelCfg);
		modelConfigDic = modelConfig.LoadConfig();
    }

    public static Vector3 GetPosAndRotInfo(string modelName, out bool needResetRot)
    {
        Vector3 pos = new Vector3();
        ModelConfig cfg = null;
        modelConfigDic.TryGetValue(modelName, out cfg);
        if (cfg != null)
        {
			pos.x = cfg.posX;
			pos.y = cfg.posY;
            pos.z = cfg.posZ;
            needResetRot = cfg.resetRot == 1;
        }
        else
            needResetRot = false;
        return pos;
    }

	private void InitPathData()
	{
        IConfigManager configMgr = new ConfigManagerByJson(SysDefine.MODEL_PATH_CONFIG_INFO);
		if (configMgr != null)
		{
			modelPathDic = configMgr.AppSetting;
		}
	}

    public void Update()
    {
        foreach(var unit in modelObjDic.Values)
        {
            unit.Update();
        }
    }

    public ModelUnit CreateModel(string modelName, Vector3 pos = default(Vector3), float scaleFactor = 1, bool setPos = false)
	{
		string modelPath = null;
        GameObject modelObj = null;
        ModelUnit unit = null;
        if (modelName.EndsWith("Ui"))
            modelName = modelName.Substring(0, modelName.Length - 2);
        modelPathDic.TryGetValue(modelName, out modelPath);
        if (!string.IsNullOrEmpty(modelPath))
        {
            modelObj = ResourcesMgr.GetInstance().LoadAsset(modelPath, false);
            modelObj.transform.SetParent(modelRoot);
        }

        if (modelObj != null)
        {
            modelObjDic.TryGetValue(modelObj.GetInstanceID(), out unit);
            if (unit == null)
            {
                unit = new ModelUnit();
                unit.isUiModel = false;
                unit.SetModelObj(modelObj, modelName);
                unit.SetSelfPosSize(pos, scaleFactor, setPos);
                modelObjDic.Add(modelObj.GetInstanceID(), unit);
            }
        }

        return unit;
	}

    public Transform modelRoot
    {
        get
        {
            if (m_ModelRoot == null)
            {

                var obj = GameObject.Find("ModelRoot");
                if(obj == null)
                    obj = new GameObject("ModelRoot");
                m_ModelRoot = obj.transform;
            }
            return m_ModelRoot;
        }

    }

    protected Transform m_ModelRoot;
}
