/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   MapWayPoint.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/4 14:26:31
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class MapWayPoint : MonoBehaviour
{
    public List<Transform> pointList;

    public void AddPoint(Transform tran)
    {
        if (pointList == null)
            pointList = new List<Transform>();
        tran.SetParent(transform);
        tran.localScale = Vector3.one;
        //if (pointList.Count != 0)
        //    pointList.Insert(pointList.Count - 1, tran);
        //else
        pointList.Add(tran);
    }

    public void AddPoint(GameObject go)
    {
        AddPoint(go.transform);
    }

    public void RemovePoint(Transform tran)
    {
        pointList.Remove(tran);
    }
}