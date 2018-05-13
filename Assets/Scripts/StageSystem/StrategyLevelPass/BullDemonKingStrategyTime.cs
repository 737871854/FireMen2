/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:01:51
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingStrategyTime : IStrategyLevelPass
{
    public int GetTime(int level)
    {
        return 999999999;
    }

    protected float mTimer;
    public bool HasKillCondition(int level)
    {
        if(mTimer < 5.0f)
        {
            mTimer += UnityEngine.Time.deltaTime;
            return false;
        }
        return !ioo.characterSystem.IsBossLive();
    }
}