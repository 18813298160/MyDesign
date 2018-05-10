using UnityEngine;
using SUIFW;

public class CameraCtrl : Singleton<CameraCtrl>
{
    private bool canDragCamera;
    private float cachedFieldView;

    void Awake()
    {
        cachedFieldView = GlobalObj.mainCamera.fieldOfView;
    }

    void Update()
	{
        if (UIManager.GetInstance().IsVisible(ProConst.PRO_DETAIL_UIFORM)) return;
		//Zoom out  
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (Camera.main.fieldOfView <= 100)
				Camera.main.fieldOfView += 2;
			if (Camera.main.orthographicSize <= 20)
				Camera.main.orthographicSize += 0.5F;
		}
		//Zoom in  
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (Camera.main.fieldOfView > 2)
				Camera.main.fieldOfView -= 2;
			if (Camera.main.orthographicSize >= 1)
				Camera.main.orthographicSize -= 0.5F;
		}

		if (Input.GetMouseButton(1) && canDragCamera)
		{
			if (GlobalObj.LabObj.activeInHierarchy) return;
			transform.Rotate(-Input.GetAxis("Mouse Y") * 2, Input.GetAxis("Mouse X") * 2, 0);
		}
	}

    public Quaternion SetDrag()
    {
        Quaternion cachedRot = transform.rotation;
        canDragCamera = true;
        return cachedRot;
    }

    public void ResetFieldView()
    {
        GlobalObj.mainCamera.fieldOfView = cachedFieldView;
    }

    public void SetRot(Quaternion rot)
    {
        transform.rotation = rot;
    }
}