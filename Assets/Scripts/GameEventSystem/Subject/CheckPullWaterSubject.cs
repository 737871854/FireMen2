/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CheckPullWaterSubject.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/23 9:00:23
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CheckPullWaterSubject : IGameEventSubject
{
    private E_CharacterType mCharacterType;

    public E_CharacterType characterType { get { return mCharacterType; } }

    public override void Notify(params int[] args)
    {
        mCharacterType = (E_CharacterType)args[0];
        base.Notify(args);
    }
}