/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Player.cs
 * 
 * 简    介:    玩家控制类
 * 
 * 创建标识：   Pancake 2017/4/27 11:04:38
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections;
using System;

public class Player
{
    private enum State
    {
        Normal,
        SupperWater,
    }

    private PlayerData mData = new PlayerData();

    private State mState = State.Normal;

    private float mSupperWaterTime;

    public int attackValue    { get { return mData.attackValue; } }
    public PlayerData data      { get { return mData; } }
    public int id               { get { return mData.id; } }
    public float FireTime       { get { return mData.fireTime; } set { mData.fireTime = value; } }
    public float HPProgress     { get { return mData.HPProgress; } }
    public int continueTime { get { return (int)mData.continueTime; } }
    public int Score            { get { return mData.Score; } }
    public Vector3 position { get { return mData.position; } set { mData.position = value; } }
    public int location { get { return mData.location; } }

    #region Public Function
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init(int id)
    {
        mData.Init(id);
        Reset();
    }

    /// <summary>
    /// 重置玩家id除外的数据
    /// </summary>
    public void Reset()
    {
        mData.Reset();
    }

    /// <summary>
    /// 消耗币
    /// </summary>
    public void UseCoin()
    {
        mData.UseCoin();
    }

    /// <summary>
    /// 帧更新
    /// </summary>
    public void UpdatePreFrame()
    {
        //mPos = Input.mousePosition;      
    }

    /// <summary>
    /// 正在玩
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return mData.IsPlay();
    }

    /// <summary>
    /// 续币倒计时
    /// </summary>
    /// <returns></returns>
    public bool IsContinue()
    {
        return mData.IsContinue();
    }

    /// <summary>
    /// 劫持状态
    /// </summary>
    /// <returns></returns>
    public bool IsHold()
    {
        return mData.IsHold();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return mData.IsDead();
    }

    /// <summary>
    /// 等待
    /// </summary>
    /// <returns></returns>
    public bool IsWaiting()
    {
        return mData.IsWaiting();
    }

    internal void AddCoin()
    {
        mData.AddCoin();
    }

    /// <summary>
    /// 超级水炮状态
    /// </summary>
    /// <returns></returns>
    public bool IsSupperWater()
    {
        return mState == State.SupperWater;
    }

    /// <summary>
    /// 设置头像
    /// </summary>
    /// <param name="head"></param>
    public void SetHead(int head)
    {
        mData.SetHead(head);
    }

    /// <summary>
    /// 是否设置过了头像
    /// </summary>
    /// <returns></returns>
    public bool HasHead()
    {
        if (mData.head != -1)
            return true;
        return false;
    }

    /// <summary>
    /// 加水
    /// </summary>
    /// <param name="value"></param>
    public void FillWater(int value)
    {
        mData.FillWater(value);
    }

    /// <summary>
    /// 固定帧更新
    /// </summary>
    public void UpdateFexedFrame()
    {
        if (mData.State == E_PlayerState.Continue)
        {
            if (mData.continueTime > 0)
                mData.continueTime -= Time.fixedDeltaTime;
            else
            {
                mData.continueTime = 0;
                mData.ChangeState(E_PlayerState.Dead);
            }
        }

        if (mState == State.SupperWater)
        {
            if (mSupperWaterTime > 0)
                mSupperWaterTime -= Time.deltaTime;
            else
            {
                mState = State.Normal;
                EndSupperWater();
            }
        }
    }

    /// <summary>
    /// 更新分数
    /// </summary>
    /// <param name="value"></param>
    public void UpdateScore(int value)
    {
        mData.UpdateScore(value);
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage)
    {
        if (mData.health == 0)
            return;

        int value = mData.health - damage;
        mData.ChangeHealth(value);
    }

    /// <summary>
    /// 超级水炮
    /// </summary>
    public void OnSupperWater(float time)
    {
        mState = State.SupperWater;
        mSupperWaterTime = time;
        mData.SupperWater();
        // 向下位机发送消息
        Debugger.Log("超级水炮，玩家："+mData.id);
    }
    #endregion

    #region Private Function
    /// <summary>
    /// 结束超级水炮
    /// </summary>
    private void EndSupperWater()
    {
        mData.EndSupperWater();
        // 向下位机发送消息
        Debugger.Log("结束超级水炮，玩家：" + mData.id);
    }
    #endregion
}
