/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CoinBuilder.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/13 14:11:04
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinBuilder : ICharacterBuilder
{
    public CoinBuilder(ICharacter character,int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new CoinAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        ICharacterAttr attr = new CoinAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
        mCharacter.InitRefreshData((E_ActionType)mCharacterRefreshPO.ActionType, mCharacterRefreshPO.AppeareArea, mCharacterRefreshPO.FactorSpeed, mCharacterRefreshPO.DisappearTime);
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
        ioo.characterSystem.AddCoin(mCharacter as Coin);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}