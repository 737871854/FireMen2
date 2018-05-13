/*
 * Copyright  (2015,广州擎天柱网络科技有限公司) 
 * 
 * 文件名称：   ResourceMisc.cs
 * 
 * 简    介:    辅助资源处理类，
 * 
 * 创建标识：   Lorry 2015/7
 * 
 * 修改描述：   Pancake整理 2017/10/23
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ResourceMisc
{
    // bundle ref wrapper
    public class BundleWrapper
    {
        string _abName;
        AssetBundle _bundle;
        Dictionary<string, int> _refAssets = new Dictionary<string, int>();

        public BundleWrapper(string abName, AssetBundle bundle)
        {
            _abName = abName;
            _bundle = bundle;
            _refAssets.Clear();
        }

        /// <summary>
        /// 增加引用次数
        /// </summary>
        /// <param name="assetKey"></param>
        public void AddRefAsset(string assetKey)
        {
            if (!_refAssets.ContainsKey(assetKey))
            {
                _refAssets.Add(assetKey, 1);
            }
            else
            {
                _refAssets[assetKey] += 1;
            }
        }

        /// <summary>
        /// 减少引用次数
        /// </summary>
        /// <param name="assetKey"></param>
        public void DecRefAsset(string assetKey)
        {
            if(_refAssets.ContainsKey(assetKey))
            {
                _refAssets[assetKey] -= 1;
                if (_refAssets[assetKey] == 0)
                {
                    _refAssets.Remove(assetKey);
                }
            }
        }

        public int GetRefAssetCount()
        {
            return _refAssets.Count;
        }

        public AssetBundle GetBundle()
        {
            return _bundle;
        }
    }

    // asset ref wrapper
    public class AssetWrapper
    {
        int _ref_count;

        Object mAsset;
        string _path;
        System.Type _assetType;
        string _bundle_name;

        // 该资源所依赖的所有bundle的名称
        string[] m_dps;

        bool _is_scene;

        public AssetWrapper(Object asset, string path, System.Type assetType, string bundle_name, bool is_scene)
        {
            if (asset != null)
            {
                GameObject temp = asset as GameObject;
                if(temp != null)
                {
                    ResourceManager.excuteShader(temp);
                }
            }

            _ref_count = 1;

            mAsset = asset;
            _path = path;
            _assetType = assetType;
            _bundle_name = bundle_name;
            _is_scene = is_scene;
        }

        /// <summary>
        /// 记录Asset基于的资源bundle名称，便于资源的释放 
        /// </summary>
        /// <param name="parDps"></param>
        public void SetDps(string[] parDps)
        {
            m_dps = parDps;
        }

        public string[] GetDps()
        {
            return m_dps;
        }

        public void AddRef()
        {
            ++_ref_count;
        }

        public void Release()
        {
            if (_ref_count > 0)
            {
                --_ref_count;
            }
        }

        public int GetRefCount()
        {
            return _ref_count;
        }

        public Object GetAsset()
        {
            return mAsset;
        }

        public string GetAssetPath()
        {
            return _path;
        }

        public string GetAssetType()
        {
            return _assetType.ToString();
        }

        public string GetBundleName()
        {
            return _bundle_name;
        }

        public bool IsScene()
        {
            return _is_scene;
        }
    }
}