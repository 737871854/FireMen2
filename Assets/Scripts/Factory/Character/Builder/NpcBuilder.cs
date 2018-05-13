/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   NpcBuilder.cs
 * 
 * 简    介:    构建NPC
 * 
 * 创建标识：   Pancake 2018/3/20 16:49:45
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcBuilder : ICharacterBuilder
{
    public NpcBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        mPrefabName = mPrefabName + Util.Random(0, 5);
        IAttrStrategy attrStrategy = new NpcAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        mSpawnLocalEuler = attrStrategy.GetEulerAngle(mCharacterRefreshPO);
        ICharacterAttr attr = new NpcAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
        mCharacter.InitRefreshData((E_ActionType)mCharacterRefreshPO.ActionType, mCharacterRefreshPO.AppeareArea, mCharacterRefreshPO.FactorSpeed, mCharacterRefreshPO.DisappearTime);
    }

    public override void AddGameObject()
    {
        GameObject characterGO = PoolManager.Instance.Spawn(mPrefabName);
        characterGO.transform.position = mSpawnPosition;
        characterGO.transform.localEulerAngles = mSpawnLocalEuler;
        characterGO.transform.localScale = Vector3.one * mCharacterRefreshPO.BegineLocalScale;
        characterGO.transform.DOScale(Vector3.one * mCharacterRefreshPO.TargetLocalScale, mCharacterRefreshPO.LocalScaleTime);
        mCharacter.gameObject = characterGO;
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddNpc(mCharacter as Npc);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}