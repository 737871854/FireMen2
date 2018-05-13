/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectCharacterAttrStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/3 9:43:52
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterAttrStrategy : IAttrStrategy
{
    public Vector3 GetEulerAngle(CharacterRefreshPO characterRefreshPO)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetLocalScale(CharacterRefreshPO characterRefreshPO)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetSpawnPosition(CharacterRefreshPO characterRefreshPO)
    {
        return new Vector3(characterRefreshPO.AppearePoint[0], characterRefreshPO.AppearePoint[1], characterRefreshPO.AppearePoint[2]);
    }
}