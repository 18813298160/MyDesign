using UnityEngine;
using System.Collections;

public class GlobalObj
{
    public static Camera UiModelCamera;
	// Use this for initialization
    public static void Init()
	{
        UiModelCamera = FindGlobalObject<Camera>("ModelCamera");
	}


	public static T FindGlobalObject<T>(string name) where T : Component
	{
		GameObject go = GameObject.Find(name);
		if (go == null)
		{
			return null;
		}
		return go.GetComponent<T>();
	}

}
