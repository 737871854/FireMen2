/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：Config.cs
 * 简	 述：配置文件读取
 * 作	 者：Fanki
 * 创建标识：2015/09/21
 */

using UnityEngine;
using System;

public class Config
{
	#region 单例模式
	static private Config m_instance;
	static public Config Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = new Config();
				m_instance.Init();
			}
			return m_instance;
		}
	}

	static public Config Reload()
	{
		m_instance = new Config();
		m_instance.Init();
		return m_instance;
	}
	#endregion //单例模式

	#region 私有成员
	/// <summary>
	/// 配置信息
	/// </summary>
	private LitJson.JsonData m_config;
	#endregion //公共成员

	/// <summary>
	/// 初始化
	/// </summary>
	void Init()
	{
		string jsonPath = Application.persistentDataPath + "/" + Const.ConfigFile;
		if (!System.IO.File.Exists(jsonPath))
		{
			Debug.LogError("Can't get Config.json file.");
			return;
		}

		string content = System.IO.File.ReadAllText(jsonPath);
		LitJson.JsonReader reader = new LitJson.JsonReader(content);
		m_config = LitJson.JsonMapper.ToObject(reader);

		if (m_config == null)
			Debug.LogError("Config Json Data structure error.");
		else
		{
			try
			{
				Const.UpdateMode = (bool)m_config["update_mode"];
			}
			catch (Exception err)
			{
				Debug.LogError(err.Message);
			}
		}
	}

	#region 公共接口
	/// <summary>
	/// 返回配置文件
	/// </summary>
	/// <returns></returns>
	public LitJson.JsonData GetConfig()
	{
		return m_config;
	}

	/// <summary>
	/// 获得配置文件中的一个key对应的
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public LitJson.JsonData GetConfigByKey(string key)
	{
		try
		{
			return m_config[key];
		}
		catch (Exception err)
		{
			Debug.LogError(err.Message);
			return null;
		}
	}

	/// <summary>
	/// 获得本地资源路径
	/// </summary>
	/// <returns></returns>
	public string GetLocalBundlePath()
	{
		try
		{
			return (string)m_config["bundle_path"];
		}
		catch (Exception err)
		{
			Debug.LogError(err.Message);
			return "";
		}
	}

	/// <summary>
	/// 获得配置更新方式方案类型
	/// </summary>
	/// <returns></returns>
	public int GetVersionControlType()
	{
		try
		{
			return (int)m_config["version_control"];
		}
		catch (Exception err)
		{
			Debug.LogError(err.Message);
			return 1;
		}
	}
	#endregion //公共成员
}
