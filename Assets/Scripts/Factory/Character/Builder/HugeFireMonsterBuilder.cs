/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HugeFireMonsterBuilder.cs
 * 
 * 简    介:    构建大火怪
 * 
 * 创建标识：   Pancake 2018/4/8 9:24:53
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HugeFireMonsterBuilder : ICharacterBuilder
{
    public HugeFireMonsterBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new HugeFireMonsterAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        mSpawnLocalEuler = attrStrategy.GetEulerAngle(mCharacterRefreshPO);
        ICharacterAttr attr = new HugeFireMonsterAttr(attrStrategy, baseAttr);
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
        mCharacter.bornPosition = mSpawnPosition;
        mCharacter.gameObject = characterGO;
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddHugeFireMonster(mCharacter as HugeFireMonster);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}