using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvatarSys : MonoBehaviour
{
    private Transform source;//来源
    private Transform target;//生成目标
    private GameObject sourceObj;//两个临时变量
    private GameObject targetObj;

    //Data字典 存储source的各种信息；
    private Dictionary<string, Dictionary<string, Transform>> data = new Dictionary<string, Dictionary<string, Transform>>();
    //targetSmr字典 存储目标的骨骼信息
    private Dictionary<string, SkinnedMeshRenderer> targetSmr = new Dictionary<string, SkinnedMeshRenderer>();
    //动画
    private Animation anim;
    //骨骼数组
    private Transform[] hips;
    //单例模式(方便外部传值)
    public static AvatarSys instance;

    //模拟服务器数据
    string[,] avatarStr = new string[,] { { "coat", "003" }, { "hair", "001" }, { "pant", "001" }, { "hand", "003" }, { "foot", "001" }, { "head", "003" } };

    // Use this for initialization
    void Start()
    {
        instance = this;
        InstantiateAvatar();
        InstantiateSkeleton();
        LoadAvatarData(source);
        hips = target.GetComponentsInChildren<Transform>();
        Inivatar();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //实例化来源
    void InstantiateAvatar() 
    {
        source = ((GameObject)Instantiate(Resources.Load("FemaleAvatar"))).transform;
        //source = sourceObj.transform;
        //sourceObj.SetActive(false);
        source.gameObject.SetActive(false);
    }
    //实例化骨架
    void InstantiateSkeleton()
    {
        targetObj = (GameObject)Instantiate(Resources.Load("targetmodel"));
        target = targetObj.transform;
    }
    //读取来源的各种信息
    void LoadAvatarData(Transform source)
    {
        if (source == null)
        {
            return;
        }
        SkinnedMeshRenderer[] parts = source.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        foreach (SkinnedMeshRenderer part in parts)
        {
            string[] partName = part.name.Split('-');
            if (!data.ContainsKey(partName[0]))
            {
                data.Add(partName[0],new Dictionary<string, Transform>());
                GameObject partObj = new GameObject();
                partObj.name = partName[0];
                partObj.transform.parent = target;
                targetSmr.Add(partName[0], partObj.AddComponent<SkinnedMeshRenderer>());
            }
            data[partName[0]].Add(partName[1], part.transform);
        }
    }
    //更改Mesh
   public void ChangeMesh(string part,string item)
    {
        SkinnedMeshRenderer smr = data[part][item].GetComponent<SkinnedMeshRenderer>();
        List<Transform> bones = new List<Transform>();
        foreach (Transform bone in smr.bones)
        {
            foreach (Transform hip in hips)
            {
                if (hip.name!=bone.name)
                {
                    continue;
                }
                bones.Add(hip);
                break;
            }
        }
        targetSmr[part].sharedMesh = smr.sharedMesh;
        targetSmr[part].bones = bones.ToArray();
        targetSmr[part].materials = smr.materials;
    }

    void Inivatar()
    {
        int nLength = avatarStr.GetLength(0);
        for (int i = 0; i < nLength; i++)
        {
            ChangeMesh(avatarStr[i, 0], avatarStr[i, 1]);
        }
    }


}
