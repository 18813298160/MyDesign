using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUiObj : MonoBehaviour, 
    IDragHandler,
    IPointerUpHandler,
    IEndDragHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
	private Vector2 curDiff;
	private Vector2 prePos;
    private GameObject dragObj;
	private RectTransform canvas;                           //得到canvas的ugui坐标
	private RectTransform imgRect;
	private bool _enableDrag = true;                        
    private float _rotateSpeed = 8f;                       //旋转速度大小
	Vector3 enlargeScale = new Vector3(1.2f, 1.2f, 1);      //设置缩放
	Vector3 imgNormalScale = new Vector3(1, 1, 1);          //正常大小
    private string modelStr = "";

    public GameObject dragObject { get { return dragObj; }}


	void Awake()
    {
        canvas = GameObject.Find("Canvas(Clone)").GetComponent<RectTransform>();
		imgRect = GetComponent<RectTransform>();
    }

    public void SetModelType(string modelName)
    {
        modelStr = modelName;
        dragObj = ObjectPool.Instance.takeOne(modelStr, true).SelfGameObj;
    }

	//当鼠标抬起时调用对应接口  IPointerUpHandler
	public void OnPointerUp(PointerEventData eventData)
	{
        
	}

	//当鼠标进入图片时调用对应接口   IPointerEnterHandler
	public void OnPointerEnter(PointerEventData eventData)
	{
		imgRect.localScale = enlargeScale;   //放大图片
	}

	//当鼠标退出图片时调用对应接口   IPointerExitHandler
	public void OnPointerExit(PointerEventData eventData)
	{
		imgRect.localScale = imgNormalScale;   //恢复图片
    }

	void OnDisable()
	{
        ObjectPool.Instance.putBack(dragObj);
	}

	// 设置拖拽时的旋转速度
	public void SetRotateSpeed(float speed)
	{
		_rotateSpeed = speed;
	}

	// 是否允许拖拽
	public void EnableDrag(bool enable)
	{
		_enableDrag = enable;
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!_enableDrag || _rotateSpeed <= 0)
		{
			return;
		}

		prePos = eventData.position;
		curDiff = Vector2.zero;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_enableDrag || _rotateSpeed <= 0)
		{
			return;
		}

		var diff = eventData.position - prePos;
		if (diff != Vector2.zero)
		{
			prePos = eventData.position;
			curDiff = diff;
			RotateModel();
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!_enableDrag || _rotateSpeed <= 0)
		{
			return;
		}

		curDiff = Vector2.zero;
	}

	//旋转模型
	private void RotateModel()
	{
		if (dragObj != null)
		{
			var deg = curDiff.x * -_rotateSpeed * Time.unscaledDeltaTime;
			dragObj.transform.Rotate(Vector3.up, deg);
		}
	}
}

