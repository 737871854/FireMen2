/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Coin.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/13 14:08:54
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class Coin : ICharacter
{
    private Vector3 mDirector;
    public Coin()
    {
    }

    protected override void Init()
    {
        base.Init();
        if (Vector3.Distance(mGameObject.transform.position, ioo.battleScene.circle0.transform.position) < Vector3.Distance(mGameObject.transform.position, ioo.battleScene.circle1.transform.position))
            mDirector = ioo.battleScene.circle0.Direction;
        else
            mDirector = ioo.battleScene.circle1.Direction;
    }

    public override void UnderAttack(Player player)
    {
        if (mIsKilled || mIsInvincible) return;
        base.UnderAttack(player);
        DoPlayBeAttackedEffect();
        if(mAttr.currentHP <= 0)
        {
            mPlayerKill = player;
            Killed();
        }
    }

    public override void Killed()
    {
        base.Killed();
        DoPlayBeDestroyEffectSound();
        DoPlayBeDestroySound();
        if (mPlayerKill == null) return;
        int[] args = new int[] { mPlayerKill.id, attr.baseAttr.id, attr.baseAttr.worth };
        ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        float speed = attr.baseAttr.baseSpeed;
        float rotationSpeed = attr.baseAttr.baseRotationSpeed;
        mGameObject.transform.position += mDirector.normalized * Time.deltaTime * speed - Vector3.up * 0.4f * Time.deltaTime;
        mGameObject.transform.localEulerAngles += rotationSpeed * Time.deltaTime * Vector3.right;
    }
}