/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearAttr.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/25 9:17:26
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearAttr : ICharacterAttr
{
    public BearAttr(IAttrStrategy strategy, CharacterBaseAttr baseAttr) : base(strategy, baseAttr)
    {
    }
}