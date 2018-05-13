/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingStageHandler.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/20 8:32:12
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingStageHandler : IStageHandler
{
    public BullDemonKingStageHandler(StageSystem stageSystem, int lv) : base(stageSystem, lv)
    {
    }

    protected override void UpdateStage()
    {
        base.UpdateStage();
    }

    protected override void NewStage()
    {
        base.NewStage();
        switch (mLv)
        {

        }

    }

    protected override void LoopRefreshCharacter()
    {
        if (mLoopList.Count == 0) return;

    }
}