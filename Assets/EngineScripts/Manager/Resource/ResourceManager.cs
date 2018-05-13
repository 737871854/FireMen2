/*
 * Copyright  (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ResourceManager.cs
 * 
 * 简    介:    资源加载管理类
 * 
 * 创建标识：   Pancake整理 2017/10/23 借鉴  (2015,广州擎天柱网络科技有限公司) 
 * 
 * 修改描述：   
 * 
 */


using UnityEngine;
using System.Collections;
using ResourceMisc;
using System.Collections.Generic;
using System;
using System.Xml;
using UnityEngine.Assertions;
using System.Text.RegularExpressions;

class AssetBundleInfo
{
    int ref_count;

    public string bundleName;
    public string assetName;
    public List<string> dependencies;

    public void AddDependence(string dep)
    {
        if (dependencies == null)
        {
            dependencies = new List<string>();
        }
        dependencies.Add(dep);
    }
}

public class ResourceManager : MonoBehaviour
{

    // AssetBundle Name， Information 获取每个AssetBundle文件以及依赖关系
    private Dictionary<string, AssetBundleInfo> fileMap = new Dictionary<string, AssetBundleInfo>();

    List<string> _sceneBundles = new List<string>();

    // 已经加载的AssetBundle缓存
    Dictionary<string, BundleWrapper> m_bundleCache = new Dictionary<string, BundleWrapper>();

    // 已经加载的Asset缓存
    Dictionary<string, AssetWrapper> m_assetWrapperCache = new Dictionary<string, AssetWrapper>();

    bool m_isLocalization = false;
    #region 外部调用接口
    /// <summary>
    ///  初始化加载器
    /// </summary>
    /// <param name="fileList">文件列表</param>
    /// <param name="initOK">初始化完成回调</param>
    public void Initialize(string fileList, Action initOK)
    {
        // 
        string path = FileUtils.getAbPath(Const.AssetBundleXMLName);
        if (!System.IO.File.Exists(path))
        {
            return;
        }
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList nodeList = doc.SelectSingleNode("files").ChildNodes;
        foreach (XmlElement xe in nodeList)
        {
            AssetBundleInfo info = new AssetBundleInfo();
            string _fileName = xe.SelectSingleNode("fileName").InnerText;
            info.assetName = xe.SelectSingleNode("assetName").InnerText;
            info.bundleName = xe.SelectSingleNode("bundleName").InnerText;
            XmlNode deps = xe.SelectSingleNode("deps");
            if (null != deps)
            {
                XmlNodeList depList = deps.ChildNodes;
                foreach (XmlElement _xe in depList)
                {
                    info.AddDependence(_xe.InnerText);
                }
            }
            fileMap.Add(_fileName.Substring("Assets/RawRes/".Length), info);
        }
    }

    /// <summary>
    /// 加载Json文件
    /// </summary>
    /// <param name="url"></param>
    /// <param name="completeHandler"></param>
    public void LoadRes(string url, LoadHandler completeHandler)
    {
        string json = IOHelper.OpenText(Const.GetLocalFileUrl(url));
        if (null != completeHandler)
            completeHandler(new LoadedData(json, url, url));
    }

