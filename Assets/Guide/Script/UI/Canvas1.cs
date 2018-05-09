using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Canvas1 : MonoBehaviour {

    void Awake()
    {
        EventTriggerListener.GetListener(UIManager1.Instance.Find("Button")).onPointerClick +=
            (go) => { UIManager1.Instance.Show("EquipPanel"); };
        EventTriggerListener.GetListener(UIManager1.Instance.Find("Button (1)")).onPointerClick +=
            (go) => { UIManager1.Instance.Show("BagPanel"); };
        EventTriggerListener.GetListener(UIManager1.Instance.Find("Button (2)")).onPointerClick +=
            (go) => {
                GuideManager.Instance.Next(); };
        EventTriggerListener.GetListener(UIManager1.Instance.Find("Button (3)")).onPointerClick +=
            (go) => { GuideManager.Instance.Next(); };

        GuideManager.Instance.Init(); 
    }

}
