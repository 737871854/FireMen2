/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：PkgConfig.cs
 * 简	述：更新Config文件类
 * 作	者：Fanki
 * 创建标识：2015/09/24
 */

using UnityEngine;
using System.Collections;

namespace Qtz.Q5.Version
{
	public class PkgConfig : Package
	{
		#region 初始化
		public override void Init()
		{
			base.Init();
			_fileList = "Config.json";
			_loadingPart = "PkgConfig";
		}
		#endregion //初始化

		#region 下载流程
		/// <summary>
		/// 下载version.json处理
		/// </summary>
		/// <param name="www"></param>
		/// <returns></returns>
		public override IEnumerator OnDownloadingFile(WWW www)
		{
            if(_localABMode==false)
            {
                EnsureFileExist(_localDataPath);
                WriteWwwToLocal(_localDataPath + "/" + _fileList, ref www);
            }
			OnDownloadFinish();
			yield break;
		}
		#endregion //下载流程

		#region 路径函数
		/// <summary>
		/// 本地获取路径
		/// </summary>
		/// <returns></returns>
		public override string GetPackageUrl()
		{
			return base.GetPackageUrl() + "lua/logic/config/";
		}
		#endregion //路径函数
	}
}
