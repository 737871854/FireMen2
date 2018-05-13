/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   AreaManager.cs
 * 
 * 简    介:    借鉴消防员1代功能
 * 
 * 创建标识：   Pancake 2017/11/2 15:52:16
 * 
 * 修改描述：   
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaManager : SingletonBehaviour<AreaManager>
{
    public GameObject[] areaList;
    protected Dictionary<string, int> areaIndex;
    protected List<List<Transform>> tranList;

    // Use this for initialization
    void Start()
    {
        areaIndex = new Dictionary<string, int>();
        tranList = new List<List<Transform>>();

        for (int index = 0; index < areaList.Length; ++index)
        {
            areaIndex.Add(areaList[index].name, index);
            List<Transform> list = new List<Transform>();
            Transform[] childs = areaList[index].transform.GetComponentsInChildren<Transform>();
            list.AddRange(childs);
            list.RemoveAt(0);
            tranList.Add(list);
        } 
    }
    
    /// <summary>
    /// 根据区域名字随机获得一个空间位置
    /// </summary>
    public bool GetRandomPositionInArea(string name, ref Vector3 pos)
    {
        if (areaIndex.ContainsKey(name) == false)
        {
            return false;
        }

        GameObject go = areaList[areaIndex[name]];
        if (go == null)
        {
            return false;
        }

        float x = go.transform.localScale.x / 2.0f;
        float y = go.transform.localScale.y / 2.0f;
        float z = go.transform.localScale.z / 2.0f;
        pos.x = Random.Range(-x, x);
        pos.y = Random.Range(-y, y);
        pos.z = Random.Range(-z, z);
        pos = go.transform.position + pos;
        //Log.Hsz(pos);
        //Log.Hsz(go.transform.position);
        //Log.Hsz(IsPositionInArea(name,pos));
        return true;
    }

    /// <summary>
    /// 根据区域名从已经配置好的坐标点中获得一个空间
    /// </summary>
    public bool GetExitOrRandPositionInArea(string name, ref Vector3 pos, int index = 0)
    {
        if (areaIndex.ContainsKey(name) == false)
        {
            return false;
        }

        List<Transform> list = tranList[areaIndex[name]];
        if (list == null)
        {
            return false;
        }

        if (list.Count == 0 || list.Count <= index)
            GetRandomPositionInArea(name, ref pos);
        else
            pos = list[index].position;

        return true;
    }

    /// <summary>
    /// 根据区域名字判断空间的某一点是否在区域内
    /// </summary>
    public bool IsPositionInArea(string name, Vector3 pos)
    {
        if (areaIndex.ContainsKey(name) == false)
        {
            return false;
        }
        GameObject go = areaList[areaIndex[name]];
        if (go == null)
        {
            return false;
        }

        Vector3 cloeset = new Vector3();
        cloeset = go.GetComponent<BoxCollider>().ClosestPointOnBounds(pos);
        float distance = Vector3.Distance(cloeset, pos);
        if (distance <= 0.05f)
        {
            return true;
        }

        return false;
    }
}
