/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PoolManager.cs
 * 
 * 简    介:    对象池管理 通过Spawn获取一个对象，通过DeSpaw回收一个对象(对象池的有所对象过场全部销毁，不会保留)
 * 
 * 创建标识：   Pancake 2017/4/2 16:19:47
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class PoolManager
{
    private static PoolManager mInstance;

    private static readonly object mObject = new object();

    public static PoolManager Instance
    {
        get
        {
            if (null == mInstance)
                lock (mObject)
                    if (null == mInstance)
                        mInstance = new PoolManager();
            if (_parentTransform == null) _parentTransform = new GameObject(CONTAINER_NAME).transform;
            return mInstance;
        }
    }

    private const string CONTAINER_NAME = "SimplePool";
    public static Transform _parentTransform;
    /// <summary>
    /// 对象池字典
    /// </summary>
    private Dictionary<string, GameObjectPool> poolDic = new Dictionary<string, GameObjectPool>();

    private PoolManager()
    {
        // 初始化
        LoadPoolConfig();
    }

    /// <summary>
    /// 加载对象池配置文件
    /// </summary>
    private void LoadPoolConfig()
    {
        ResourceMisc.AssetWrapper aw = ioo.resourceManager.LoadAsset("prefabs\\pool\\gameobjectpool", typeof(GameObjectPoolList));
        GameObjectPoolList poolList  = (GameObjectPoolList)aw.GetAsset();
        if (null == poolList)
            return;
        foreach(GameObjectPool pool in poolList.poolList)
        {
            poolDic.Add(pool.name, pool);
        }
    }

    /// <summary>
    /// 预加载 （视情况而定，如各个关卡需要使用到的对象种类以及个数差异化较大，建议不要调用该方法，
    /// 使用LoadSceneMgr.Instance.AddPreLoadPrefab(string, int)方法精准化预加载）
    /// </summary>
    public void PreLoad()
    {
        Dictionary<string, GameObjectPool>.Enumerator er = poolDic.GetEnumerator();
        while (er.MoveNext())
        {
            er.Current.Value.PreLoad();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="count"></param>
    public void CreatePool(string poolName, int count)
    {
        GameObjectPool pool;
        if(poolDic.TryGetValue(poolName, out pool))
        {
            pool.CreateByCount(count);          
        }
        else
            Debugger.LogWarning("Pool: " + pool.name + "is not exits!");
    }

    /// <summary>
    /// 从对象池中获取指定的对象
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject Spawn(string poolName)
    {
        GameObjectPool pool;
        if (poolDic.TryGetValue(poolName, out pool))
        {
            return pool.GetInstance();
        }

        Debugger.LogWarning("Pool: " + poolName + "is not exits!");
        return null;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="go"></param>
    public void DeSpawn(GameObject go)
    {
        Dictionary<string, GameObjectPool>.Enumerator er = poolDic.GetEnumerator();
        while(er.MoveNext())
        {
            if (er.Current.Value.Contain(go))
                er.Current.Value.Destory(go);
            else
                continue;
        }
    }

    /// <summary>
    /// 清除所有对象
    /// </summary>
    public void Clear()
    {
        Dictionary<string, GameObjectPool>.Enumerator er = poolDic.GetEnumerator();
        while(er.MoveNext())
        {
            er.Current.Value.Clear();
        }
    }
}
