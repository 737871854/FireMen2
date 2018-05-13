/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ICharacterBuilder.cs
 * 
 * 简    介:    角色构建基类
 * 
 * 创建标识：   Pancake 2018/3/2 11:32:31
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterBuilder
{
    protected int mCharacterID;
    protected ICharacter mCharacter;
    protected CharacterRefreshPO mCharacterRefreshPO;

    protected string mPrefabName = "";
    protected Vector3 mSpawnPosition;
    protected Vector3 mSpawnLocalEuler;

    public ICharacterBuilder(ICharacter character,int characterID, CharacterRefreshPO characterRefreshPO)
    {
        mCharacter = character;
        mCharacterID = characterID;
        mCharacterRefreshPO = characterRefreshPO;
    }

    public abstract void AddCharacterAttr();
    public abstract void AddGameObject();
    public abstract void AddInCharacterSystem();
    public abstract ICharacter GetResult();
}