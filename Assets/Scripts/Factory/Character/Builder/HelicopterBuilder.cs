/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HelicopterBuilder.cs
 * 
 * 简    介:    构建直升机
 * 
 * 创建标识：   Pancake 2018/3/21 10:31:45
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBuilder : ICharacterBuilder
{
    public HelicopterBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new HelicopterAttrStrategy();
        //mSpawnLocalEuler = attrStrategy.GetEulerAngle(mCharacterRefreshPO);
        HelicopterAttr attr = new HelicopterAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
        mCharacter.InitRefreshData(E_ActionType.UnKonw, null, 1, -1);
    }

    public override void AddGameObject()
    {
        GameObject characterGO = PoolManager.Instance.Spawn(mPrefabName);
        //characterGO.transform.localEulerAngles = mSpawnLocalEuler;
        //characterGO.transform.localScale = Vector3.one * mCharacterRefreshPO.BegineLocalScale;
        //characterGO.transform.DOScale(Vector3.one * mCharacterRefreshPO.TargetLocalScale, mCharacterRefreshPO.LocalScaleTime);
        mCharacter.gameObject = characterGO;
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddHelicopter(mCharacter as Helicopter);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}