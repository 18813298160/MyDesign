using UnityEngine;
using System.Collections;

public class AvatarBtn : MonoBehaviour
{

    int count = 0;
    public void OnClick_coat()
    {
        if (count == 0)
        {
            AvatarSys.instance.ChangeMesh("coat", "001");
            count = 1;
        }
        else
        {
            AvatarSys.instance.ChangeMesh("coat", "003");
            count = 0;
        }
    }
    public void OnClick_hair()
    {
        if (count == 0)
        {
            AvatarSys.instance.ChangeMesh("hair", "001");
            count = 1;
        }
        else
        {
            AvatarSys.instance.ChangeMesh("hair", "003");
            count = 0;
        }
    }
    public void OnClick_hand()
    {
        if (count == 0)
        {
            AvatarSys.instance.ChangeMesh("hand", "001");
            count = 1;
        }
        else
        {
            AvatarSys.instance.ChangeMesh("hand", "003");
            count = 0;
        }
    }
    public void OnClick_head()
    {
        if (count == 0)
        {
            AvatarSys.instance.ChangeMesh("head", "001");
            count = 1;
        }
        else
        {
            AvatarSys.instance.ChangeMesh("head", "003");
            count = 0;
        }
    }
    public void OnClick_pant()
    {
        if (count == 0)
        {
            AvatarSys.instance.ChangeMesh("pant", "001");
            count = 1;
        }
        else
        {
            AvatarSys.instance.ChangeMesh("pant", "003");
            count = 0;
        }
    }
    public void OnClick_foot()
    {
        if (count == 0)
        {
            AvatarSys.instance.ChangeMesh("foot", "001");
            count = 1;
        }
        else
        {
            AvatarSys.instance.ChangeMesh("foot", "003");
            count = 0;
        }
    }


}
