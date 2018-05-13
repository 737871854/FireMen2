/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PropAttr.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 16:01:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class PropAttr : ICharacterAttr
{
    public PropAttr(IAttrStrategy strategy, CharacterBaseAttr baseAttr) : base(strategy, baseAttr)
    {
    }
}