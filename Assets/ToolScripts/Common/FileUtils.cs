using System;
using UnityEngine;

// #define USE_MD5

/// <summary>
/// 文件工具类
/// </summary>
public class FileUtils
{

	static string DATA_ROOT_PATH = String.Format("{0}/", Application.streamingAssetsPath);

	/// <summary>
	/// 构造函数
	/// </summary>
	private FileUtils () { }

	/// <summary>
	/// 获取真实路径
	/// </summary>
	/// <returns>真实文件路径</returns>
	/// <param name="file">相对文件路径</param>
	public static string getPath(string file)
	{
		return DATA_ROOT_PATH + file;
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string getAbPath(string file)
    {
        return DATA_ROOT_PATH + "assetbundle/" + file;
    }

	/// <summary>
	/// 获取本地路径
	/// </summary>
	/// <returns>The local path.</returns>
	/// <param name="file">File.</param>
	public static string getLocalPath(string file)
	{
		return file;
	}
}

