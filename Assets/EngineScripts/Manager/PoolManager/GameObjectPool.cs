/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PoolManager.cs
 * 
 * 简    介:    对象池
 * 
 * 创建标识：   Pancake 2017/4/2 11:04:59
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class GameObjectPool
{
    /// <summary>
    /// 名字
    /// </summary>
    public string name;

    /// <summary>
    /// 路径
    /// </summary>
    public string path;
    ///// <summary>
    ///// 预制体
    ///// </summary>
    //private GameObject prefab;
    /// <summary>
    /// 原始对象
    /// </summary>
    private UnityEngine.Object asset;

    /// <summary>
    /// 预先生成各个数
    /// </summary>
    [SerializeField]
    private int preAmount = 0;

    /// <summary>
    /// 最大数量
    /// </summary>
    [SerializeField]
    private int maxAmount = 1;

    /// <summary>
    /// 存放对象池正被使用的对象
    /// </summary>
    private List<GameObject> mUseList = new List<GameObject>();

    /// <summary>
    /// 存放对象池中空闲状态下的对象
    /// </summary>
    private List<GameObject> mFreeList = new List<GameObject>();

    /// <summary>
    /// 个数
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 预加载最大个数
    /// </summary>
    public void PreLoad()
    {
        CreateByCount(maxAmount);
    }

    /// <summary>
    /// 指定创建个数
    /// </summary>
    /// <param name="num"></param>
    public void CreateByCount(int num)
    {
        //if (num > maxAmount)
        //    num = maxAmount;
        //if (Count >= num)
        //    return;

        //num = num - Count;
        for (int i = 0; i < num; ++i)
            CreateNewInstance();
    }

    /// <summary>
    /// 表示从资源池中获取一个实例
    /// </summary>
    public GameObject GetInstance()
    {
        GameObject ret = null;

        if (mFreeList.Count == 0)
            CreateNewInstance();

        ret = mFreeList[0];
        mFreeList.RemoveAt(0);
        mUseList.Add(ret);

        ret.SetActive(true);
        return ret;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="go"></param>
    public void Destory(GameObject go)
    {
        mUseList.Remove(go);
        go.SetActive(false);
        go.transform.SetParent(PoolManager._parentTransform);
        SetToFree(go);
    }

    public bool Contain(GameObject go)
    {
        if (mUseList.Contains(go))
            return true;
        return false;
    }

    /// <summary>
    /// 清除缓存
    /// </summary>
    public void Clear()
    {
        Count = 0;
        mUseList.Clear();
        mFreeList.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    private void SetToFree(GameObject go)
    {
        if (mFreeList.Count + mUseList.Count > maxAmount)
        {
            //if (go == prefab)
            //    mFreeList.Add(go);
            //else
                GameObject.Destroy(go);
        }
        else
            mFreeList.Add(go);
    }

    /// <summary>
    /// 创建新的对象
    /// </summary>
    /// <returns></returns>
    private void CreateNewInstance()
    {
        if (null == asset)
        {
            ResourceMisc.AssetWrapper aw = ioo.resourceManager.LoadAsset(path, typeof(GameObject));
            asset = aw.GetAsset();
        }

        GameObject go = GameObject.Instantiate(asset) as GameObject;
        go.transform.localEulerAngles   = Vector3.zero;
        go.transform.localScale         = Vector3.one;
        go.transform.localPosition      = Vector3.zero;
        go.name                         = name + "_" + Count++;
        go.SetActive(false);
        //AddScriptToPrefab(go);
        go.transform.SetParent(PoolManager._parentTransform);
        mFreeList.Add(go);
    }

    ///// <summary>
    ///// 添加脚本组件给生成的对象
    ///// </summary>
    //private void AddScriptToPrefab(GameObject go)
    //{
    //    Type type = DefineType.GetType(name);
    //    if (null == type)
    //        return;

    //    go.AddComponent(type);
    //}
}
