using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developer.Machinery;
using System;

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

    public ModelUnit takeOne(string name, bool isUiObj = false)
    {
        if (isUiObj && !name.EndsWith("Ui"))
            name += "Ui";
        
		ModelUnit unit = null;
        if(poolDic.Count == 0)
        {
            unit = NoUnitHandle(name, isUiObj);
            return unit;
        }
        poolDic.TryGetValue(name, out unit);
        if (unit != null)
        {
			unit.SelfGameObj.SetActive(true);
            poolDic.Remove(name);
            return unit;
        }
		unit = NoUnitHandle(name, isUiObj);
		return unit;
    }

    private ModelUnit NoUnitHandle(string modelName, bool isUiModel, bool setPos = false)
    {
		var unit = ModelMgr.instance.CreateModel(modelName, scaleFactor: 0.5f);
		unit.SetName(modelName);
        if(isUiModel)
		    unit.SetUiModel(setPos);
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