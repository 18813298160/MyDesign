using UnityEngine;
using System.Collections;

public class GlobalObj
{
    public static Camera UiModelCamera;
	public static GameObject LabObj;
	public static Camera mainCamera;
	public static Camera uiCamera;
	// Use this for initialization
    public static void Init()
	{
        UiModelCamera = FindGlobalObject<Camera>("ModelCamera");
        LabObj = GameObject.Find("Lab");
		mainCamera = FindGlobalObject<Camera>("MainCamera");
        mainCamera.AddOrGetComponent<CameraCtrl>();
	}

    public static void InitUiCamera(Camera ca)
    {
        uiCamera = ca;
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
