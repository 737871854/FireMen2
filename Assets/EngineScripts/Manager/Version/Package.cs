/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：Package.cs
 * 简	述：更新文件父类
 * 作	者：Fanki
 * 创建标识：2015/08/24
 * 加入SettingConfig文件操作 2017/10.20
 */  

using UnityEngine;
using System.Collections;
using System.IO;

namespace Qtz.Q5.Version
{
	/// <summary>
	/// 更新文件类型
	/// </summary>
	public enum UpdateType
	{
        kSetting, // Add By Pancake
		kConfig,
		kFileList,
		kVersion,
		kBundle,
		kLua,
	}

	public class Package : MonoBehaviour
	{
		#region 创建初始化
        ///// <summary>
        ///// 构造
        ///// </summary>
        //public Package()
        //{
        //    Init();
        //}

        void Awake()
        {
            Init();
        }

		/// <summary>
		/// 初始化
		/// </summary>
		public virtual void Init()
		{
			_localDataPath = GetBaseLocalDataPath();
			SetUpdateOnWeb(false);
		}

		/// <summary>
		/// 设置一次默认路径
		/// </summary>
		/// <param name="isWel"></param>
		public virtual void SetUpdateOnWeb(bool isWel)
		{
			_isWeb = isWel;
			if (_isWeb)
				_urlPath = GetSpecialWebUrl();
			else
				_urlPath = GetSpecialPackageUrl();
		}
		#endregion //创建初始化

		#region 下载流程
		/// <summary>
		/// 开始下载
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerator OnDownload()
		{
			OnPrintMsg(_urlPath);

			WWW www = GetWwwFile(_urlPath + _fileList);
			yield return www;

			if (www.error != null)
			{
				Debug.LogWarning("error .. " + www.error);
				OnDownloadFailed();
				yield break;
			}

			StartCoroutine(OnDownloadingFile(www));
		}

		/// <summary>
		/// 下载中间过程
		/// </summary>
		/// <param name="www"></param>
		/// <returns></returns>
		public virtual IEnumerator OnDownloadingFile(WWW www)
		{
			OnDownloadFinish();
			yield return www;
		}

		/// <summary>
		/// 外部调用开始下载方法
		/// </summary>
		public virtual void OnStartDownload()
		{
			StartCoroutine(OnDownload());
		}

		/// <summary>
		/// 下载失败，线程直接断开的
		/// </summary>
		public virtual void OnDownloadFailed()
		{
			OnCallBack(false);
		}

		/// <summary>
		/// 下载成功
		/// </summary>
		public virtual void OnDownloadFinish()
		{
			OnCallBack(true);
		}
		#endregion //下载流程

		#region 回调方法
		/// <summary>
		/// 设置回调
		/// </summary>
		/// <param name="cb"></param>
		public virtual void SetCallback(NextCallback cb)
		{
			callback = cb;
		}

		/// <summary>
		/// 下一步默认更新
		/// </summary>
		public virtual void OnCallBack(bool isSucess, string msg = "")
		{
			callback(isSucess, msg);
		}
		#endregion //回调方法

		#region 辅助函数
		/// <summary>
		/// 获得www文件
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public virtual WWW GetWwwFile(string filePath)
		{
			if (Debug.isDebugBuild)
				Debug.LogWarning("Version File Load Update---->>>" + filePath);
			return new WWW(filePath);
		}

		/// <summary>
		/// 确定文件存在
		/// </summary>
		/// <param name="filePath"></param>
		public virtual void EnsureFileExist(string filePath)
		{
			if (!Directory.Exists(filePath))
				Directory.CreateDirectory(filePath);
		}

		/// <summary>
		/// 写入网络数据
		/// </summary>
		/// <param name="path"></param>
		/// <param name="www"></param>
		public virtual void WriteWwwToLocal(string path, ref WWW www)
		{
			File.WriteAllBytes(path, www.bytes);
			www.Dispose();
		}

