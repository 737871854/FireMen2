/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IAttrStrategy.cs
 * 
 * 简    介:    角色属性策略基类
 * 
 * 创建标识：   Pancake 2018/3/2 11:26:24
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAttrStrategy
{
    //int GetExtraHPValue(int lv);
    //int GetDmgDescValue(int lv);
    //int GetCritDmg(float critRate);
    Vector3 GetSpawnPosition(CharacterRefreshPO characterRefreshPO);

    Vector3 GetLocalScale(CharacterRefreshPO characterRefreshPO);

    Vector3 GetEulerAngle(CharacterRefreshPO characterRefreshPO);
}