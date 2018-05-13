/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：VersionControlBase.cs
 * 简	述：版本控制基类。
 * 作	者：Fanki
 * 创建标识：2015/08/24
 */

using UnityEngine;
using System.IO;
using System.Collections;

namespace Qtz.Q5.Version
{
	public enum VersionControlType
	{
		kApp = 1,
		kDev = 2,
		kArt = 3,
	}

	public delegate void NextCallback(bool isSucess, string msg);

	public class VersionControlBase : MonoBehaviour
	{
		#region 生命周期函数
		/// <summary>
		/// 初始化
		/// </summary>
		public virtual void Init(GameManager ins)
		{
			mGameManager = ins;
			_isStartUpdate = false;
			if (mGameManager)
			{
                _pkgConfig = UtilCommon.AddComponent<PkgConfig>(mGameManager.gameObject) as PkgConfig;
                _pkgVersion = UtilCommon.AddComponent<PkgVersion>(mGameManager.gameObject) as PkgVersion;
                _pkgBundle = UtilCommon.AddComponent<PkgBundle>(mGameManager.gameObject) as PkgBundle;
                _pkgLua = UtilCommon.AddComponent<PkgLua>(mGameManager.gameObject) as PkgLua;
                _pkgFileList = UtilCommon.AddComponent<PkgFileList>(mGameManager.gameObject) as PkgFileList;
			}
		}

		/// <summary>
		/// 销毁函数
		/// </summary>
		public virtual void Destroy()
		{
			Destroy(_pkgConfig);
			Destroy(_pkgVersion);
			Destroy(_pkgBundle);
			Destroy(_pkgLua);
			Destroy(_pkgFileList);
			Destroy(this);
		}
		#endregion // 生命周期函数

		/// <summary>
		/// 开始更新
		/// </summary>
		public virtual void StartUpdate(bool isSucess = true, string msg = "")
		{
			// 子类实现
		}

		#region 图形界面
		/// <summary>
		/// ui界面提示，是否独立
		/// </summary>
		void OnGUI()
		{
            //if (_isStartUpdate)
            //{
            //    if (GUI.Button(new Rect(Screen.width / 2 - 180, Screen.height / 2, 180, 60), "更新最新的资源"))
            //    {
            //        _isStartUpdate = false;
            //        // 不用更新资源
            //        if (_uiCallback != null)
            //            _uiCallback(true, "");
            //    }

            //    if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 180, 60), "不更新资源"))
            //    {
            //        _isStartUpdate = false;
            //        // 不用更新资源
            //        VersionManager.Instance.OnResourceInited();
            //    }
            //}
		}
		#endregion //图形界面

		#region 回调方法
		/// <summary>
		/// 更新回调
		/// </summary>
		/// <param name="utype"></param>
		/// <param name="isWeb"></param>
		/// <param name="cb"></param>
		/// <returns></returns>
		public virtual IEnumerator OnNextUpdate(UpdateType utype, bool isWeb, NextCallback cb)
		{
			switch (utype)
			{
				case UpdateType.kConfig:
					_pkgConfig.SetUpdateOnWeb(isWeb);
					_pkgConfig.SetCallback(cb);
					StartCoroutine(_pkgConfig.OnDownload());
					break;
				case UpdateType.kVersion:
					_pkgVersion.SetUpdateOnWeb(isWeb);
					_pkgVersion.SetCallback(cb);
					StartCoroutine(_pkgVersion.OnDownload());
					break;
				case UpdateType.kBundle:
					_pkgBundle.SetUpdateOnWeb(isWeb);
					_pkgBundle.SetCallback(cb);
					StartCoroutine(_pkgBundle.OnDownload());
					break;
				case UpdateType.kLua:
					_pkgLua.SetUpdateOnWeb(isWeb);
					_pkgLua.SetCallback(cb);
					StartCoroutine(_pkgLua.OnDownload());
					break;
				case UpdateType.kFileList:
					_pkgFileList.SetUpdateOnWeb(isWeb);
					_pkgFileList.SetCallback(cb);
					StartCoroutine(_pkgFileList.OnDownload());
					break;
                case UpdateType.kSetting:   // Add By Pancake
                    string filePath = Application.persistentDataPath + "/" + Const.Setting_Coinfig_Path;
                    if (!File.Exists(filePath))
                    {
                        StartCoroutine(ToCopy(Const.Setting_Coinfig_Path, cb));
                    }
                    else
                    {
                        cb(true, string.Empty);
                    }
                    break;
				default:
					break;
			}
			yield break;
		}
		#endregion // 回调方法

