using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConfig
{
	public string id = "";
    public string path;
}

public class ModelConfig : BaseConfig
{
    public float posX = 0;
	public float posY = 0;
	public float posZ = 0;
}

public class ModelMarketConfig : BaseConfig
{
	public string modelName = "";
	public string meName = "";
	public string intro = "";
}
