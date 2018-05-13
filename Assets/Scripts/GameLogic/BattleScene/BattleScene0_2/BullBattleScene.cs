/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FarmBattleScene.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 9:25:46
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullBattleScene : IBattleScene
{
    private bool mIsInit;

    public List<SpriteRenderer> mSRList;

    public override void Init()
    {
        mIsInit = false;

        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Active_Circle_Coin, OnCircleCoin);
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Active_Boss_Black, OnBlack);
    }

    public override void Release()
    {
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Active_Circle_Coin, OnCircleCoin);
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Active_Boss_Black, OnBlack);
    }

    private void Update()
    {
        if (ioo.preIsLoad && !mIsInit) { StartCoroutine(OnSceneIsReady()); mIsInit = true; }
    }

    IEnumerator OnSceneIsReady()
    {
        yield return new WaitForSeconds(1.0f);
        ActiveStageSys();
        yield return new WaitForSeconds(2.0f);
        ioo.cameraManager.BossLongShake();
        ioo.cameraManager.BullCameraBackLens();
    }


    private void ActiveStageSys()
    {
        EventDispatcher.TriggerEvent(EventDefine.Event_Stage_System_Is_Pause, false);
    }

    private void DisActiveStageSys()
    {
        EventDispatcher.TriggerEvent(EventDefine.Event_Stage_System_Is_Pause, true);
    }

    /// <summary>
    /// 开启Coin功能
    /// </summary>
    /// <param name="active"></param>
    private void OnCircleCoin(bool active)
    {
        // TODO 开启特效
        // TODO 跟谁机械圆环
    }

    private void OnBlack(bool active)
    {
        Color color = Color.white;
        color.a = active ? 1 : 0;
        foreach (SpriteRenderer sp in mSRList)
            sp.DOColor(color, 0.5f);
    }

    /// <summary>
    /// 获取指定圆环的位置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override Vector3 GetCirclePositionByName(string name)
    {
        if (mCircle0.name == name)
        {
            return mCircle0.transform.position;
        }

        if (mCircle1.name == name)
        {
            return mCircle1.transform.position;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// 获取指定Hold
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override List<Transform> GetHoldPositionByLV(int lv)
    {
        if (lv == 8)
        {
            return mHoldPointList;
        }
        return null;
    }
}