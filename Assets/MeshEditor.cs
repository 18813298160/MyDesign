using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

/*
 * 每个顶点的位置可能存在几个顶点，所以代码里有subList进行统计分组
 * */

[ExecuteInEditMode, DisallowMultipleComponent, AddComponentMenu("网格 Editor")]
public class MeshEditor : MonoBehaviour
{
	[SerializeField, Header("顶点大小"), Range(0, 1)]
	public float vertexSize = 0.03f;
	//顶点数量
	[SerializeField, Header("顶点数量")]
	public int vertexCnt = 0;
	//最大处理顶点数
	[System.NonSerialized]
	public const int maxVerticeCnt = 2000;
	//所有顶点物体集合
	[System.NonSerialized]
	public GameObject[] verticesObj;
	[System.NonSerialized]
	public int verticesObjCnt = 0;
	//物体网格
	[System.NonSerialized]
	public Mesh mesh;
	//目标物体
	[System.NonSerialized]
	public GameObject targetObj;
	//目标物体所有三角面集合
	[System.NonSerialized]
	public List<List<int>> allTriangleList;
	//克隆体名称
	[System.NonSerialized]
	public string cloneName = "Clone Mesh";
	[System.NonSerialized]
	public List<OrderedVertice> allVerticeList;
	[System.NonSerialized]
	public List<OrderedVertice> finalVerticeList;
	[System.NonSerialized]
	public List<List<OrderedVertice>> verticesGroupList;

	//顶点大小缓存
	[System.NonSerialized]
	public float chachedVertexSize;

	void Start()
	{
		if (!PreFiguration()) return;

		Init();
	}

	void Update()
	{
		RefreshMesh();
	}

	bool PreFiguration()
	{
		if (GetComponent<MeshFilter>() == null)
		{
			EditorUtility.DisplayDialog("警告", "游戏物体缺少组件 MeshFilter！", "确定");
			return false;
		}
		if (GetComponent<MeshRenderer>() == null)
		{
			EditorUtility.DisplayDialog("警告", "游戏物体缺少组件 MeshRenderer！", "确定");
			return false;
		}
		if (GetComponent<MeshRenderer>().sharedMaterial == null)
		{
			EditorUtility.DisplayDialog("警告", "游戏物体无材质！", "确定");
			return false;
		}
		vertexCnt = GetComponent<MeshFilter>().sharedMesh.vertexCount;
		if (vertexCnt > maxVerticeCnt)
		{
			vertexCnt = 0;
			EditorUtility.DisplayDialog("警告", "超过最大顶点数！", "确定");
			return false;
		}
		return true;
	}

