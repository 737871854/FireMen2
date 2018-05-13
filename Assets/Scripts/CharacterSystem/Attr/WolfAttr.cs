/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfAttr.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 16:27:14
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class WolfAttr : ICharacterAttr
{
    public WolfAttr(IAttrStrategy strategy, CharacterBaseAttr baseAttr) : base(strategy, baseAttr)
    {
    }
}