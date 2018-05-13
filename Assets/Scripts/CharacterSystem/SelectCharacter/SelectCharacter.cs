/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectCharacter.cs
 * 
 * 简    介:    角色
 * 
 * 创建标识：   Pancake 2018/3/2 16:22:46
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : ICharacter
{
    // 该角色是否已经被选中
    private bool mHasBeenSelected;

    // 进度条
    private float mProgress;

    // 射击当前角色的玩家id
    private int mSprayID = -1;

    private float mCurrentUnderAttackTimer;
    private float mLastUnderAttackTimer;
    private float mCheckTimer = 0.1f;

    public bool hasBeenSelected
    {
        get
        {
            return mHasBeenSelected;
        }

        set
        {
            mHasBeenSelected = value;
        }
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        throw new NotImplementedException();
    }

    protected override void UpdateExtra()
    {
        mCurrentUnderAttackTimer = Time.realtimeSinceStartup;

        if (mCurrentUnderAttackTimer - mLastUnderAttackTimer >= mCheckTimer)
        {
            SwitchPlayer();
        }
    }

    public override void UnderAttack(Player player)
    {
        mLastUnderAttackTimer = mCurrentUnderAttackTimer;
        if (mHasBeenSelected) return;

        int id = player.id;

        if (mSprayID == -1)
            mSprayID = id;
        else
        {
            if (mSprayID != id)
                return;
        }

        base.UnderAttack(player);
        object[] objs = new object[2];
        mProgress = 1.0f - attr.currentHP * 1.0f / attr.baseAttr.maxHP;

        objs[0] = attr.baseAttr.headID;
        objs[1] = mProgress;
        ioo.TriggerListener(EventLuaDefine.Character_Is_Been_Spray, objs);

        if(mProgress >= 1)
        {
            mHasBeenSelected = true;
            // 玩家选择结束
            EventDispatcher.TriggerEvent(EventDefine.Event_Player_Select_Character, id, attr.baseAttr.headID);
        }
    }

    private void SwitchPlayer()
    {
        mSprayID = -1;
        attr.ReplyHealth();

        object[] objs = new object[2];
        objs[0] = attr.baseAttr.headID;
        objs[1] = 0;
        ioo.TriggerListener(EventLuaDefine.Character_Is_Been_Spray, objs);
    }
}