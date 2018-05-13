/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CoinAttrStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/13 14:07:40
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinAttrStrategy : IAttrStrategy
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
        string name = characterRefreshPO.CricleName;
        IBattleScene battleScene = ioo.battleScene;
        if (battleScene == null) return Vector3.zero;
        return battleScene.GetCirclePositionByName(name);
    }
}