		#region 下载完成检测
		/// <summary>
		/// 查找缺失文件并输出错误log
		/// </summary>
		/// <param name="dirPath"></param>
		/// <param name="filePath"></param>
		/// <param name="outLog"></param>
		/// <returns></returns>
		public virtual bool CheckDirectoryAndFileListExist(string dirPath, string filePath, string outLog = "资源缺失")
		{
            bool res = UtilCommon.IsDirectoryExist(dirPath);
			if (res)
			{
                res = UtilCommon.IsFileExist(filePath);
				if (!res)
					Log.PrintMsg(outLog);
			}
			return res;
		}

		/// <summary>
		/// 版本文件下载完成
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckVersionDownloadFinish()
		{
            return UtilCommon.IsFileExist(Application.persistentDataPath + "/VersionTemp.json");
		}

		/// <summary>
		/// bundle下载完成
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckBundleDownloadFinish()
		{
			return CheckDirectoryAndFileListExist(Application.persistentDataPath + "/resources", 
				Application.persistentDataPath + "/resources/FileList.json", "资源缺失FileList");
		}

		/// <summary>
		/// lua下载完成
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckLuaDownloadFinish()
		{
			return CheckDirectoryAndFileListExist(Application.persistentDataPath + "/lua",
				Application.persistentDataPath + "/lua/LuaFileList.json", "资源缺失LuaFileList");
		}

		/// <summary>
		/// 检查下载的文件是否存在
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckFileDownloadFinish()
		{
			bool resFile = CheckBundleDownloadFinish();
			bool luaFile = CheckLuaDownloadFinish();
			return ((Application.isEditor && resFile) || (!Application.isEditor && resFile && luaFile));
		}

		/// <summary>
		/// 检测版本号
		/// </summary>
		/// <returns></returns>
		public virtual int CheckVersionUpdate()
		{
			// 更新版本号文件成功后，对比版本
			// 获得当前版本
            string oldV = UtilCommon.GetVersion();
			// 新版本
            string newV = UtilCommon.ReadVersionFile(Application.persistentDataPath + "/VersionTemp.json");
            UtilCommon.SetTempVersion(newV);

            int resV = UtilCommon.VersionCompare(oldV, newV);
			Log.PrintMsg("old version is:" + oldV + " , new version is:" + newV);
			return resV;
		}

		/// <summary>
		/// 是否更新
		/// </summary>
		/// <returns></returns>
		public virtual bool HaveToUpdate()
		{
			return !CheckVersionUpdate().Equals(0);
		}
		#endregion // 下载完成检测

		/// <summary>
		/// 写入新版本的文件
		/// </summary>
		public virtual void OverrideNewVersion()
		{
			string desc = Application.persistentDataPath + "/Version.json";
			string src = Application.persistentDataPath + "/VersionTemp.json";
            if (UtilCommon.IsFileExist(src))
			{
				File.Copy(src, desc, true);
				File.Delete(src);
                UtilCommon.SetVersion(UtilCommon.ReadVersionFile(desc));
			}
		}

		#region 私有成员
		protected bool _isStartUpdate = false;
		/// <summary>
		/// gamemanager管理保存
		/// </summary>
		private GameManager mGameManager;
		/// <summary>
		/// 版本下载控制部分
		/// </summary>
		protected PkgConfig _pkgConfig;
		protected PkgVersion _pkgVersion;
		protected PkgBundle _pkgBundle;
		protected PkgLua _pkgLua;
		protected PkgFileList _pkgFileList;

		protected NextCallback _uiCallback;
		#endregion //私有成员

        #region Setting Add By Pancake
        IEnumerator ToCopy(string fileName, NextCallback cb)
        {
            string src = Const.getStreamingPath_for_www(fileName);
            string des = Application.persistentDataPath + "/" + fileName;
            WWW www = new WWW(src);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("www.error: " + www.error);
            }
            else
            {
                FileStream fsDes = new FileStream(des, FileMode.Create);
                fsDes.Write(www.bytes, 0, www.bytes.Length);
                fsDes.Flush();
                fsDes.Close();
            }
            www.Dispose();
            cb(true, string.Empty);
        }

        #endregion
    }
}