		/// <summary>
		/// 打印
		/// </summary>
		/// <param name="msg"></param>
		protected virtual void OnPrintMsg(string msg)
		{
			string totolMsg = _loadingPart + " Update msg ---->>> " + msg;
			//Debug.LogWarning(totolMsg);
			Log.PrintMsg(totolMsg);
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public virtual void DestroyPackage()
		{
			Destroy(this);
		}
		#endregion //辅助函数

		#region 路径函数
		/// <summary>
		/// 指定WebUrl路径获得
		/// </summary>
		/// <returns></returns>
		public virtual string GetSpecialWebUrl()
		{
			return string.IsNullOrEmpty(_spWebUrl) ? GetWebUrl() : _spWebUrl;
		}

		/// <summary>
		/// 指定PackageUrl路径获得
		/// </summary>
		/// <returns></returns>
		public virtual string GetSpecialPackageUrl()
		{
			return string.IsNullOrEmpty(_spPackageUrl) ? GetPackageUrl() : _spPackageUrl;
		}

		/// <summary>
		/// 网络路径
		/// </summary>
		/// <returns></returns>
		public virtual string GetWebUrl()
		{
			return Const.WebUrl;
		}

		/// <summary>
		/// 本地包路径
		/// </summary>
		/// <returns></returns>
		public virtual string GetPackageUrl()
		{
			return Const.PackageUrl;
		}

		/// <summary>
		/// 不同平台选择
		/// </summary>
		/// <returns></returns>
		public virtual string GetPlatformString()
		{
			string url = "";
			switch (Application.platform)
			{
				case RuntimePlatform.IPhonePlayer:
					url += "iphone/";
					break;
				case RuntimePlatform.Android:
					url += "android/";
					break;
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WindowsEditor:
					url += "pc/";
					break;
				case RuntimePlatform.OSXEditor:
					url += "iphone/";
					break;
			}
			return url;
		}

		/// <summary>
		/// 设定指定的webUrl路径
		/// </summary>
		/// <param name="webUrl"></param>
		public virtual void SetSpecialWebUrl(string webUrl)
		{
			_spWebUrl = webUrl;
		}

		/// <summary>
		/// 设定指定的packageUrl路径
		/// </summary>
		/// <param name="packageUrl"></param>
		public virtual void SetSpecialPackageUrl(string packageUrl)
		{
			_spPackageUrl = packageUrl;
		}

		/// <summary>
		/// 修改保存路径
		/// </summary>
		/// <param name="localPath"></param>
		public virtual void SetLocalDataPath(string localPath)
		{
			_localDataPath = localPath;
		}

		/// <summary>
		/// 获得默认路径
		/// </summary>
		/// <returns></returns>
		public virtual string GetBaseLocalDataPath()
		{
			return Application.persistentDataPath;
		}
		#endregion //路径

		#region 变量区
		/// <summary>
		/// 是否从网上下载
		/// </summary>
		protected bool _isWeb = false;

		/// <summary>
		/// 本地路径
		/// </summary>
		protected string _localDataPath;

		/// <summary>
		/// 网络路径
		/// </summary>
		protected string _urlPath;

		/// <summary>
		/// 是否有指定网络路径
		/// </summary>
		protected string _spWebUrl;

		/// <summary>
		/// 是否有指定包路径
		/// </summary>
		protected string _spPackageUrl;

		/// <summary>
		/// 下载list文件名
		/// </summary>
		protected string _fileList = Const.VersionFile;

        /// <summary>
        /// 是否为直接调用本地资源更新模式，是则跳过更新持久化资源而直接调用LocalTestWebUrl资源
        /// </summary>
        protected bool _localABMode = Const.LocalABMode;

        /// <summary>
        /// 类名，用了判断运行到的位置而已，没有太大作用
        /// </summary>
        protected string _loadingPart = "";

		/// <summary>
		/// 回调
		/// </summary>
		protected NextCallback callback;
		#endregion //变量区
	}
}
