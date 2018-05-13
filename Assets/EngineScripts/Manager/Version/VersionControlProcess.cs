/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：VersionControlProcess.cs
 * 简	述：版本控制基本流程类。
 * 作	者：Fanki
 * 创建标识：2015/09/24
 */

using UnityEngine;
using System.Collections;

namespace Qtz.Q5.Version
{
	/// <summary>
	/// 基本下载流程
	/// </summary>
	public class VersionControlProcess : VersionControlBase
	{
		#region 下载流程
		/// <summary>
		/// 开始
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public override void StartUpdate(bool isSucess = true, string msg = "")
		{
			// 下载配置
			//StartCoroutine(OnNextUpdate(UpdateType.kConfig, false, OnConfigFinish));
            StartCoroutine(OnNextUpdate(UpdateType.kSetting, false, OnConfigFinish));
			// 外部下载完配置文件
            //OnConfigFinish(isSucess, msg);
		}

        #region 本地下载部分
        /// <summary>
		/// 下载配置完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnConfigFinish(bool isSucess, string msg)
		{
            if (UtilCommon.IsFirstOpen())
			{
				StartCoroutine(OnNextUpdate(UpdateType.kVersion, false, OnDownloadLocalVersionFinish));
			}
			else
			{
				StartCoroutine(OnNextUpdate(UpdateType.kVersion, true, OnDownloadUpVersionFinish));
			}
		}

		/// <summary>
		/// 下载本地版本文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadLocalVersionFinish(bool isSucess, string msg)
		{
			StartCoroutine(OnNextUpdate(UpdateType.kFileList, false, OnDownloadLocalBundleFileListFinish));
		}
		
		/// <summary>
		/// 下载bundle filelist
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadLocalBundleFileListFinish(bool isSucess, string msg)
		{
			if (isSucess)
				StartCoroutine(OnNextUpdate(UpdateType.kBundle, false, OnDownloadLocalBundleFinish));
			else
			{
				Debug.LogWarning("本地下载bundle filelist文件出错");
				OnStartGame();
			}
		}

		/// <summary>
		/// 下载本地Bundle文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadLocalBundleFinish(bool isSucess, string msg)
		{
			_pkgBundle.SetLocalDataPath(_pkgBundle.GetBaseLocalDataPath() + _pkgBundle.GetL10nPath());
			_pkgBundle.SetSpecialPackageUrl(_pkgBundle.GetL10nPackageUrl());
			StartCoroutine(OnNextUpdate(UpdateType.kBundle, false, OnDownloadL10nLocalBundleFinish));
		}

		/// <summary>
		/// 下载本地语言包Bundle文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadL10nLocalBundleFinish(bool isSucess, string msg)
		{
			StartCoroutine(OnNextUpdate(UpdateType.kLua, false, OnDownloadLocalLuaFinish));
		}

		/// <summary>
		/// 下载本地Lua文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadLocalLuaFinish(bool isSucess, string msg)
		{
			if (Const.UpdateMode)
				StartCoroutine(OnNextUpdate(UpdateType.kVersion, true, OnDownloadUpVersionFinish));
			else
				OnStartGame();
		}
		#endregion //本地下载部分

		#region 网络下载部分
		/// <summary>
		/// 下载网络版本文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadUpVersionFinish(bool isSucess, string msg)
		{
			_updateVersion = isSucess;
			if (isSucess)
			{
				Debug.Log("hot fit update http version file success.");
				if (HaveToUpdate())
				{
					_isStartUpdate = true;
					_uiCallback = UiCallback;
				}
				else
					OnStartGame();
			}
			else
			{
				Debug.Log("hot fit update http version file fail.");
				OnStartGame();
			}
		}

		/// <summary>
		/// 设置ui操作回调
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void UiCallback(bool isSucess, string msg)
		{
			StartCoroutine(OnNextUpdate(UpdateType.kFileList, true, OnDownloadUpBundleFileListFinish));
		}

		public virtual void OnDownloadUpBundleFileListFinish(bool isSucess, string msg)
		{
			if (isSucess)
			{
				_pkgBundle.SetLocalDataPath(_pkgBundle.GetBaseLocalDataPath());
				StartCoroutine(OnNextUpdate(UpdateType.kBundle, true, OnDownloadUpBundleFinish));
			}
			else
			{
				Debug.LogWarning("网络下载bundle filelist文件出错");
				OnStartGame();
			}
		}

		/// <summary>
		/// 下载网络Bundle文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadUpBundleFinish(bool isSucess, string msg)
		{
			_updateBundle = isSucess;
			_pkgBundle.SetLocalDataPath(_pkgBundle.GetBaseLocalDataPath() + _pkgBundle.GetL10nPath());
			_pkgBundle.SetSpecialWebUrl(_pkgBundle.GetL10nWebUrl());
			StartCoroutine(OnNextUpdate(UpdateType.kBundle, true, OnDownloadL10nUpBundleFinish));
		}

		/// <summary>
		/// 下载网络语言包Bundle文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadL10nUpBundleFinish(bool isSucess, string msg)
		{
			_updateBundle = isSucess;
			StartCoroutine(OnNextUpdate(UpdateType.kLua, true, OnDownloadUpLuaFinish));
		}

		/// <summary>
		/// 下载网络Lua文件完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnDownloadUpLuaFinish(bool isSucess, string msg)
		{
			_updateLua = isSucess;
			OnFinish(isSucess, msg);
		}

		#endregion //网络下载部分

		/// <summary>
		/// 下载完成
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		public virtual void OnFinish(bool isSucess, string msg)
		{
			if (!_updateVersion)
				Debug.LogWarning("版本文件下载失败。");
			if (!_updateBundle)
				Debug.LogWarning("网络下载bundle错误。");
			if (!_updateLua)
				Debug.LogWarning("网络下载lua错误。");

			if (CheckFileDownloadFinish() && _updateBundle && _updateLua)
			{
				OverrideNewVersion();
			}
			OnStartGame();
		}

		/// <summary>
		/// 开始游戏
		/// </summary>
		public virtual void OnStartGame()
		{
			VersionManager.Instance.OnResourceInited();
		}
		#endregion // 下载流程

		#region 控制变量
		/// <summary>
		/// 检查是更新是否成功
		/// </summary>
		protected bool _updateVersion = true;
		protected bool _updateBundle = true;
		protected bool _updateLua = true;
		#endregion //控制变量
	}
}