	void Init()
	{
		//顶点处理
		allVerticeList = new List<OrderedVertice>();
		verticesGroupList = new List<List<OrderedVertice>>();
		var tmp = GetComponent<MeshFilter>().sharedMesh.vertices;
		for (int i = 0; i < tmp.Length; i++)
		{
			allVerticeList.Add(new OrderedVertice(tmp[i], i));
		}

		//获得去重顶点列表
		finalVerticeList = allVerticeList.Distinct(new ListCompare<OrderedVertice>((a, b) =>
		{
			return a.vertex == b.vertex;
		})).ToList();

		//相同位置顶点分组
		var subList = allVerticeList.GroupBy((ver) => ver.vertex);
		foreach (var list in subList)
		{
			verticesGroupList.Add(list.ToList());
		}

		//处理三角面
		allTriangleList = new List<List<int>>();
		int[] total = GetComponent<MeshFilter>().sharedMesh.triangles;
		List<int> triangle;
		for (int i = 0; i + 2 < total.Length; i += 3)
		{
			triangle = new List<int>();
			triangle.Add(total[i]);
			triangle.Add(total[i + 1]);
			triangle.Add(total[i + 2]);
			allTriangleList.Add(triangle);
		}

		//创建顶点标志物
		vertexCnt = finalVerticeList.Count;
		verticesObj = new GameObject[vertexCnt];
		for (int i = 0; i < vertexCnt; i++)
		{
			EditorUtility.DisplayProgressBar("创建顶点", "正在创建顶点（" + i + "/" + vertexCnt + "）......", 1.0f / vertexCnt * i);
			verticesObj[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			verticesObj[i].name = "Vertex";
			verticesObj[i].transform.localScale = new Vector3(vertexSize, vertexSize, vertexSize);
			verticesObj[i].transform.position = transform.localToWorldMatrix.MultiplyPoint3x4(finalVerticeList[i].vertex);
			verticesObj[i].GetComponent<MeshRenderer>().sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;
			//verticesObj[i].AddComponent<VertexIdentity>();
			//verticesObj[i].GetComponent<VertexIdentity>()._Identity = i;
			verticesObj[i].transform.SetParent(transform);
		}

		//网格重构
		Transform cloneObj = transform.Find(transform.name + "(Clone)");
		if (cloneObj != null)
			DestroyImmediate(cloneObj.gameObject);
		cloneObj = null;

		targetObj = new GameObject(transform.name + "(Clone)");

		targetObj.transform.position = transform.position;
		targetObj.transform.localScale = transform.localScale;
		targetObj.transform.rotation = transform.rotation;
		targetObj.transform.SetParent(transform);

		targetObj.AddComponent<MeshFilter>();
		targetObj.AddComponent<MeshRenderer>();
		targetObj.GetComponent<MeshRenderer>().sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;

		mesh = new Mesh();
		mesh.Clear();

		mesh.vertices = OutPutList(allVerticeList).ToArray();

		mesh.triangles = GetComponent<MeshFilter>().sharedMesh.triangles;
		mesh.uv = GetComponent<MeshFilter>().sharedMesh.uv;
		mesh.name = cloneName + transform.name;
		mesh.RecalculateNormals();
		targetObj.GetComponent<MeshFilter>().sharedMesh = mesh;

		GetComponent<MeshRenderer>().enabled = false;

		EditorUtility.ClearProgressBar();
	}

	void RefreshMesh()
	{
		if (mesh != null && verticesObj.Length == verticesGroupList.Count)
		{
			//刷新并应用顶点位置
			for (int i = 0; i < verticesObj.Length; i++)
			{
				for (int j = 0; j < verticesGroupList[i].Count; j++)
					//每组顶点都更新才行，否则会导致只有某一个面发生变化
					allVerticeList[verticesGroupList[i][j].order].vertex = targetObj.transform.worldToLocalMatrix.MultiplyPoint3x4(verticesObj[i].transform.position);
			}
		}
		//刷新网格
		mesh.vertices = OutPutList(allVerticeList).ToArray();
		mesh.RecalculateNormals();
	}
	void OnDestroy()
	{
		for (int i = 0; i < verticesObj.Length; i++)
		{
			EditorUtility.DisplayProgressBar("应用顶点", "正在应用顶点（" + i + "/" + vertexCnt + "）......", 1.0f / vertexCnt * i);
			if (verticesObj[i] != null)
				DestroyImmediate(verticesObj[i]);
		}
		EditorUtility.ClearProgressBar();
	}

	List<Vector3> OutPutList(List<OrderedVertice> list)
	{
		if (list == null)
			return null;
		List<Vector3> vecList = new List<Vector3>();
		foreach (var vec in list)
		{
			vecList.Add(vec.vertex);
		}
		return vecList;
	}

}

public class OrderedVertice
{
	public Vector3 vertex;
	public int order;

	public OrderedVertice(Vector3 vec, int o)
	{
		vertex = vec;
		order = o;
	}
}

public delegate bool CompareDelegate<T>(T x, T y);

public class ListCompare<T> : IEqualityComparer<T>
{
	private CompareDelegate<T> compare;
	public ListCompare(CompareDelegate<T> d)
	{
		this.compare = d;
	}

	public bool Equals(T x, T y)
	{
		if (compare != null)
		{
			return this.compare(x, y);
		}
		else
		{
			return false;
		}
	}

	public int GetHashCode(T obj)
	{
		return obj.ToString().GetHashCode();
	}
}