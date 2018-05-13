/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CharacterBaseAttr.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/2 11:22:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CharacterBaseAttr
{
    protected int mID;
    protected string mName;
    protected int mMaxHP;
    protected string mPrefabName;
    protected float mBaseSpeed;
    protected float mBaseRotationSpeed;
    protected int mWorth;
    protected int mDamageValue;
    protected string mBornEffectName;
    protected string mBeAttackedEffectName;
    protected string mDeadEffectName;
    protected string mExplodeEffectSoundName;
    protected string mBeDestroyEffectSoundName;
    protected string[] mBeDestroySoundName;
    protected E_CharacterType mCharacterType;
    protected float mDisappearTime;
    protected float mValidTime;
    protected float mFogTime;
    protected float mFreezeTime;
    protected float mOffset_Y;
    protected int mHeadID;
    protected int mMapID;
    protected string[] mHitBone;

    public CharacterBaseAttr(CharacterPO agentPO)
    {
        mID                 = agentPO.Id;
        mPrefabName         = agentPO.Name;
        mCharacterType      = (E_CharacterType)agentPO.Type;
        mBaseSpeed          = agentPO.BaseSpeed;
        mBaseRotationSpeed  = agentPO.RotationSpeed;
        mMaxHP              = agentPO.Health;
        mWorth              = agentPO.Score;
        mDamageValue        = agentPO.DamageValue;
        mBornEffectName     = agentPO.BornEffect;
        mBeAttackedEffectName   = agentPO.HitEffect;
        mDeadEffectName         = agentPO.DieEffet;
        mExplodeEffectSoundName = agentPO.ExplodeEffectSound;
        mBeDestroyEffectSoundName   = agentPO.DestroyEffectSound;
        mBeDestroySoundName         = agentPO.DestroySound;
        mDisappearTime              = agentPO.DisappearTime;
        mValidTime          = agentPO.ValidTime;
        mFogTime            = agentPO.FogTime;
        mFreezeTime         = agentPO.FreezeTime;
        mOffset_Y           = agentPO.Offset_Y;
        mHeadID             = agentPO.HeadID;
        mMapID              = agentPO.MapID;
        mHitBone            = agentPO.HitBone;
    }
    public int id { get { return mID; } }
    public string name { get { return mName; } }
    public int maxHP { get { return mMaxHP; } }
    public string prefabName { get { return mPrefabName; } }
    public float baseSpeed { get { return mBaseSpeed; } }
    public float baseRotationSpeed { get { return mBaseRotationSpeed; } }
    public int worth { get { return mWorth; } }
    public int damageValue { get { return mDamageValue; } }
    public string bornEffectName { get { return mBornEffectName; } }
    public string beAttackedEffectName { get { return mBeAttackedEffectName; } }
    public string deadEffectName { get { return mDeadEffectName; } }
    public string explodeEffectSoundName { get { return mExplodeEffectSoundName; } }
    public string beDestroyEffectSoundName { get { return mBeDestroyEffectSoundName; } }
    public string[] beDestroySoundName { get { return mBeDestroySoundName; } }
    public E_CharacterType characterType { get { return mCharacterType; } }
    public float disappearTime { get { return mDisappearTime; } }
    public float validTime { get { return mValidTime; } }
    public float fogTime { get { return mFogTime; } }
    public float freezeTime { get { return mFreezeTime; } }
    public float offset_Y { get { return mOffset_Y; } }
    public int headID { get { return mHeadID; } }
    public int mapID { get { return mMapID; } }
    public string[] hitBone { get { return mHitBone; } }
}
