/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：VersionControlArt.cs
 * 简	 述：开发者版本控制类。
 * 作	 者：Fanki
 * 创建标识：2015/09/24
 */

using UnityEngine;
using System.Collections;

namespace Qtz.Q5.Version
{
	public class VersionControlDev : VersionControlProcess
	{
		/// <summary>
		/// 设置本地bundle路径
		/// </summary>
		/// <param name="ins"></param>
		public override void Init(GameManager ins)
		{
			base.Init(ins);
			// 设置本地路径
			_pkgFileList.SetSpecialPackageUrl(Const.LocalTestWebUrl + _pkgBundle.GetPlatformString() + "assetbundle/");
			_pkgBundle.SetSpecialPackageUrl(Const.LocalTestWebUrl + _pkgBundle.GetPlatformString() + "assetbundle/");
		}

		/// <summary>
		/// 读配置和一般逻辑不一样
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public override void OnConfigFinish(bool isSucess, string msg)
		{
			StartCoroutine(OnNextUpdate(UpdateType.kFileList, false, OnDownloadLocalBundleFileListFinish));
		}

		/// <summary>
		/// 下载本地Bundle文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public override void OnDownloadLocalBundleFinish(bool isSucess, string msg)
		{
			_pkgBundle.SetLocalDataPath(_pkgBundle.GetBaseLocalDataPath() + _pkgBundle.GetL10nPath());
			_pkgBundle.SetSpecialPackageUrl(Const.LocalTestWebUrl + _pkgBundle.GetPlatformString() + "assetbundle/" + _pkgBundle.GetL10nPath());
			StartCoroutine(OnNextUpdate(UpdateType.kBundle, false, OnDownloadL10nLocalBundleFinish));
		}

		#region 下载bundle文件
		/// <summary>
		/// 下载bundle后不用下载lua
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public override void OnDownloadL10nLocalBundleFinish(bool isSucess, string msg)
		{
			if (Const.UpdateMode)
				StartCoroutine(OnNextUpdate(UpdateType.kVersion, true, OnDownloadUpVersionFinish));
			else
				OnStartGame();
		}

		/// <summary>
		/// 不显示ui框，直接更新
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public override void OnDownloadUpVersionFinish(bool isSucess, string msg)
		{
			_updateVersion = isSucess;
			if (isSucess)
			{
				StartCoroutine(OnNextUpdate(UpdateType.kFileList, true, OnDownloadUpBundleFileListFinish));
			}
			else
				OnFinish(isSucess, "Version update file fail.");
		}

		/// <summary>
		/// 网上下载完bundle也不用检查lua了，读本地
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadL10nUpBundleFinish(bool isSucess, string msg)
		{
			_updateBundle = isSucess;
			OnFinish(isSucess, msg);
		}
		#endregion //下载bundle文件
	}
}
