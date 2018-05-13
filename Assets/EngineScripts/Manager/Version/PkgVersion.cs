/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：PkgVersion.cs
 * 简	 述：版本文件更新
 * 作	 者：Fanki
 * 创建标识：2015/08/24
 */

using UnityEngine;
using System.Collections;

namespace Qtz.Q5.Version
{
	public class PkgVersion : Package
	{
		#region 初始化
		public override void Init()
		{
			base.Init();
			_loadingPart = "PkgVersion";
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
			EnsureFileExist(_localDataPath);
			WriteWwwToLocal(_localDataPath + "/VersionTemp.json", ref www);
			OnDownloadFinish();
			yield break;
		}

		/// <summary>
		/// 下载成功处理
		/// </summary>
		public override void OnDownloadFinish()
		{
            string newV = UtilCommon.ReadVersionFile(Application.persistentDataPath + "/VersionTemp.json");
            UtilCommon.SetTempVersion(newV);
			base.OnDownloadFinish();
		}
		#endregion //下载流程
	}
}
