using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// 读取配置
/// </summary>
public class ConfigReader<T> where T : BaseConfig
{
	public ConfigReader(string name)
	{
		this.configName = name;
	}

    /// <summary>
    /// 总配置
    /// </summary>
    /// <returns>The config.</returns>
    public Dictionary<string, T> LoadConfig()
    {
		string path = GetXmlFilePath(this.configName);
		if (!File.Exists(path))
		{
			Debug.LogError("path not exist");
			return null;
		}
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("Root");

        foreach(var node in root.ChildNodes)
        {
            XmlElement e = node as XmlElement;
            T obj = Fill(e);
            dic.Add(e.GetAttribute("name").ToString(), obj);
        }

        return dic;
    }

	private T Fill(XmlElement e)
	{
		T obj = Activator.CreateInstance<T>();
		var fields = typeof(T).GetFields();
		string nodeName = string.Empty;
		foreach (var field in fields)
		{
			nodeName = e.GetAttribute(field.Name);
			field.SetValue(obj, Convert.ChangeType(nodeName, field.FieldType));
		}

		return obj;
	}

	private XElement LoadXml()
	{
		string path = GetXmlFilePath(this.configName);
		if (!File.Exists(path))
		{
			Debug.LogError("path not exist");
			return null;
		}
		XElement xml = XElement.Load(path);
		return xml;
	}

	private string GetXmlFilePath(string fileName)
	{
		fileName = Path.Combine(Application.dataPath + "/Resources/Configs/", fileName);

		if (!fileName.EndsWith(".xml") && !fileName.EndsWith(".bytes"))
		{
			fileName += ".xml";
		}
		return fileName;
	}

	/// <summary>
	/// 配置名称
	/// </summary>
	private string configName;
    private XElement xFile = null;
    private Dictionary<string, T> dic = new Dictionary<string, T>();
}