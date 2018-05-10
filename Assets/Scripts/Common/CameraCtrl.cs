using UnityEngine;
using System.Collections;

public class CameraCtrl : Singleton<CameraCtrl>
{
    private bool canDragCamera;

    void Update()
	{
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
            
			transform.Rotate(-Input.GetAxis("Mouse Y") * 2, Input.GetAxis("Mouse X") * 2, 0);
		}
	}

    public Quaternion SetDrag()
    {
        Quaternion cachedRot = transform.rotation;
        canDragCamera = true;
        return cachedRot;
    }

    public void SetRot(Quaternion rot)
    {
        transform.rotation = rot;
    }
}