/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PropAttrStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 16:02:43
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class PropAttrStrategy : IAttrStrategy
{
    public Vector3 GetEulerAngle(CharacterRefreshPO characterRefreshPO)
    {
        if (characterRefreshPO.LocalEulerAngles.Length % 3 == 0)
            return new Vector3(characterRefreshPO.LocalEulerAngles[0], characterRefreshPO.LocalEulerAngles[1], characterRefreshPO.LocalEulerAngles[2]);
        else
            return Vector3.zero;
    }

    public Vector3 GetLocalScale(CharacterRefreshPO characterRefreshPO)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetSpawnPosition(CharacterRefreshPO characterRefreshPO)
    {
        if (characterRefreshPO.AppearePoint.Length %3 == 0)
            return new Vector3(characterRefreshPO.AppearePoint[0], characterRefreshPO.AppearePoint[1], characterRefreshPO.AppearePoint[2]);
        else
            return Vector3.zero;
    }
}