/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfHoldSuccessState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/3 8:34:46
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class WolfHoldSuccessState : IWolfState
{
    public WolfHoldSuccessState(WolfFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = WolfStateID.Success;
    }

    private Wolf mWolf;
    private float mNormalTime;
    public override void DoBeforeEntering()
    {
        mWolf = mCharacter as Wolf;
        mCharacter.PlayAnim("beat", 5);
    }

    public override void Act(E_ActionType actionType)
    {
        mNormalTime = mCharacter.AnimNormalizedTime("beat");
    }

    public override void Reason(E_ActionType actionType)
    {
        if(mNormalTime > 0.99f)
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
}