    private UnityEngine.Object obj;
    public UnityEngine.Object LoadMovie(string name)
    {
        obj = null;

        //string path = Const.GetLocalFileUrl("Movie/" + name);
        //if (!File.Exists(path))
        //{
        //    Debug.LogError(path + " is not exit!");
        //    return obj;
        //}

        //StartCoroutine(DownAsset(path));
        obj = Resources.Load(name);

        return obj;
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="assetPath"></param>
    /// <param name="assetType"></param>
    /// <returns></returns>
    public AssetWrapper LoadAsset(string fileName, System.Type assetType)
    {
        // 找不到指定的AssetBundle配置信息
        if (!fileMap.ContainsKey(fileName))
        {
            Debugger.LogError("fileMap.ContainsKey(" + fileName + ") == false");
            return null;
        }

        AssetBundleInfo info = fileMap[fileName];
        string assetPath = info.assetName;
        string bundleName = info.bundleName;

        // 从缓存中获取AssetWrapper
        if (m_assetWrapperCache.ContainsKey(assetPath))
        {
            m_assetWrapperCache[assetPath].AddRef();
            return m_assetWrapperCache[assetPath];
        }

        // 从bundle缓存中实例化一个AssetWrapper
        if (m_bundleCache.ContainsKey(bundleName))
        {
            UnityEngine.Object obj = m_bundleCache[bundleName].GetBundle().LoadAsset(bundleName, assetType);
            AssetWrapper ret = new AssetWrapper(obj, assetPath, assetType, bundleName, IsSceneBundle(bundleName));
            m_assetWrapperCache.Add(bundleName, ret);
            m_bundleCache[bundleName].AddRefAsset(bundleName);
            return ret;
        }

        SetLocalization(false);
        return LoadRes(info, assetType);
    }

    /// <summary>
    /// 释放指定资源
    /// </summary>
    /// <param name="asset"></param>
    public void ReleaseAsset(AssetWrapper asset)
    {
        asset.Release();
        if (asset.GetRefCount() < 1)
        {
            // Destroy
            DestroyAsset(asset);
        }
    }

    /// <summary>
    /// 释放所有资源
    /// </summary>
    public void ClearAllAsset()
    {
        Debugger.Log("ResourceManager ClearAllAsset");
        m_assetWrapperCache.Clear();

        foreach(KeyValuePair<string, BundleWrapper> kv in m_bundleCache)
        {
            kv.Value.GetBundle().Unload(true);
        }
        m_bundleCache.Clear();
        Resources.UnloadUnusedAssets();
    }

    public IEnumerator LoadAssetAsync(string asset_path, System.Type asset_type)
    {
        if (!Application.isEditor && !fileMap.ContainsKey(asset_path))
            yield break;

        // see if there's a cache shot
        if (m_assetWrapperCache.ContainsKey(asset_path))
        {
            m_assetWrapperCache[asset_path].AddRef();
            yield break;
        }
        if (!Application.isEditor)
        {
            //yield return StartCoroutine(LoadFromBundleAsync(asset_path, asset_type));
            yield return StartCoroutine(LoadFromLocalAsync(asset_path, asset_type));
        }
        else
        {
            yield return StartCoroutine(LoadFromLocalAsync(asset_path, asset_type));
        }
    }

    IEnumerator LoadFromLocalAsync(string asset_path, System.Type asset_type)
    {
        yield return new WaitForEndOfFrame();
    }

    IEnumerator LoadFromBundleAsync(string asset_path, System.Type asset_type)
    {
        yield return new WaitForEndOfFrame();
    }
    #endregion
    private AssetWrapper LoadRes(AssetBundleInfo info, System.Type assetType)
    {
        string assetPath = info.assetName;
        string bundleName = info.bundleName;
        if (info.dependencies != null)
        {
            foreach (string dep in info.dependencies)
            {
                if (m_bundleCache.ContainsKey(dep))
                {
                    m_bundleCache[dep].AddRefAsset(bundleName);
                    continue;
                }
                else
                {
                    if (string.IsNullOrEmpty(dep))
                    {
                        Debugger.LogError("AB name " + bundleName + " 依赖名空！");
                        continue;
                    }

                    BundleWrapper loaded_bundle_bw = LoadBundle(dep);
                    loaded_bundle_bw.AddRefAsset(bundleName);
                    UnityEngine.Object temp = loaded_bundle_bw.GetBundle().mainAsset;
                    if (temp != null)
                    {
                        Debugger.Log("dep_bundle mainAsset: " + temp.name);
                    }
                }
            }
        }

        if (m_bundleCache.ContainsKey(bundleName))
        {
            UnityEngine.Object obj = m_bundleCache[bundleName].GetBundle().LoadAsset(assetPath, assetType);
            AssetWrapper ret = new AssetWrapper(obj, assetPath, assetType, bundleName, IsSceneBundle(bundleName));
            m_assetWrapperCache.Add(assetPath, ret);
            m_bundleCache[bundleName].AddRefAsset(assetPath);
            return ret;
        }

        // load target bundle
        BundleWrapper target_bundle_bw = LoadBundle(bundleName);
        target_bundle_bw.AddRefAsset(assetPath);
        // load asset
        AssetWrapper ret_aw;
        if (IsSceneBundle(bundleName))
        {
            ret_aw = new AssetWrapper(null, assetPath, assetType, bundleName, true);
        }else
        {
            string realPath = assetPath;
            //if (m_isLocalization)
            //{
            //    realPath = Regex.Replace(realPath, "^Assets/", "Assets/L10n/" + Const.L10n + "/");
            //}
            UnityEngine.Object obj = target_bundle_bw.GetBundle().LoadAsset(realPath, assetType);
            if (obj == null)
            {
                Debugger.LogError("targetBundle Wrong!");
                return null;
            }
            ret_aw = new AssetWrapper(obj, assetPath, assetType, bundleName, false);
        }
        m_assetWrapperCache.Add(assetPath, ret_aw);

        return ret_aw;
    }

    private BundleWrapper LoadBundle(string bundleName)
    {
        string target_local_path = FileUtils.getAbPath(bundleName);
        if(!System.IO.File.Exists(target_local_path))
        {
            if(m_isLocalization)
            {
                SetLocalization(false);
                target_local_path = FileUtils.getAbPath(bundleName);
                SetLocalization(true);
            }
            else
            {
                return null;
            }
        }
        byte[] target_content = System.IO.File.ReadAllBytes(target_local_path);
        AssetBundle target_bundle = AssetBundle.LoadFromMemory(target_content);
        Assert.IsNotNull<AssetBundle>(target_bundle, "assetbundle为空 " + bundleName);
        BundleWrapper target_bundle_bw = new BundleWrapper(bundleName, target_bundle);
        m_bundleCache.Add(bundleName, target_bundle_bw);
        return target_bundle_bw;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="asset"></param>
    private void DestroyAsset(AssetWrapper asset)
    {
        string assetPath = asset.GetAssetPath();
        string[] dps = asset.GetDps();
        string mainBundle = asset.GetBundleName();
        if (m_bundleCache.ContainsKey(mainBundle))
        {
            m_bundleCache[mainBundle].DecRefAsset(assetPath);
            if (dps != null)
            {
                for (int i = 0; i < dps.Length; ++i)
                {
                    string dep_name = dps[i];
                    if (m_bundleCache.ContainsKey(dep_name))
                    {
                        m_bundleCache[dep_name].DecRefAsset(assetPath);
                    }
                }
            }
        }else{
            Debugger.LogWarning("AssetWrapper :" + assetPath + " has no bundle:" + mainBundle + " in m_bundleCache");
        }
    }

    /// <summary>
    /// 是否为场景
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    private bool IsSceneBundle(string bundleName)
    {
        return _sceneBundles.Contains(bundleName); 
    }

    private void SetLocalization(bool param)
    {
        m_isLocalization = param;
    }

    void OnDestroy()
    {
        ClearAllAsset();
        Debugger.Log("~ResourceManager was destroy!");
    }

    #region 设置对象所有材质球的Shader
    static public void excuteShader(GameObject _gameobject)
    {
        Renderer[] renders = _gameobject.transform.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer rd in renders)
        {
            if (rd != null && rd.sharedMaterial != null)
            {
                rd.sharedMaterial.shader = Shader.Find(rd.sharedMaterial.shader.name);
            }
        }
    }
    #endregion
    
}
