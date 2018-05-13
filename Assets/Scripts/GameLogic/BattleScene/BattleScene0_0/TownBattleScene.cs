/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BattleScene0Controller.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/10 16:41:16
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TownBattleScene : IBattleScene
{
    // 镜头一两小孩路线
    public List<Transform> mCitizen0Path;
    public List<Transform> mCitizen1Path;
    // 镜头一场景特效
    public GameObject mParticles;

    private List<UselessCitizen> citizenList;

    private bool mIsInit;

    public override void Init()
    {
        mIsInit = false;
        citizenList = new List<UselessCitizen>();
        mCheckWaterPullList = new List<E_CharacterType>() { E_CharacterType.SandBox};

        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Active_Circle_Coin, OnCircleCoin);
    }

    public override void Release()
    {
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Active_Circle_Coin, OnCircleCoin);
    }

    private void Update()
    {
        if (ioo.preIsLoad && !mIsInit) { StartCoroutine(OnSceneIsReady()); mIsInit = true; }

        foreach(UselessCitizen uc in citizenList)
        {
            if (!uc.canDestroy)
                uc.Update();
        }

        RemoveCitizenIsKilled(citizenList);
    }

    IEnumerator OnSceneIsReady()
    {
        yield return new WaitForSeconds(1.0f);
        mParticles.SetActive(true);
        StartCoroutine(ShowCitizen());
    }

    IEnumerator ShowCitizen()
    {
        yield return new WaitForSeconds(1.0f);
        CreateCitizen();
    }

    private void CreateCitizen()
    {
        UselessCitizen.E_UselessType type = UselessCitizen.E_UselessType.Null;
        GameObject character0 = PoolManager.Instance.Spawn(PoolItemName.Citizen0);
        type = Type(type);
        List<Transform> path0 = type == UselessCitizen.E_UselessType.Escaple ? mCitizen0Path : mCitizen1Path;
        UselessCitizen citizen0 = new UselessCitizen(character0, path0, type);
        GameObject character1 = PoolManager.Instance.Spawn(PoolItemName.Citizen2);
        type = Type(type);
        List<Transform> path1 = type == UselessCitizen.E_UselessType.ForHelp ? mCitizen1Path : mCitizen0Path;
        UselessCitizen citizen1 = new UselessCitizen(character1, path1, type);
        citizenList.Add(citizen0);
        citizenList.Add(citizen1);

        Invoke("ActiveStageSys", 2.0f);
    }

    private void ActiveStageSys()
    {
        EventDispatcher.TriggerEvent(EventDefine.Event_Stage_System_Is_Pause, false);
    }

    private void DisActiveStageSys()
    {
        EventDispatcher.TriggerEvent(EventDefine.Event_Stage_System_Is_Pause, true);
    }

    private UselessCitizen.E_UselessType Type(UselessCitizen.E_UselessType type)
    {
        UselessCitizen.E_UselessType ret = UselessCitizen.E_UselessType.Null;
        if(type == UselessCitizen.E_UselessType.Null)
        {
            ret = (UselessCitizen.E_UselessType)Util.Random(0, 2);
        }
        else
        {
            ret = type == UselessCitizen.E_UselessType.Escaple ? UselessCitizen.E_UselessType.ForHelp : UselessCitizen.E_UselessType.Escaple;
        }
        return ret;
    }

    private void RemoveCitizenIsKilled(List<UselessCitizen> citizen)
    {
        List<UselessCitizen> canDestroyes = new List<UselessCitizen>();
        foreach (UselessCitizen uc in citizen)
        {
            if (uc.canDestroy)
            {
                canDestroyes.Add(uc);
            }
        }

        foreach (UselessCitizen uc in canDestroyes)
        {
            uc.Release();
            citizenList.Remove(uc);
        }
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


    /// <summary>
    /// 获取指定圆环的位置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override Vector3 GetCirclePositionByName(string name)
    {
        if(mCircle0.name == name)
        {
            return mCircle0.transform.position;
        }

        if(mCircle1.name == name)
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
        if(lv == 8)
        {
            return mHoldPointList;
        }
        return null;
    }

}