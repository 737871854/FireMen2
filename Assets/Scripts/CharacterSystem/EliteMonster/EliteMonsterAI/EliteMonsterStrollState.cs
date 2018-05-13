/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterIdleState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 19:23:15
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonsterStrollState : IEliteMonsterState
{
    private Vector3 mNextPos = Vector3.zero;
    private int index;

    public EliteMonsterStrollState(EliteMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = EliteMonsterStateID.Stroll;
    }

    public override void Act(E_ActionType actionType)
    {
        string areaName = mCharacter.stayArea;
        if (!AreaManager.Instance.IsPositionInArea(areaName, mNextPos))
        {
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, index);
        }
        if (mCharacter.MoveTo(mNextPos, 0.25f))
        {
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, index);
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (ioo.characterSystem.HasToOrHoldingElite(mCharacter as EliteMonster)) return;
        mFSMSystem.PerformTransition(EliteMonsterTransition.SeaEnemy);
    }
}