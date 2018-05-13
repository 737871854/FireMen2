/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CharacterSystem.cs
 * 
 * 简    介:    角色管理者
 * 
 * 创建标识：   Pancake 2018/3/2 11:04:58
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem
{
    private bool mPause;

    #region 角色存储
    /// <summary>
    /// 备选角色列表
    /// </summary>
    private List<ICharacter> mSelectCharacter = new List<ICharacter>();
    /// <summary>
    /// 备选地图列表
    /// </summary>
    private List<ICharacter> mSelectMap = new List<ICharacter>();
    /// <summary>
    /// 火怪列表
    /// </summary>
    private List<ICharacter> mFireMonster = new List<ICharacter>();
    /// <summary>
    /// 大伙怪
    /// </summary>
    private List<ICharacter> mHugeFireMonster = new List<ICharacter>();
    /// <summary>
    /// 市民
    /// </summary>
    private List<ICharacter> mCitizen = new List<ICharacter>();
    /// <summary>
    /// 动物
    /// </summary>
    private List<ICharacter> mNpc = new List<ICharacter>();
    /// <summary>
    /// 直升机
    /// </summary>
    private List<ICharacter> mHelicopter = new List<ICharacter>();
    /// <summary>
    /// 精英怪
    /// </summary>
    private List<ICharacter> mEliteMonster = new List<ICharacter>();
    /// <summary>
    /// 金币
    /// </summary>
    private List<ICharacter> mCoin = new List<ICharacter>();
    /// <summary>
    /// 道具
    /// </summary>
    private List<ICharacter> mProp = new List<ICharacter>();
    /// <summary>
    /// 狼
    /// </summary>
    private List<ICharacter> mWolf = new List<ICharacter>();

    // Boss
    private ICharacter mBoss;

    // 射击点
    private List<IHitPoint> mHitPoints = new List<IHitPoint>();
    #endregion

    public CharacterSystem()
    {
        ioo.gameManager.RegisterUpdate(Update);
    }

    public void Release()
    {
        ioo.gameManager.UnregisterUpdate(Update);
    }

    /// <summary>
    /// 攻击玩家的大小火怪和烟雾怪个数
    /// </summary>
    /// <returns></returns>
    public int GetNormalFlyMonsterCout()
    {
        int count = 0;
        foreach(ICharacter c in mFireMonster)
        {
            if (c.actionType == E_ActionType.Normal)
                ++count;
        }
        foreach (ICharacter c in mHugeFireMonster)
        {
            if (c.actionType == E_ActionType.Normal)
                ++count;
        }
        return count;
    }

    /// <summary>
    /// 能劫持玩家的精英飞行怪
    /// </summary>
    /// <returns></returns>
    public int GetNormalEliteMonsterCount() { return mEliteMonster.Count; }
    /// <summary>
    /// 能劫持玩家的狼的个数
    /// </summary>
    /// <returns></returns>
    public int GetWolfCount() { return mWolf.Count; }
    /// <summary>
    /// 获取可以被飞机救援的市民
    /// </summary>
    /// <returns></returns>
    public Citizen CitizenIsWaittingForHelp()
    {
        foreach(ICharacter character in mCitizen)
        {
            Citizen citizen = character as Citizen;
            if (citizen.canCallRescued)
                return citizen;
        }
        return null;
    }
    /// <summary>
    /// 场景中已存在的行为为需要飞机救援的市民个数
    /// </summary>
    /// <returns></returns>
    public int GetCitizenTypeIsWaittingForHelpCount()
    {
        int count = 0;
        foreach(Citizen c in mCitizen)
        {
            if (c.actionType == E_ActionType.WaitForHelp)
                ++count;
        }
        return count;
    }
    /// <summary>
    /// 场景中是否有直升机
    /// </summary>
    /// <returns></returns>
    public bool HasHelicopter() { return mHelicopter.Count > 0 ? true : false; }
    /// <summary>
    /// 是否有摇屏狼
    /// </summary>
    /// <returns></returns>
    public bool HasHoldWolf()
    {
        foreach(Wolf wolf in mWolf)
        {
            if (wolf.actionType == E_ActionType.ShakeScreen) return true;
        }
        return false;
    }
    /// <summary>
    /// Npc是否和其他Npc将要发生碰撞
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public bool HasNeighbor(ICharacter character)
    {
        foreach(ICharacter npc in mNpc)
        {
            if(npc != character)
            {
                if (Vector3.Distance(npc.position, character.position) < 0.3f)
                    return true;
            }
        }
        return false;
    }
    #region 添加或移除Character
    // 角色
    public void AddSelectCharacter(SelectCharacter selectCharacter) { mSelectCharacter.Add(selectCharacter); }
    public void RemoveSelectCharacter(SelectCharacter selectCharacter) { mSelectCharacter.Remove(selectCharacter); }

    // 地图
    public void AddSelectMap(SelectMap selectMap) { mSelectMap.Add(selectMap); }
    public void RemoveSelectMap(SelectMap selectMap) { mSelectMap.Remove(selectMap); }

    // 小火怪,烟雾怪
    public void AddFireMonster(FireMonster monster) { mFireMonster.Add(monster); }
    public void RemoveFireMonster(FireMonster monster) { mFireMonster.Remove(monster); }

    // 大火怪
    public void AddHugeFireMonster(HugeFireMonster monster) { mHugeFireMonster.Add(monster); }
    public void RemoveHugeFireMonster(HugeFireMonster monster) { mHugeFireMonster.Remove(monster); }

    // 市民
    public void AddCitizen(Citizen citizen) { mCitizen.Add(citizen); }
    public void RemvoeCitizen(Citizen citizen) { mCitizen.Remove(citizen); }

    // 动物
    public void AddNpc(Npc npc) { mNpc.Add(npc); }
    public void RemoveNpc(Npc npc) { mNpc.Remove(npc); }

    // 直升机
    public void AddHelicopter(Helicopter helicopter) { mHelicopter.Add(helicopter); }
    public void RemoveHelicopter(Helicopter helicopter) { mHelicopter.Remove(helicopter); }

    // 精英怪
    public void AddEliteMonster(EliteMonster eliteMonster) { mEliteMonster.Add(eliteMonster); }
    public void RemoveEliteMonster(EliteMonster eliteMonster) { mEliteMonster.Remove(eliteMonster); }

    // 金币 
    public void AddCoin(Coin coin) { mCoin.Add(coin); }
    public void RemoveCoin(Coin coin) { mCoin.Remove(coin); }

    // 道具
    public void AddProp(Prop prop) { mProp.Add(prop); }
    public void RemoveProp(Prop prop) { mProp.Remove(prop); }

    public void AddBoss(ICharacter boss) { mBoss = boss; }
    
    // 射击点
    public void AddHitPoint(IHitPoint hitPoint) { mHitPoints.Add(hitPoint); }
    public void RemoveHitPoint(IHitPoint hitPoint) { mHitPoints.Remove(hitPoint); }

    public void AddWolf(Wolf wolf) { mWolf.Add(wolf); }
    public void RemoveWolf(Wolf wolf) { mWolf.Remove(wolf); }
    #endregion

    public void Pause() { mPause = true; }
    public void Active() { mPause = false; }

    public bool IsBossLive() { return mBoss != null; }

    public void Clear()
    {
        mSelectCharacter.Clear();
        mSelectMap.Clear();
        mFireMonster.Clear();
        mHugeFireMonster.Clear();
        mCitizen.Clear();
        mNpc.Clear();
        mHelicopter.Clear();
        mEliteMonster.Clear();
        mCoin.Clear();
        mProp.Clear();
        mWolf.Clear();
        mBoss = null;
        mHitPoints.Clear();
    }
    /// <summary>
    /// 销毁所有Character
    /// </summary>
    public void Destroy()
    {
        foreach (SelectCharacter fm in mSelectCharacter) { fm.Destroy(); }
        foreach (SelectMap fm in mSelectMap) { fm.Destroy(); }
        foreach (FireMonster fm in mFireMonster) { fm.Destroy(); }
        foreach (HugeFireMonster fm in mHugeFireMonster) { fm.Destroy(); }
        foreach (Citizen fm in mCitizen) { fm.Destroy(); }
        foreach (Npc fm in mNpc) { fm.Destroy(); }
        foreach (Helicopter fm in mHelicopter) { fm.Destroy(); }
        foreach (EliteMonster fm in mEliteMonster) { fm.Destroy(); }
        foreach (Coin fm in mCoin) { fm.Destroy(); }
        foreach (Prop fm in mProp) { fm.Destroy(); }
        foreach (Wolf fm in mWolf) { fm.Destroy(); }
    }
    // 道济时结束，玩家没有成功选择角色，设置默认角色
    public void NoCharacterIsSelect()
    {
        for (int i = 0; i < Define.MAX_PLAYER_NUMBER; ++i)
        {
            if (ioo.playerManager.GetPlayer(i).HasHead())
                continue;
            else
            {
                foreach(ICharacter character in mSelectCharacter)
                {
                    SelectCharacter sc = character as SelectCharacter;
                    if (sc.hasBeenSelected)
                        continue;
                    else
                    {
                        sc.hasBeenSelected = true;
                        ioo.playerManager.GetPlayer(i).SetHead(sc.attr.baseAttr.headID);
                    }
                }
            }
        }
    }
    // 杀死所有飞行怪物
    public void KillFlyMonster()
    {
        foreach(FireMonster fm in mFireMonster)
        {
            fm.Destroy();
        }

        foreach(HugeFireMonster fm in mHugeFireMonster)
        {
            fm.Destroy();
        }

        foreach(EliteMonster fm in mEliteMonster)
        {
            fm.Destroy();
        }
    }
    // 杀死所有攻击玩家的怪物
    public void KillFlyMonsterAnimPlayerOrNpc()
    {
        foreach (FireMonster fm in mFireMonster)
        {
            if(fm.actionType != E_ActionType.AttackCitizen && fm.actionType != E_ActionType.AttackNpc)
                fm.Destroy();
        }

        foreach (HugeFireMonster fm in mHugeFireMonster)
        {
            if (fm.actionType != E_ActionType.AttackCitizen && fm.actionType != E_ActionType.AttackNpc)
                fm.Destroy();
        }
    }
    // 杀死精英怪
    public void KillEliteMonster()
    {
        foreach (EliteMonster fm in mEliteMonster)
        {
            fm.Destroy();
        }
    }
    // 怪物获取要攻击的市民
    public ICharacter MonsterFindCitizen()
    {
        ICharacter character = null;
        foreach(ICharacter c in mCitizen)
        {
            // 限定每个市民不能同时被2个以上的怪物攻击
            if (c.EnemyCount < 2 && c.actionType == E_ActionType.Normal)
                return c;
        }

        return character;
    }
    // 获取要攻击的npc
    public ICharacter MonsterFindNpc()
    {
        ICharacter character = null;
        foreach(ICharacter c in mNpc)
        {
            if (c.EnemyCount < 1)
                return c;
        }
        return character;
    }
    // 是否存在其他EliteMonster将要或正在攻击玩家
    public bool HasToOrHoldingElite(EliteMonster elite)
    {
        foreach(EliteMonster e in mEliteMonster)
        {
            if (e == elite) continue;
            if (e.IsToHoldOrHolding()) return true;
        }
        return false;
    }
    public bool CanSpawnSpecialHugeMonster()
    {
        int count = 0;
        foreach (ICharacter c in mHugeFireMonster)
        {
            if (c.actionType != E_ActionType.Normal)
                ++count;
        }
        return count < mNpc.Count + mCitizen.Count * 2;
    }
    /// <summary>
    /// 射击怪物
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="character"></param>
    /// <param name="targetGO"></param>
    /// <returns></returns>
    public bool PickCharacter(Vector2 screenPos, out ICharacter character, out GameObject targetGO)
    {
        if (PickAppointCharacter(mFireMonster, screenPos, out character, out targetGO)) return true;
        else if (PickAppointCharacter(mHugeFireMonster, screenPos, out character, out targetGO)) return true;
        else if (PickAppointCharacter(mEliteMonster, screenPos, out character, out targetGO)) return true;
        else if (PickAppointCharacter(mCoin, screenPos, out character, out targetGO)) return true;
        else if (PickAppointCharacter(mProp, screenPos, out character, out targetGO)) return true;
        else if (PickAppointCharacter(mWolf, screenPos, out character, out targetGO)) return true;
        else if (mBoss != null && PickAppointCharacter(mBoss, screenPos, out character, out targetGO)) return true;
        else return false;
    }
    public bool PickHitPoint(Vector2 screenPos, out IHitPoint hitPoint, out GameObject targetGo)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        hitPoint = null;
        targetGo = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, DefineMaskLayer.MASK_TARGET))
        {
            foreach (IHitPoint c in mHitPoints)
            {
                if (c.gameObject == hit.transform.gameObject)
                {
                    hitPoint = c;
                    targetGo = hit.transform.gameObject;
                    break;
                }
            }
            if (hitPoint == null) return false;
            return true;
        }
        return false;
    }
    public bool PickAppointCharacter(List<ICharacter> list, Vector2 screenPos, out ICharacter character, out GameObject targetGO)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        character = null;
        targetGO = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, DefineMaskLayer.MASK_OBJECT))
        {
            foreach (ICharacter c in list)
            {
                if (c.gameObject == hit.transform.gameObject)
                {
                    character = c;
                    targetGO = hit.transform.gameObject;
                    break;
                }
            }
            if (character == null) return false;
            return true;
        }
        return false;
    }
    public bool PickAppointCharacter(ICharacter check, Vector2 screenPos, out ICharacter character, out GameObject targetGO)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        character = null;
        targetGO = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, DefineMaskLayer.MASK_OBJECT))
        {
            if (check.gameObject == hit.transform.gameObject)
            {
                character = check;
                targetGO = hit.transform.gameObject;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 选角色
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="character"></param>
    /// <param name="targetGO"></param>
    /// <returns></returns>
    public bool PickSelectCharacter(Vector2 screenPos, out ICharacter character, out GameObject targetGO)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        character = null;
        targetGO = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, DefineMaskLayer.MASK_OBJECT))
        {
            foreach (ICharacter c in mSelectCharacter)
            {
                if (c.gameObject == hit.transform.gameObject)
                {
                    character = c;
                    targetGO = hit.transform.gameObject;
                    break;
                }
            }
            if (character == null) return false;
            return true;
        }
        return false;
    }
    /// <summary>
    /// 选地图
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="character"></param>
    /// <param name="targetGO"></param>
    /// <returns></returns>
    public bool PickSelectMap(Vector2 screenPos, out ICharacter character, out GameObject targetGO)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        character = null;
        targetGO = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, DefineMaskLayer.MASK_OBJECT))
        {
            foreach (ICharacter c in mSelectMap)
            {
                if (c.gameObject == hit.transform.gameObject)
                {
                    character = c;
                    targetGO = hit.transform.gameObject;
                    break;
                }
            }
            if (character == null) return false;
            return true;
        }
        return false;
    }
    /// <summary>
    /// 更新所有角色，并将被击杀的角色从列表中移除
    /// </summary>
    public void Update()
    {
        if (mPause) return;

        UpdateSelectCharacter();
        UpdateSelectMap();
        UpdateFireMonster();
        UpdateHugeFireMonster();
        UpdateCitizen();
        UpdateNpc();
        UpdateHelicopter();
        UpdateEliteMonster();
        UpdateCoin();
        UpdateProp();
        UpdateBoss();
        UpdateWolf();

        RemoveCharacterIsKilled(mFireMonster);
        RemoveCharacterIsKilled(mHugeFireMonster);
        RemoveCharacterIsKilled(mCitizen);
        RemoveCharacterIsKilled(mNpc);
        RemoveCharacterIsKilled(mHelicopter);
        RemoveCharacterIsKilled(mEliteMonster);
        RemoveCharacterIsKilled(mCoin);
        RemoveCharacterIsKilled(mProp);
        RemoveCharacterIsKilled(mWolf);
        if (mBoss.canDestroy)
        {
            mBoss.Release();
            mBoss = null;
        }
    }

    #region 更新角色逻辑
    private void UpdateSelectCharacter()
    {
        foreach (ICharacter c in mSelectCharacter)
        {
            c.Update();
            //c.UpdateFSMAI();
        }
    }
    private void UpdateSelectMap()
    {
        foreach (ICharacter c in mSelectMap)
        {
            c.Update();
            //c.UpdateFSMAI();
        }
    }
    private void UpdateFireMonster()
    {
        foreach (FireMonster fm in mFireMonster)
        {
            fm.Update();
            fm.UpdateFSMAI(fm.actionType);
        }
    }
    private void UpdateHugeFireMonster()
    {
        foreach (HugeFireMonster fm in mHugeFireMonster)
        {
            fm.Update();
            fm.UpdateFSMAI(fm.actionType);
        }
    }
    private void UpdateCitizen()
    {
        foreach (Citizen fm in mCitizen)
        {
            fm.Update();
            fm.UpdateFSMAI(fm.actionType);
        }
    }
    private void UpdateNpc()
    {
        foreach (Npc fm in mNpc)
        {
            fm.Update();
            fm.UpdateFSMAI(fm.actionType);
        }
    }
    private void UpdateHelicopter()
    {
        foreach (Helicopter fs in mHelicopter)
        {
            fs.Update();
            fs.UpdateFSMAI(fs.actionType);
        }
    }
    private void UpdateEliteMonster()
    {
        foreach (EliteMonster fs in mEliteMonster)
        {
            fs.Update();
            fs.UpdateFSMAI(fs.actionType);
        }
    }
    private void UpdateCoin()
    {
        foreach(Coin fs in mCoin)
        {
            fs.Update();
            fs.UpdateFSMAI(fs.actionType);
        }
    }
    private void UpdateProp()
    {
        foreach (Prop fs in mProp)
        {
            fs.Update();
            fs.UpdateFSMAI(fs.actionType);
        }
    }
    private void UpdateWolf()
    {
        foreach(Wolf fs in mWolf)
        {
            fs.Update();
            fs.UpdateFSMAI(fs.actionType);
        }
    }
    private void UpdateBoss()
    {
        if(mBoss != null)
        {
            mBoss.Update();
            mBoss.UpdateFSMAI(mBoss.actionType);
            float percent = 1.0f * mBoss.attr.currentHP / mBoss.attr.baseAttr.maxHP;
            ioo.gameMode.UpdateBossProgress(percent);
        }
    }
    #endregion
    // 移除被击杀或自爆的角色
    private void RemoveCharacterIsKilled(List<ICharacter> characters)
    {
        List<ICharacter> canDestroyes = new List<ICharacter>();
        foreach(ICharacter character in characters)
        {
            if (character.canDestroy)
            {
                canDestroyes.Add(character);
            }
        }

        foreach(ICharacter character in canDestroyes)
        {
            character.Release();
            characters.Remove(character);
        }
    }
}