/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingBuilder.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 15:55:36
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKingBuilder : ICharacterBuilder
{
    public BullDemonKingBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new BullDemonKingAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        mSpawnLocalEuler = attrStrategy.GetEulerAngle(mCharacterRefreshPO);
        ICharacterAttr attr = new BullDemonKingAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
        mCharacter.InitRefreshData((E_ActionType)mCharacterRefreshPO.ActionType, mCharacterRefreshPO.AppeareArea, mCharacterRefreshPO.FactorSpeed, mCharacterRefreshPO.DisappearTime, mCharacterRefreshPO.StayArea);
    }

    public override void AddGameObject()
    {
        GameObject characterGO = PoolManager.Instance.Spawn(mPrefabName);
        characterGO.transform.position = mSpawnPosition;
        characterGO.transform.localEulerAngles = mSpawnLocalEuler;
        characterGO.transform.localScale = Vector3.one * mCharacterRefreshPO.BegineLocalScale;
        characterGO.transform.DOScale(Vector3.one * mCharacterRefreshPO.TargetLocalScale, mCharacterRefreshPO.LocalScaleTime);
        mCharacter.gameObject = characterGO;

        string doorName = mCharacterRefreshPO.WindowName;
        if (mCharacterRefreshPO.WindowName != "")
            DoorManager.Instance.OpenDoor(doorName);
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddBoss(mCharacter);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}