/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfAttackOrHoldState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/3 8:34:32
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttackOrHoldState : IWolfState
{
    public WolfAttackOrHoldState(WolfFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = WolfStateID.AttackOrHold;
    }

    private Wolf mWolf;
    private float mNormalTime;
    public override void DoBeforeEntering()
    {
        mNormalTime = 0;
        mWolf = mCharacter as Wolf;
        mCharacter.EnterInvincible();
        if (mCharacter.actionType == E_ActionType.Normal)
        {
            mCharacter.PlayAnim("attack0", 2);
            ioo.cameraManager.PauseCPA();
        }else if(mCharacter.actionType == E_ActionType.ShakeScreen)
        {
            mCharacter.PlayAnim("attack1_0", 3);
            EventDispatcher.TriggerEvent(EventDefine.Event_Monster_Hold_Screen);
            ioo.gameMode.RunState(E_GameState.Hold);
        }
    }

    public override void Act(E_ActionType actionType)
    {
        if (actionType == E_ActionType.Normal)
        {
            NormalAct();
        }
        else if (actionType == E_ActionType.ShakeScreen)
        {
            SpecialAct();
        }
    }

    private void NormalAct()
    {
        mNormalTime = mCharacter.AnimNormalizedTime("attack0");
    }

    private void SpecialAct()
    {
       
    }

    public override void Reason(E_ActionType actionType)
    {
        if (actionType == E_ActionType.Normal)
        {
            NormalReason();
        }
        else if (actionType == E_ActionType.ShakeScreen)
        {
            SpecialReason();
        }
    }

    private void NormalReason()
    {
        if (mNormalTime > 0.99f)
        {
            // 相机震动
            ioo.cameraManager.NormalShake();
            // 对玩家造成伤害
            int[] args = new int[] { -1, mCharacter.attr.baseAttr.id, mCharacter.attr.baseAttr.damageValue };
            ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);

            mWolf.crashPoint.AddScreenCrash();
            mFSMSystem.PerformTransition(WolfTransition.MissionComplete);
        }
    }

    private void SpecialReason()
    {
       
    }
}