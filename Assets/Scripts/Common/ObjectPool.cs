using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developer.Machinery;
using System;
using SUIFW;

public class ObjectPool : MonoBehaviour
{

    //集合里面的元素，相当于对象池里面的对象，这里的集合可看作为对象池。
    Dictionary<string, ModelUnit> poolDic = new Dictionary<string, ModelUnit>();

    private static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                //动态的生成一个名为“GameObjectPool”的对象并将单例脚本附加上去
                _instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
            }
            return _instance;
        }
    }

    public ModelUnit takeOne(string name, bool isUiObj = false, Vector3 pos = default(Vector3), float scaleFactor = 0.5f, bool setPos = false)
    {
        if (isUiObj && !name.EndsWith("Ui"))
            name += "Ui";
        
		ModelUnit unit = null;
        if(poolDic.Count == 0)
        {
            unit = NoUnitHandle(name, isUiObj, pos, scaleFactor, setPos);
            return unit;
        }
        poolDic.TryGetValue(name, out unit);
        if (unit != null)
        {
			unit.SelfGameObj.SetActive(true);
            poolDic.Remove(name);
            return unit;
        }
		unit = NoUnitHandle(name, isUiObj, pos, scaleFactor, setPos);
		return unit;
    }

    private ModelUnit NoUnitHandle(string modelName, bool isUiModel, Vector3 pos = default(Vector3), float scaleFactor = 0.5f, bool setPos = false)
    {
        var unit = ModelMgr.instance.CreateModel(modelName, pos, scaleFactor, setPos);
		unit.SetName(modelName);
        if (isUiModel)
            unit.SetUiModel();
        else
            unit.SetLayer(LayerMask.NameToLayer(SysDefine.ModelLayer));
		return unit;
    }

    //向对象池里面存对象
    public void putBack(ModelUnit unit)
    {
        //将传进来的对象隐藏（处于非激活状态）
        unit.SelfGameObj.SetActive(false);
        //添加到对象池中（添加到集合中） 
        poolDic.Add(unit.name, unit);
    }

    public void putBack(GameObject unitHolder)
	{
        var driver = unitHolder.GetComponent<MeDriver>();
        if (!driver)
        {
            Debug.LogError("not correct object");
        }
        var unit = driver.unit;
		//将传进来的对象隐藏（处于非激活状态）
		unit.SelfGameObj.SetActive(false);
		//添加到对象池中（添加到集合中） 
		poolDic.Add(unit.name, unit);
	}
}