/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CitizenRunState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 10:35:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class CitizenRunState : ICitizenState
{
    public CitizenRunState(CitizenFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = CitizenStateID.Run;
    }

    private Citizen mCitizen;
    public override void DoBeforeEntering()
    {
        mIndex = 0;
        mCitizen = mCharacter as Citizen;
    }

    public override void Act(E_ActionType actionType)
    {
        switch (actionType)
        {
            case E_ActionType.Normal:
                NormalAct();
                break;
            case E_ActionType.RunAndEcape:
                break;
            case E_ActionType.RunAndForHelp:
                break;
            case E_ActionType.WaitForBoat:
                break;
            case E_ActionType.WaitForHelp:
                WaitForHelpAct();
                break;
        }
    }

    private Vector3 mNextPos = Vector3.zero;
    private int mIndex;
    /// <summary>
    /// 在区域跑
    /// </summary>
    private void NormalAct()
    {
        string areaName = mCharacter.appearArea;
        if (!AreaManager.Instance.IsPositionInArea(areaName, mNextPos))
        {
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, mIndex);
            mNextPos.y = mCharacter.position.y;
        }
        if (mCharacter.MoveTo(mNextPos, 0.25f))
        {
            AreaManager.Instance.GetExitOrRandPositionInArea(areaName, ref mNextPos, mIndex);
            mNextPos.y = mCharacter.position.y;
        }
    }


    private bool mReachedWaitPos;
    /// <summary>
    /// 移动到飞机救援点
    /// </summary>
    private void WaitForHelpAct()
    {
        Vector3 targetPos = mCitizen.orginPos + mCharacter.gameObject.transform.forward * 0.5f;
        mReachedWaitPos = mCharacter.MoveStraight(targetPos);
        mCharacter.LookAtCamera();
    }

    public override void Reason(E_ActionType actionType)
    {
        switch (actionType)
        {
            case E_ActionType.Normal:
                NormalReason();
                break;
            case E_ActionType.RunAndEcape:
                break;
            case E_ActionType.RunAndForHelp:
                break;
            case E_ActionType.WaitForBoat:
                break;
            case E_ActionType.WaitForHelp:
                WaitForHelpReason();
                break;
        }
    }

    private void NormalReason()
    {

    }

    private void WaitForHelpReason()
    {
        if(mReachedWaitPos)
        {
            mFSMSystem.PerformTransition(CitizenTransition.Trapped);
        }
    }
}