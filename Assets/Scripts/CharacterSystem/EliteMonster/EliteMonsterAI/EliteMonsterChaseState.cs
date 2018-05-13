/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterRunState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 19:23:49
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonsterChaseState : IEliteMonsterState
{
    private List<Transform> mHoldPointList;
    private int mIndex;

    public EliteMonsterChaseState(EliteMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = EliteMonsterStateID.Chase;
    }

    public override void DoBeforeEntering()
    {
        mIndex = 0;
        mHoldPointList = ioo.stageSystem.GetHoldPointTran();
    }

    public override void Act(E_ActionType actionType)
    {
        if (mCharacter.MoveTo(mHoldPointList[mIndex].position, 0.01f))
        {
            ++mIndex;
            mIndex %= mHoldPointList.Count;
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if(ioo.characterSystem.HasToOrHoldingElite(mCharacter as EliteMonster))
        {
            mFSMSystem.PerformTransition(EliteMonsterTransition.Waitting);
            return;
        }

        if (Vector3.Distance(mCharacter.gameObject.transform.position, mHoldPointList[mHoldPointList.Count - 1].position) < 0.02f)
        {
            mFSMSystem.PerformTransition(EliteMonsterTransition.Reached);
            return;
        }
    }
}