/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：PkgBundle.cs
 * 简	 述：Bundle文件更新
 * 作	 者：Fanki
 * 创建标识：2015/08/24
 */

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Qtz.Q5.Version
{
	public class PkgBundle : Package
	{
		#region 初始化
		public override void Init()
		{
			base.Init();
			_fileList = "assetbundle";
			//_fileList = "FileList.json";
			_loadingPart = "PkgBundle";
		}
		#endregion //初始化

		#region 下载流程
		/// <summary>
		/// 下载ab处理
		/// </summary>
		/// <param name="www"></param>
		/// <returns></returns>
		public override IEnumerator OnDownloadingFile(WWW hash_www)
		{
            if (_localABMode == false)
            {
                // 远程hash清单
                AssetBundle remoteHashBundle = AssetBundle.LoadFromMemory(hash_www.bytes); //hash_www.assetBundle;
                AssetBundleManifest remoteHashManifest = remoteHashBundle.LoadAllAssets()[0] as AssetBundleManifest;
                remoteHashBundle.Unload(false);
                // 本地hash清单
                AssetBundle localHashBundle = null;
                AssetBundleManifest localHashManifest = null;
                if (System.IO.File.Exists(_localDataPath + "assetbundle"))
                {
                    //localHashBundle		= AssetBundle.CreateFromFile(dataPath + "assetbundle");
                    byte[] dep_content = System.IO.File.ReadAllBytes(_localDataPath + "assetbundle");
                    localHashBundle = AssetBundle.LoadFromMemory(dep_content);
                    localHashManifest = localHashBundle.LoadAllAssets()[0] as AssetBundleManifest;
                }

                List<string> bundleNameList = new List<string>(remoteHashManifest.GetAllAssetBundles());

                // 对比远程列表与本地文件Hash，判断是否重新下载文件
                for (int i = 0; i < bundleNameList.Count; i++)
                {
                    string bundleName = bundleNameList[i];
                    if (string.IsNullOrEmpty(bundleName)) continue;
                    string localfile = (_localDataPath + bundleName).Trim();
                    string path = Path.GetDirectoryName(localfile);

                    EnsureFileExist(path);

                    string fileUrl = _urlPath + bundleName;// +"?v=" + random;
                    bool canUpdate = !File.Exists(localfile);
                    if (!canUpdate)
                    {
                        Hash128 remoteHash = remoteHashManifest.GetAssetBundleHash(bundleName);
                        Hash128 localHash = Hash128.Parse("0");

                        if (localHashManifest != null)
                            localHash = localHashManifest.GetAssetBundleHash(bundleName);

                        canUpdate = !remoteHash.Equals(localHash);

                        if (canUpdate && System.IO.File.Exists(localfile))
                            File.Delete(localfile);
                    }

                    if (canUpdate) //本地缺少文件
                    {
                        Log.PrintMsg(fileUrl);
                        WWW filewww = new WWW(fileUrl); yield return filewww;
                        if (filewww.error != null)
                        {
                            OnPrintMsg("error! :fileUrl= " + fileUrl + " " + path);
                            yield break;
                        }

                        WriteWwwToLocal(localfile, ref filewww);
                    }
                    //_loadingScene.SetPercentage(0.1f + 0.3f / bundleNameList.Count * i);
                }

                // 删除多余的数据包
                string[] all_bundles = Directory.GetFiles(_localDataPath, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string bundle_path in all_bundles)
                {
                    string bundle_name = Path.GetFileName(bundle_path);
                    if ((!bundleNameList.Contains(bundle_name))
                        && (bundle_name != _fileList)
                        && (bundle_name != "assetbundle")
                        && (bundle_name != "FileList.json"))
                        File.Delete(bundle_path);
                }

                // 销毁Hash Bundle
                if (localHashBundle != null)
                    localHashBundle.Unload(true);

                // 远程清单缓存到本地
                WriteWwwToLocal(_localDataPath + "assetbundle", ref hash_www);

                //如果asset载入的assetbundle.unload(false)，一定要调用下面的方法才能销毁;
                Resources.UnloadAsset(remoteHashManifest);
                remoteHashManifest = null;

                yield return new WaitForEndOfFrame();
            }
            OnDownloadFinish();
        }
		#endregion //下载流程

		#region 路径函数
		/// <summary>
		/// 网络部分路径
		/// </summary>
		/// <returns></returns>
		public override string GetWebUrl()
		{
            return base.GetWebUrl() + "res/v" + UtilCommon.GetTempVersion() + "/res_" + GetPlatformString() + "assetbundle/";
		}

		/// <summary>
		/// 多语言网络路径
		/// </summary>
		/// <returns></returns>
		public virtual string GetL10nWebUrl()
		{
			return GetWebUrl() + GetL10nPath();
		}

		/// <summary>
		/// 本地部分路径
		/// </summary>
		/// <returns></returns>
		public override string GetPackageUrl()
		{
			return base.GetPackageUrl() + GetPlatformString() + "assetbundle/";
		}

		/// <summary>
		/// 多语言本地路径
		/// </summary>
		/// <returns></returns>
		public virtual string GetL10nPackageUrl()
		{
			return GetPackageUrl() + GetL10nPath();
		}

		/// <summary>
		/// 默认保存路径
		/// </summary>
		/// <returns></returns>
		public override string GetBaseLocalDataPath()
		{
            return UtilCommon.resourcesPath;
		}

		/// <summary>
		/// 固定语言路径
		/// </summary>
		/// <returns></returns>
		public virtual string GetL10nPath()
		{
			return "L10n/" + Const.L10n + "/";
		}
		#endregion //路径函数
	}
}
