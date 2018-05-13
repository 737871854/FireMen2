/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   OceanBattleScene.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 9:26:30
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class OceanBattleScene : IBattleScene
{
    public override void Init()
    {
    }

    public override void Release()
    {
    }

    public override Vector3 GetCirclePositionByName(string name)
    {
        throw new NotImplementedException();
    }

    public override List<Transform> GetHoldPositionByLV(int lv)
    {
        throw new NotImplementedException();
    }
}