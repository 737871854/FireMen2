/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：PkgFileList.cs
 * 简	 述：FileList文件更新
 * 作	 者：Fanki
 * 创建标识：2015/10/12
 */

using UnityEngine;
using System.Collections;

namespace Qtz.Q5.Version
{
	public class PkgFileList : PkgBundle
	{
		#region 初始化
		public override void Init()
		{
			base.Init();
			_fileList = "FileList.json";
			_loadingPart = "PkgFileList";
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
                WriteWwwToLocal(_localDataPath + _fileList, ref www);
            }
			OnDownloadFinish();
			yield break;
		}

		/// <summary>
		/// 设置下载文件名
		/// </summary>
		/// <param name="fileName"></param>
		public void SetFileName(string fileName)
		{
			_fileList = fileName;
		}
		#endregion //下载流程
	}
}
