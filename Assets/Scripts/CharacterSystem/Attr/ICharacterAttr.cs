/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ICharacterAttr.cs
 * 
 * 简    介:    角色属性基类
 * 
 * 创建标识：   Pancake 2018/3/2 11:06:15
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class ICharacterAttr
{
    // 基础属性，由配表填充
    protected CharacterBaseAttr mBaseAttr;

    protected int mCurrentHP;

    protected IAttrStrategy mStrategy;
    public ICharacterAttr(IAttrStrategy strategy, CharacterBaseAttr baseAttr)
    {
        mStrategy = strategy;
        mBaseAttr = baseAttr;
        mCurrentHP = baseAttr.maxHP;/* + mStrategy.GetExtraHPValue()*/
    }

    public int currentHP { get { { return mCurrentHP; } } }
    public IAttrStrategy strategy { get { return mStrategy; } }
    public CharacterBaseAttr baseAttr { get { return mBaseAttr; } }

    public void TakeDamage(int damage)
    {
        mCurrentHP -= damage;
        mCurrentHP = mCurrentHP < 0 ? 0 : mCurrentHP;
    }

    public void ReplyHealth()
    {
        mCurrentHP = baseAttr.maxHP;
    }

}