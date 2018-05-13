/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PlayerData.cs
 * 
 * 简    介:    玩家所有状态以及数据信息
 * 
 * 创建标识：   Pancake 2017/9/20 15:04:31
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// 可配表，
/// </summary>
public class PlayerData
{
    public PlayerData()
    {
        mMaxHealth = Define.GAME_CONFIG_PLAYER_MAX_HEALTH;
    }

    /// <summary>
    /// 玩家所在区域
    /// </summary>
    private int mLocation;
    public int location { get { return mLocation; } }

    /// <summary>
    /// 最大生命值
    /// </summary>
    private int mMaxHealth;

    /// <summary>
    /// 基础攻击力
    /// </summary>
    private int mBaseAttack = 1;

    /// <summary>
    /// 超级水炮攻击力
    /// </summary>
    private int mSupperWaterAttack = 2;

    /// <summary>
    /// 续币允许最大时间
    /// </summary>
    private int mMaxContinue = 10;

    /// <summary>
    /// 持续射击时间
    /// </summary>
    private float mFireTime;
    public float fireTime { get { return mFireTime; } set { mFireTime = value; } }

    /// <summary>
    /// 续币倒计时时间
    /// </summary>
    public float continueTime { get; set; }

    /// <summary>
    /// 玩家ID
    /// </summary>
    private int mID;
    public int id { get { return mID; } }

    /// <summary>
    /// 头像
    /// </summary>
    private int mHead;
    public int head { get { return mHead; } }

    /// <summary>
    /// 玩家状态
    /// </summary>
    public E_PlayerState State { get; set; }

    /// <summary>
    /// 得分
    /// </summary>
    private int mScore;
    public int Score { get{return mScore; }}

    /// <summary>
    /// 生命值
    /// </summary>
    private int mHealth;
    public int health { get { return mHealth; } }

    /// <summary>
    /// 水量剩余百分比
    /// </summary>
    public float HPProgress { get { return (1.0f * mHealth) / mMaxHealth; } }

    /// <summary>
    /// 攻击力
    /// </summary>
    private int mAttackValue;
    public int attackValue { get { return mAttackValue; } }

    /// <summary>
    /// 水标坐标
    /// </summary>
    public Vector3 position { get; set; }

    /// <summary>
    /// 设置玩家ID和区域
    /// </summary>
    /// <param name="id"></param>
    public void Init(int id)
    {
        mID = id;
        mLocation = id;
    }

    /// <summary>
    /// 重置玩家id以外的数据
    /// </summary>
    public void Reset()
    {
        mHead = -1;
        mScore = 0;
        State = E_PlayerState.Waitting;
        mAttackValue = mBaseAttack;
    }

    public  void AddCoin()
    {
        continueTime = mMaxContinue; 
    }

    /// <summary>
    /// 设置头像
    /// </summary>
    /// <param name="head"></param>
    public void SetHead(int head)
    {
        mHead = head;
    }

    /// <summary>
    /// 改变玩家爱状态
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(E_PlayerState state)
    {
        if (State == state)
            return;

        State = state;
        switch (state)
        {
            case E_PlayerState.Dead:
                ContinueToDead();
                break;
        }
    }

    /// <summary>
    /// 进入超级水炮状态
    /// </summary>
    public void SupperWater()
    {
        mAttackValue = mSupperWaterAttack;
    }

    /// <summary>
    /// 结束超级水炮状态
    /// </summary>
    public void EndSupperWater()
    {
        mAttackValue = mBaseAttack;
    }

    public void UpdateScore(int value)
    {
        mScore = value;
    }

    /// <summary>
    /// 改变玩家爱生命值
    /// </summary>
    public void ChangeHealth(int value)
    {
        mHealth = value;
        if (mHealth < 0)
            mHealth = 0;

        if (mHealth > mMaxHealth)
            mHealth = mMaxHealth;

        if (mHealth == 0)
            ChangeState(E_PlayerState.Continue);
    }

    /// <summary>
    /// 加水
    /// </summary>
    /// <param name="value"></param>
    public void FillWater(int value)
    {
        mHealth += value;
        if (mHealth > mMaxHealth)
            mHealth = mMaxHealth;
    }

    /// <summary>
    /// 耗币
    /// </summary>
    public void UseCoin()
    {
        switch (State)
        {
            case E_PlayerState.Waitting:
                WaittingToPlay();
                break;
            case E_PlayerState.Continue:
                ContinueToPlay();
                break;
            case E_PlayerState.Dead:
                DeadToPlay();
                break;
        }

        ChangeState(E_PlayerState.Play);
    }

    /// <summary>
    /// 处于可玩状态
    /// </summary>
    /// <returns></returns>
    public bool IsPlay()
    {
        return State == E_PlayerState.Play;
    }

    /// <summary>
    /// 处于续币倒计时状态
    /// </summary>
    /// <returns></returns>
    public bool IsContinue()
    {
        return State == E_PlayerState.Continue;
    }

    /// <summary>
    /// 劫持
    /// </summary>
    /// <returns></returns>
    public bool IsHold()
    {
        return State == E_PlayerState.Hold;
    }

    /// <summary>
    /// 处于死亡状态
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return State == E_PlayerState.Dead;
    }

    /// <summary>
    /// 等待状态
    /// </summary>
    /// <returns></returns>
    public bool IsWaiting()
    {
        return State == E_PlayerState.Waitting;
    }

    #region Private Function
    /// <summary>
    /// 初始化
    /// </summary>
    private void WaittingToPlay()
    {
        mScore = 0;
        mHealth = mMaxHealth;
        mAttackValue = mBaseAttack;
        continueTime = 10;
    }

    /// <summary>
    /// 续币
    /// </summary>
    private void ContinueToPlay()
    {
        mHealth = mMaxHealth;
        continueTime = 10;
    }

    /// <summary>
    /// 重玩
    /// </summary>
    private void DeadToPlay()
    {
        mHealth = mMaxHealth;
        mScore = 0;
        mAttackValue = mBaseAttack;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void ContinueToDead()
    {
        mScore = 0;
    }
    #endregion
}
