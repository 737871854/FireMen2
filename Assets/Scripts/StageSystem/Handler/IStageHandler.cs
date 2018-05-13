/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IStageHandler.cs
 * 
 * 简    介:    场景管理
 * 
 * 创建标识：   Pancake 2018/3/5 9:20:46
 * 
 * 修改描述：   依据策划文案废弃关卡切换条件(改为定时切换) 2018/3/25 @Pancake
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class IStageHandler
{
    /// <summary>
    /// 续循环刷新的怪物信息
    /// </summary>
    protected class LoopRefresh
    {
        public LoopRefresh(int characterID, string name, E_CharacterType type, CharacterRefreshPO characterRefreshPO)
        {
            mCharacterID = characterID;
            mPrefabName = name;
            mCharacterType = type;
            mCharacterRefreshPo = characterRefreshPO;
        }
        private int mCharacterID;
        private string mPrefabName;
        private CharacterRefreshPO mCharacterRefreshPo;
        private E_CharacterType mCharacterType;
        private float mInterval;

        public int characterID { get { return mCharacterID; } }
        public string prefabName { get { return mPrefabName; } }
        public CharacterRefreshPO characterRefreshPO { get { return mCharacterRefreshPo; } }
        public E_CharacterType characterType { get { return mCharacterType; } }
        public float interval { get { return mInterval; } set { mInterval = value; } }
    }

    protected int mLv;

    protected StageSystem mStageSystem;
    protected IStageHandler mNextHandler;

    protected IRefreshStrategy mFireMonsterRefreshStrategy;
    protected IRefreshStrategy mEliteMonsterRefreshStrategy;
    protected IRefreshStrategy mCitizenRefreshStrategy;
    protected IRefreshStrategy mWolfRefreshStrategy;

    protected IStrategyLevelPass mStrategyLevelPass;

    protected List<ISpawnCommand> mCommands = new List<ISpawnCommand>();
    protected List<LoopRefresh> mLoopList = new List<LoopRefresh>();

    protected float mStageTimer = 0;

    public IStageHandler(StageSystem stageSystem,int lv)
    {       
        mStageSystem = stageSystem;
        mLv = lv;
        CrateStrategyTime();
    }

    private void CrateStrategyTime()
    {
        switch(mStageSystem.sceneName)
        {
            case SceneNames.SelectScene:
                mStrategyLevelPass = new SelectStrategyTime();
                break;
            case SceneNames.BattleScene0_0:
                mStrategyLevelPass = new TownStrategyTime();
                break;
            case SceneNames.BattleScene0_2:
                mStrategyLevelPass = new BullDemonKingStrategyTime();
                break;
            case SceneNames.BattleScene1:
                mStrategyLevelPass = new FarmStrategyTime();
                break;
            case SceneNames.BattleScene2:
                mStrategyLevelPass = new OceanStrategyTime();
                break;
        }
    }

    public IStageHandler SetNextHandler(IStageHandler handler)
    {
        mNextHandler = handler;
        return mNextHandler;
    }

    public void Handle(int level)
    {
        if(mCommands.Count != 0)
        {
            mCommands[0].Execute();
            mCommands.RemoveAt(0);
        }

        if(level == mLv)
        {
            RefreshCharacter();
            LoopRefreshCharacter();
            UpdateStage();
            CheckIsFinished();
        }
        else
        {
            if(mNextHandler != null)
                mNextHandler.Handle(level);
            else
            {
            }
        }
    }

    protected virtual void UpdateStage() { }
    protected virtual void LoopRefreshCharacter() { }
    protected virtual void NewStage() { mStageSystem.EnterNextStage(); }

    /// <summary>
    /// 检查是否满足过关条件
    /// </summary>
    private void CheckIsFinished()
    {
        if (mStageTimer >= mStrategyLevelPass.GetTime(mLv) || mStrategyLevelPass.HasKillCondition(mLv))
            NewStage();
        else if (ioo.StagetyCanUpdate)
            mStageTimer += Time.deltaTime;
    }
    /// <summary>
    /// 按照刷新表动态刷新场景活动单元
    /// </summary>
    void RefreshCharacter()
    {
        while (true)
        {
            int refreshID = 100000 + ((int)mStageSystem.sceneID) * 10000 + mStageSystem.refreshIndex;
            CharacterRefreshPO characterRefreshPO = CharacterRefreshData.Instance.GetCharacterRefreshPO(refreshID);
            if (characterRefreshPO == null) break;
            if (characterRefreshPO.Stage != mLv) break;

            if (characterRefreshPO.AppeareTime > mStageTimer) return;

            CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(characterRefreshPO.CharacterID);
            if (characterRefreshPO.Loop == 1)
            {
                LoopRefresh lr = new LoopRefresh(characterRefreshPO.CharacterID, baseAttr.name, baseAttr.characterType, characterRefreshPO);
                mLoopList.Add(lr);
                ++mStageSystem.refreshIndex;
                continue;
            }

            E_CharacterType characterType = baseAttr.characterType;
            switch (characterType)
            {
                case E_CharacterType.Player:
                    ISpawnCommand command = new SpawnCharacterCommand(baseAttr.id, characterRefreshPO);
                    mCommands.Add(command);
                    break;
                case E_CharacterType.Map:
                    command = new SpawnMapCommand(baseAttr.id, characterRefreshPO);
                    mCommands.Add(command);
                    break;
                case E_CharacterType.Citizen0:
                case E_CharacterType.Citizen1:
                case E_CharacterType.Citizen2:
                    command = new SpawnCitizenCommand(baseAttr.id, characterRefreshPO);
                    mCommands.Add(command);
                    break;
                case E_CharacterType.Npc:
                    for(int i = 0; i < characterRefreshPO.StepCount; ++i)
                    {
                        command = new SpawnNpcCommand(baseAttr.id, characterRefreshPO);
                        mCommands.Add(command);
                    }
                    break;
                case E_CharacterType.SandBox:
                case E_CharacterType.Bucket:
                    command = new SpawnPropCommand(baseAttr.id, characterRefreshPO, Vector3.zero);
                    mCommands.Add(command);
                    break;
                case E_CharacterType.BullDemonKing:
                    command = new SpawnBullDemonKingCommand(baseAttr.id, characterRefreshPO);
                    mCommands.Add(command);
                    break;
                case E_CharacterType.AngerBear:
                    command = new SpawnBearCommand(baseAttr.id, characterRefreshPO);
                    mCommands.Add(command);
                    break;
                case E_CharacterType.Wolf:
                    command = new SpawnWolfCommand(baseAttr.id, characterRefreshPO);
                    mCommands.Add(command);
                    break;
            }
            ++mStageSystem.refreshIndex;
        }
    }
}