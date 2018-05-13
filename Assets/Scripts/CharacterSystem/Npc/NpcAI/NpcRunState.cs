/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   NpcRunState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 16:41:25
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcRunState : INpcState
{
    public NpcRunState(NpcFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = NpcStateID.Run;
    }


    private Vector3 mNextPos = Vector3.zero;
    private int mIndex;
    private float mTimer;
    public override void Act(E_ActionType actionType)
    {
        string areaName = mCharacter.appearArea;
        if (!AreaManager.Instance.IsPositionInArea(areaName, mNextPos))
        {
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, mIndex);
            mNextPos.y = mCharacter.position.y;
        }
        if (ioo.characterSystem.HasNeighbor(mCharacter) && mTimer <= 0)
        {
            mTimer = 2;
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, mIndex);
            mNextPos.y = mCharacter.position.y;
        }
        else
            mTimer -= Time.deltaTime;
        if (mCharacter.MoveTo(mNextPos, 0.25f))
        {
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, mIndex);
            mNextPos.y = mCharacter.position.y;
        }
    }

    public override void Reason(E_ActionType actionType)
    {
    }
}