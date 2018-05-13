using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FileList{

	public static FileList Deserialize(string json)
	{
		return LitJson.JsonMapper.ToObject<FileList>(json);
	}

	public static FileList LoadFromFile(string path)
	{
		if( !System.IO.File.Exists(path)) return new FileList();
		string json = System.IO.File.ReadAllText(path);
		return Deserialize(json);
	}

	public string Serialize()
	{
		LitJson.JsonWriter writer = new LitJson.JsonWriter();
		writer.PrettyPrint = true;
		LitJson.JsonMapper.ToJson(this, writer);
		return writer.TextWriter.ToString();
	}

	public void WriteToFile(string path)
	{
		string json = Serialize();
		System.IO.File.WriteAllText(path, json);
	}
	public string platform = "";
	public string version = "";

	// 从资源路径获取包名
	public Dictionary<string, string> res2bundleData = new Dictionary<string, string>();

	// 从包名获取场景名
	public Dictionary<string, string> bundle2sceneData = new Dictionary<string, string>();
}

public class LuaList
{

	public static LuaList Deserialize(string json)
	{
		return LitJson.JsonMapper.ToObject<LuaList>(json);
	}

	public static LuaList LoadFromFile(string path)
	{
		if (!System.IO.File.Exists(path)) return new LuaList();
		string json = System.IO.File.ReadAllText(path);
		return Deserialize(json);
	}

	public string Serialize()
	{
		LitJson.JsonWriter writer = new LitJson.JsonWriter();
		writer.PrettyPrint = true;
		LitJson.JsonMapper.ToJson(this, writer);
		return writer.TextWriter.ToString();
	}

	public void WriteToFile(string path)
	{
		string json = Serialize();
		System.IO.File.WriteAllText(path, json);
	}

	// Lua 文件与MD5表
	public Dictionary<string, string> LuaFileMD5Data = new Dictionary<string,string>();
}