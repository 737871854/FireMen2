/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FarmStageHandler.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 11:18:06
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class FarmStageHandler : IStageHandler
{
    private float mSpawnOtherTimer = 0;
    private float mSpawnCoinTimer = 0;
    private float mSpawnCoinTime = 0.1f;
    private float mSpawnOtherTime = 0.5f;

    public FarmStageHandler(StageSystem stageSystem, int lv) : base(stageSystem, lv)
    {
        mFireMonsterRefreshStrategy = new FireMonsterRefreshStrategy(mStageSystem.sceneName);
        mEliteMonsterRefreshStrategy = new EliteMonsterRefreshStrategy(mStageSystem.sceneName);
        mCitizenRefreshStrategy = new CitizenRefreshStrategy(mStageSystem.sceneName);
        mWolfRefreshStrategy = new WolfRefreshStrategy(mStageSystem.sceneName);
    }

    protected override void UpdateStage()
    {
        base.UpdateStage();
    }

    /// <summary>
    /// 新关卡
    /// </summary>
    protected override void NewStage()
    {
        base.NewStage();
        switch (mLv)
        {
            case 0:
                ioo.cameraManager.PlayCPA();
                break;
            case 1:
                ioo.cameraManager.PlayCPA();
                break;
            case 2:
                ioo.cameraManager.PlayCPA();
                break;
            case 3:
                ioo.cameraManager.PlayCPA();
                break;
            case 4:
                ioo.cameraManager.PlayCPA();
                break;
            case 5:
                mStageSystem.isPuse = false;
                EventDispatcher.TriggerEvent<bool>(EventDefine.Event_Active_Circle_Coin, true);
                break;
            case 6:
                EventDispatcher.TriggerEvent<bool>(EventDefine.Event_Active_Circle_Coin, false);
                ioo.cameraManager.PlayCPA();
                break;
            case 7:
                ioo.cameraManager.PlayCPA();
                break;
            case 8:
                ioo.cameraManager.PlayCPA();
                ioo.characterSystem.KillEliteMonster();
                break;
            case 9:
                
                break;
            case 10:
                ioo.cameraManager.PlayCPA();
                break;
            case 11:
                EventDispatcher.TriggerEvent(EventDefine.Event_Game_Success);
                break;
        }
    }

    /// <summary>
    /// 刷新需要循环刷新的怪物
    /// </summary>
    protected override void LoopRefreshCharacter()
    {
        if (mLoopList.Count == 0) return;

        List<LoopRefresh> coins = new List<LoopRefresh>();
        List<LoopRefresh> citizen = new List<LoopRefresh>();
        List<LoopRefresh> others = new List<LoopRefresh>();
        foreach (LoopRefresh lr in mLoopList)
        {
            if (lr.characterType == E_CharacterType.Coin)
                coins.Add(lr);
            else if (lr.characterType == E_CharacterType.Citizen0 ||
                    lr.characterType == E_CharacterType.Citizen1 ||
                    lr.characterType == E_CharacterType.Citizen2)
                citizen.Add(lr);
            else
                others.Add(lr);
        }

        mSpawnCoinTimer -= Time.deltaTime;
        mSpawnOtherTimer -= Time.deltaTime;
        // 刷金币外的单元
        if (mSpawnOtherTimer <= 0 && others.Count > 0)
        {
            mSpawnOtherTimer = mSpawnOtherTime;
            int index = UnityEngine.Random.Range(0, others.Count);
            SpawnCharacter(others[index]);
        }
        // 刷金币
        if (mSpawnCoinTimer <= 0)
        {
            mSpawnCoinTimer = mSpawnCoinTime;
            foreach (LoopRefresh lr in coins)
            {
                SpawnCharacter(lr);
            }
        }
        // 市民 (只有需要飞机救援的市民才需要循环刷，且一定时间内只有一个，citizen链表中，所有要刷新市民的概率和为1)
        if (ioo.characterSystem.GetCitizenTypeIsWaittingForHelpCount() < mCitizenRefreshStrategy.GetMonsterCount(mLv))
        {
            bool spawned = false;
            int rand = Util.Random(0, 100);
            // 随机刷新一个市民
            foreach (LoopRefresh lr in citizen)
            {
                if (lr.interval < lr.characterRefreshPO.Interval)
                    lr.interval += Time.deltaTime;
                else
                {
                    if (rand >= lr.characterRefreshPO.RefreshRate[0] && rand < lr.characterRefreshPO.RefreshRate[1])
                    {
                        spawned = true;
                        SpawnCharacter(lr);
                    }
                }
            }
            // 重置刷新时间
            if (spawned)
            {
                foreach (LoopRefresh lr in citizen)
                    lr.interval = 0;
            }
        }
    }

    /// <summary>
    /// 刷新指定怪物(循环刷新的怪物有大小火怪，烟雾怪，和精英怪)
    /// </summary>
    /// <param name="index"></param>
    private void SpawnCharacter(LoopRefresh lr)
    {
        #region 对ActionType单一的Character依据CharacterType刷新
        // 循环刷新金币
        if (lr.characterType == E_CharacterType.Coin)
        {
            ISpawnCommand command = new SpawnCoinCommand(lr.characterID, lr.characterRefreshPO);
            mCommands.Add(command);
            return;
        }

        // 循环刷新能劫持玩家的精英怪
        if (lr.characterType == E_CharacterType.Elite)
        {
            int count = ioo.characterSystem.GetNormalEliteMonsterCount();
            if (count < mEliteMonsterRefreshStrategy.GetMonsterCount(mLv))
            {
                ISpawnCommand command = new SpawnEliteMonsterCommand(lr.characterID, lr.characterRefreshPO);
                mCommands.Add(command);
            }
            return;
        }

        // 循环刷新能劫持玩家的狼
        if(lr.characterType == E_CharacterType.Wolf)
        {
            int count = ioo.characterSystem.GetWolfCount();
            if (count < mWolfRefreshStrategy.GetMonsterCount(mLv))
            {
                ISpawnCommand command = new SpawnWolfCommand(lr.characterID, lr.characterRefreshPO);
                mCommands.Add(command);
            }
        }
        #endregion

        #region 对ActionType多元的Character依据ActionType进行刷新
        E_ActionType type = (E_ActionType)lr.characterRefreshPO.ActionType;
        // 刷新攻击市民 和 npc
        if (ioo.characterSystem.CanSpawnSpecialHugeMonster())
        {
            if (type == E_ActionType.AttackCitizen || type == E_ActionType.AttackNpc)
            {
                switch (lr.characterType)
                {
                    case E_CharacterType.HugeFireMonster:
                        ISpawnCommand command = new SpawnHugeFireMonsterCommand(lr.characterID, lr.characterRefreshPO);
                        mCommands.Add(command);
                        break;
                }
                return;
            }
        }


        // 等待直升机救援的市民
        if (type == E_ActionType.WaitForHelp)
        {
            ISpawnCommand command = new SpawnCitizenCommand(lr.characterID, lr.characterRefreshPO);
            mCommands.Add(command);
            return;
        }

        // 循环刷新攻击玩家的大小火怪和烟雾怪
        if (type == E_ActionType.Normal)
        {
            int count = ioo.characterSystem.GetNormalFlyMonsterCout();
            if (count < mFireMonsterRefreshStrategy.GetMonsterCount(mLv))
            {
                switch (lr.characterType)
                {
                    case E_CharacterType.MiniFireMonster:
                    case E_CharacterType.SmokeMonster:
                        ISpawnCommand command = new SpawnFireMonsterCommand(lr.characterID, lr.characterRefreshPO);
                        mCommands.Add(command);
                        break;
                    case E_CharacterType.HugeFireMonster:
                        command = new SpawnHugeFireMonsterCommand(lr.characterID, lr.characterRefreshPO);
                        mCommands.Add(command);
                        break;
                }
            }
            return;
        }
        #endregion
    }
}