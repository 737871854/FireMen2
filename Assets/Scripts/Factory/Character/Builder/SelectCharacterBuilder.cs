/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectCharacterBuilder.cs
 * 
 * 简    介:    构建角色
 * 
 * 创建标识：   Pancake 2018/3/2 17:03:33
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterBuilder : ICharacterBuilder
{
    public SelectCharacterBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new SelectCharacterAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        ICharacterAttr attr = new SelectCharacterAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
    }

    public override void AddGameObject()
    {
        GameObject characterGO = PoolManager.Instance.Spawn(mPrefabName);
        characterGO.transform.position = mSpawnPosition;
        mCharacter.gameObject = characterGO;
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddSelectCharacter(mCharacter as SelectCharacter);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}