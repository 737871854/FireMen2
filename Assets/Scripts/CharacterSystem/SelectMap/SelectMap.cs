/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectMap.cs
 * 
 * 简    介:    地图
 * 
 * 创建标识：   Pancake 2018/3/3 10:22:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : ICharacter
{
    // 该角色是否已经被选中
    private bool mHasBeenSelected;

    // 进度条
    private float mProgress;

    // 当前是否被击中
    private bool mBeenSpraied;

    private float mCurrentUnderAttackTimer;
    private float mLastUnderAttackTimer;
    private float mCheckTimer = 0.1f;

    public SelectMap()
    {
        EventDispatcher.AddEventListener(EventDefine.Event_Player_Select_Map, OnMapSelected);
    }

    public override void Release()
    {
        base.Release();
        EventDispatcher.RemoveEventListener(EventDefine.Event_Player_Select_Map, OnMapSelected);
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
            Check();
        }
    }

    public override void UnderAttack(Player player)
    {
        mLastUnderAttackTimer = mCurrentUnderAttackTimer;
        if (mHasBeenSelected) return;

        base.UnderAttack(player);
        mBeenSpraied = true;

        object[] objs = new object[2];

        mProgress = 1.0f - attr.currentHP * 1.0f / attr.baseAttr.maxHP;
        mGameObject.transform.position = Vector3.up * attr.baseAttr.offset_Y;

        objs[0] = attr.baseAttr.mapID;
        objs[1] = mProgress;
        ioo.TriggerListener(EventLuaDefine.Map_Is_Been_Spray, objs);

        if (mProgress >= 1)
        {
            mHasBeenSelected = true;
            // 地图选择结束
            ioo.gameMode.SetSelectedMap(attr.baseAttr.mapID);
        }
    }
    public void Check()
    {
        if (mBeenSpraied)
        {
            mBeenSpraied = false;
        }
        else
        {
            attr.ReplyHealth();
            mGameObject.transform.position = Vector3.zero;
            ioo.TriggerListener(EventLuaDefine.Map_Is_Been_Spray, new object[2] { attr.baseAttr.mapID, 0 });
        }
    }

    /// <summary>
    /// 地图被选中后，被水花射击后不再被选中, 并清除所有地图血条
    /// </summary>
    /// <param name="id"></param>
    private void OnMapSelected()
    {
        mHasBeenSelected = true;

        ioo.TriggerListener(EventLuaDefine.Map_Is_Been_Spray, new object[] { attr.baseAttr.mapID, 0 });
    }
}