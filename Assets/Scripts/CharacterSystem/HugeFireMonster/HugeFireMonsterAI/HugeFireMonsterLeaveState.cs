/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HugeFireMonsterLeaveState.cs
 * 
 * 简    介:    攻击完离开状态
 * 
 * 创建标识：   Pancake 2018/4/8 9:17:11
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class HugeFireMonsterLeaveState : IHugeFireMonsterState
{
    public HugeFireMonsterLeaveState(HugeFireMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HugeFireMonsterStateID.Leave;
    }

    private List<Vector3> mPath = new List<Vector3>();
    private int mCurrentIndex;

    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("idle", 0);
    }

    public override void DoBeforeLeaving()
    {
        mCurrentIndex = 0;
    }

    public override void Act(E_ActionType actionType)
    {
        if(mPath.Count == 0)
        {
            mCurrentIndex = 0;
            mPath = Util.GenBezierPath(mCharacter.position, mCharacter.bornPosition, Util.Random(2, 4));
        }
        else
        {
            mCharacter.MoveByPath(mPath, ref mCurrentIndex);
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if(Vector3.Distance(mCharacter.position, mCharacter.bornPosition) < Define.PATH_STEP)
        {
            mFSM.PerformTransition(HugeFireMonsterTransition.SeaEnemy);
        }
    }
}