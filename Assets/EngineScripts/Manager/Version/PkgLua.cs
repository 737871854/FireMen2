/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：PkgLua.cs
 * 简	 述：Lua文件更新
 * 作	 者：Fanki
 * 创建标识：2015/08/24
 */

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Qtz.Q5.Version
{
	public class PkgLua : Package
	{
		#region 初始化
		public override void Init()
		{
			base.Init();
			_fileList = "LuaFileList.json";
			_loadingPart = "PkgLua";
		}
		#endregion //初始化

		#region 下载流程
		/// <summary>
		/// 下载lua文件
		/// </summary>
		/// <param name="www"></param>
		/// <returns></returns>
		public override IEnumerator OnDownloadingFile(WWW www)
		{
			LuaList remoteLuaList = LuaList.Deserialize(www.text);
			LuaList localLuaList = LuaList.LoadFromFile(_localDataPath + "LuaFileList.json");

			//int i = 0;
			WWW luawww = null;
			foreach (KeyValuePair<string, string> md5Pair in remoteLuaList.LuaFileMD5Data)
			{
				string luaFileName = md5Pair.Key;
				if (string.IsNullOrEmpty(luaFileName)) continue;
				string localfile = (_localDataPath + luaFileName).Trim();
				string path = Path.GetDirectoryName(localfile);

				EnsureFileExist(path);

				string fileUrl = _urlPath + luaFileName;// +"?v=" + random;
				bool canUpdate = !File.Exists(localfile);
				if (!canUpdate)
				{
					string remoteMd5 = remoteLuaList.LuaFileMD5Data[luaFileName].Trim();
					string localMd5 = "";
					if (localLuaList.LuaFileMD5Data.ContainsKey(luaFileName))
						localMd5 = localLuaList.LuaFileMD5Data[luaFileName];

					canUpdate = !remoteMd5.Equals(localMd5);
					if (canUpdate) File.Delete(localfile);
				}

				if (canUpdate) //本地缺少文件
				{
					Log.PrintMsg(fileUrl);

					luawww = GetWwwFile(fileUrl);
					yield return luawww;

					if (luawww.error != null)
					{
						OnPrintMsg("error! :fileUrl=" + fileUrl + " " + path);
						yield break;
					}

					WriteWwwToLocal(localfile, ref luawww);
				}

				//_loadingScene.SetPercentage(0.6f + 0.3f / remoteLuaList.LuaFileMD5Data.Count * i);
				//i++;
			}

			EnsureFileExist(_localDataPath);
			WriteWwwToLocal(_localDataPath + _fileList, ref www);

			yield return new WaitForEndOfFrame();

			OnDownloadFinish();
		}
		#endregion //下载流程

		#region 路径函数
		/// <summary>
		/// 网络获取路径
		/// </summary>
		/// <returns></returns>
		public override string GetWebUrl()
		{
            return base.GetWebUrl() + "res/v" + UtilCommon.GetTempVersion() + "/lua/";
		}

		/// <summary>
		/// 本地获取路径
		/// </summary>
		/// <returns></returns>
		public override string GetPackageUrl()
		{
			return base.GetPackageUrl() + "lua/";
		}

		/// <summary>
		/// 默认保存路径
		/// </summary>
		/// <returns></returns>
		public override string GetBaseLocalDataPath()
		{
			return Application.persistentDataPath + "/lua/";//UtilCommon.LuaPath();
		}
		#endregion //路径函数
	}
}
