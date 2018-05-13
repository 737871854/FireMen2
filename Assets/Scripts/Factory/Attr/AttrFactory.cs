/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   AttrFactory.cs
 * 
 * 简    介:    属性工厂
 * 
 * 创建标识：   Pancake 2018/3/2 13:36:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class AttrFactory : IAttrFactory
{
    private int mCharacterID = 1000;
    private Dictionary<int, CharacterBaseAttr> mCharacterBaseAttrDict;
    public AttrFactory()
    {
        InitCharacterBaseAttr();
    }

    private void InitCharacterBaseAttr()
    {
        mCharacterBaseAttrDict = new Dictionary<int, CharacterBaseAttr>();
        while(true)
        {
            CharacterPO agentPO = CharacterData.Instance.GetCharacterPO(mCharacterID);
            if (agentPO == null) return;
            mCharacterBaseAttrDict.Add(mCharacterID, new CharacterBaseAttr(agentPO));
            ++mCharacterID;
        }
    }

    public CharacterBaseAttr GetCharacterBaseAttr(int characterID)
    {
        if (mCharacterBaseAttrDict.ContainsKey(characterID) == false)
        {
            Debug.LogError("无法根据类型:" + characterID + "得到角色基础属性(GetCharacterBaseAttr)"); return null;
        }
        return mCharacterBaseAttrDict[characterID];
    }

}