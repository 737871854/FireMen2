/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Prop.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 14:58:48
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class Prop : ICharacter
{
    public override void UnderAttack(Player player)
    {
        if (mIsKilled || mIsInvincible) return;
        base.UnderAttack(player);
        if (mAttr.currentHP <= 0)
        {
            DoPlayBeAttackedEffect();
            if (mAttr.currentHP <= 0)
            {
                mPlayerKill = player;
                Killed();
            }
        }
    }

    public override void Killed()
    {
        base.Killed();
        DoPlayBeDestroyEffectSound();
        DoPlayBeDestroySound();
        int[] args = new int[] { mPlayerKill.id, attr.baseAttr.id, attr.baseAttr.worth };
        ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
        if(attr.baseAttr.characterType == E_CharacterType.SandBox 
            || attr.baseAttr.characterType == E_CharacterType.Extinguisher 
            || attr.baseAttr.characterType == E_CharacterType.Hydrant)
        {
            args = new int[] { (int)attr.baseAttr.characterType};
            ioo.gameEventSystem.NotifySubject(GameEventType.PullWater, args);
        }
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        switch (attr.baseAttr.characterType)
        {
            case E_CharacterType.Coin:
                break;
            case E_CharacterType.Support:
                break;
            case E_CharacterType.Freeze:
                break;
        }

    }
}