/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterBuilder.cs
 * 
 * 简    介:    构建精英怪
 * 
 * 创建标识：   Pancake 2018/3/22 9:35:28
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonsterBuilder : ICharacterBuilder
{
    public EliteMonsterBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new EliteMonsterAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        EliteMonsterAttr attr = new EliteMonsterAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
        mCharacter.InitRefreshData((E_ActionType)mCharacterRefreshPO.ActionType, mCharacterRefreshPO.AppeareArea, mCharacterRefreshPO.FactorSpeed, mCharacterRefreshPO.DisappearTime,mCharacterRefreshPO.StayArea);
    }

    public override void AddGameObject()
    {
        GameObject characterGO = PoolManager.Instance.Spawn(mPrefabName);
        characterGO.transform.position = mSpawnPosition;
        characterGO.transform.localScale = Vector3.one * mCharacterRefreshPO.BegineLocalScale;
        characterGO.transform.DOScale(Vector3.one * mCharacterRefreshPO.TargetLocalScale, mCharacterRefreshPO.LocalScaleTime);
        mCharacter.gameObject = characterGO;
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddEliteMonster(mCharacter as EliteMonster);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}