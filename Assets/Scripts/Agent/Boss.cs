/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BossBase.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/4/25 13:58:03
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class Boss : AdvanceFSM
{
    // HittingPart
    protected bool hpIsActive;
    public bool HPIsActive { get { return hpIsActive; } }

    protected Dictionary<int, float> damageDic = new Dictionary<int, float>();
    protected void AddDamageValue(int key, float value)
    {
        if (!damageDic.ContainsKey(key))
            damageDic.Add(key, value);
        else
        {
            damageDic[key] += value;
        }
    }
    public float GetDamageValue(int key)
    {
        return damageDic[key];
    }

    protected override void Initialize()
    {
        _canPlayNext    = true;
        // 注册动画播放完比消息
        //RegesterHandle(FSMMesgType.AnimationEnd, OnAnimationEnd);

        BossInitialize();
    }
    
    /// <summary>
    /// 销毁
    /// </summary>
    protected override void FSMOnDestroy()
    {
        // 注销消息
        //RemoveHandle(FSMMesgType.AnimationEnd);
        BossOnDesroy();
    }

    /// <summary>
    /// 更新
    /// </summary>
    protected override void FSMUpdate()
    {
        //SkillsPO[] array = new SkillsPO[_coolDic.Count];
        //_coolDic.Keys.CopyTo(array, 0);
        //for (int i = 0; i < array.Length; ++i)
        //{
        //    if (_coolDic[array[i]] > 0)
        //    {
        //        _coolDic[array[i]] -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        // 重新冷却
        //        _coolDic[array[i]] = 99;
        //        // 可用一个字典存储所有技能对应的冷却时间
        //        //coolDic[array[i]] = MonsterPO.CoolTime;
        //        _skillQueue.Enqueue(array[i]);
        //    }
        //}

        //// 上个技能播放完毕, 如技能队列里还有技能，等待2秒再播放
        //if (_animationEnd)
        //{
        //    if (_interval > 0)
        //    {
        //        _interval -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        _canPlayNext    = true;
        //        _animationEnd   = false;
        //    }
        //}

        //// 播放下个技能
        //if (_canPlayNext && _skillQueue.Count > 0)
        //{
        //    _skillPO      = _skillQueue.Dequeue();
        //}

        UpdatePreFrame();
    }

    /// <summary>
    /// 固定帧更新
    /// </summary>
    protected override void FSMFixedUpdate()
    {

        UpdateFixedFrame();
    }

    protected virtual void BossInitialize() { }
    protected virtual void UpdatePreFrame() { }
    protected virtual void UpdateFixedFrame() { }
    protected virtual void BossOnDesroy() { }

    /// <summary>
    /// 动画播放完毕
    /// </summary>
    private void OnAnimationEnd()
    {
        _interval       = 2;
        //_animationEnd   = true;

    }

    public virtual void AddHittingPart(HittingPart hp)
    {

    }
}
