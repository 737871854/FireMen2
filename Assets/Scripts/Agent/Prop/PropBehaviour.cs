/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PropBehaviour.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/9/29 11:48:42
 * 
 * 修改描述：
 * 
 */

using UnityEngine;
using System.Collections;

public class PropBehaviour : FSMBase
{
    // 攻击时间
    private float _attackTime;
    // 喷雾时间
    private float mFogTime;
    // 有效使用时间
    private float _validTime;

    #region Unity Call Back
    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    private float _speed_Y;
    private Vector3 direction_xy;
    protected override void FSMUpdate()
    {
        if (_rigidbody != null)
            _rigidbody.velocity = Vector3.zero;
        if (_disappearType == E_DisappearType.CanDisappear)
        {
            if (_disappearTime > 0)
                _disappearTime -= Time.deltaTime;
            //else
            //    ScenesManager.Instance.DespawnProp(this);
        }

        if (_agentType == global::E_AgentType.Coin)
        {
            _speed_Y = _speed_Y - 9.8f * Time.deltaTime;
            _speed_Y = _speed_Y < -1 ? -1 : _speed_Y;
            transform.position += (ioo.cameraManager.parcentRight * direction_xy.x + _speed_Y * Vector3.up) * Time.deltaTime;
        }

        switch (AgentType)
        {
            case global::E_AgentType.SuperWater:
            case global::E_AgentType.Support:
            case global::E_AgentType.Freeze:
                transform.localEulerAngles += Vector3.up * Time.deltaTime * 20;
                break;
        }
    }
    #endregion
   
    #region Public Function
    /// <summary>
    /// 道具初始化
    /// </summary>
    /// <param name="po"></param>
    public override void InitPO(CharacterPO po0, CharacterRefreshPO po1)
    {
        _health     = po0.Health;
        _worth      = po0.Score;
        _baseSpeed  = po0.BaseSpeed;
        _attackTime = po0.AttackTime;
        mFogTime    = po0.FogTime;
        _freezeTime = po0.FreezeTime;
        _validTime  = po0.ValidTime;
        _agentType  = (global::E_AgentType)po0.Type;
        _agentID    = po0.Id;

        if (po1 != null)
        {
            _disappearTime  = po1.DisappearTime;
        }
        else
        {
            _disappearTime = po0.DisappearTime;
        }

        _disappearType = _disappearTime == 0 ? E_DisappearType.Normal : E_DisappearType.CanDisappear;

        if (IsCoin())
        {
            InitCoin(_agentID);
        }

        if (IsSandBox())
        {
            InitSandBox();
        }

        _rigidbody = GetComponent<Rigidbody>();

        AddBornEffect(po0.BornEffect);
    }
   
    ///// <summary>
    ///// 响应射击
    ///// </summary>
    ///// <param name="player"></param>
    public override void OnSprayWaterHitting(Player player)
    {
        if (Invincible)
            return;

        _health -= player.attackValue;
        if (_health <= 0)
        {
            _health = 0;
            DeSpawn();
            //EventDispatcher.TriggerEvent(EventDefine.Event_Add_Score, player, _worth);
            //EventDispatcher.TriggerEvent(EventDefine.Event_Agent_Death, _agentID);
            
            if (IsSandBox())
                EventDispatcher.RemoveEventListener(EventDefine.Event_SandBox_Can_Be_Spray, OnSandBoxCanUse);
        }

        switch (_agentType)
        {
            case global::E_AgentType.SuperWater:
                player.OnSupperWater(_attackTime);
                break;
            case global::E_AgentType.Freeze:
                EventDispatcher.TriggerEvent(EventDefine.Event_Freeze_Prop, mFogTime, _freezeTime);
                break;
            case global::E_AgentType.Support:
                EventDispatcher.TriggerEvent(EventDefine.Event_Get_Support_Prop, _validTime);
                break;
        }
    }

    /// <summary>
    /// 回收
    /// </summary>
    public override void DeSpawn()
    {
        // TODO：播放特效 播放音效
        RemoveBornEffect();
        //ScenesManager.Instance.DespawnProp(this);
    }
    #endregion

    #region Private Function
    /// <summary>
    /// 初始化金币
    /// </summary>
    /// <param name="id"></param>
    private void InitCoin(int id)
    {
        direction_xy = Random.insideUnitCircle;
        direction_xy = new Vector3(direction_xy.x, direction_xy.y, 0);
        direction_xy = direction_xy.normalized;
        _speed_Y     = direction_xy.y;

        //EventDispatcher.TriggerEvent(EventDefine.Event_Agent_Create, id);
    }

    /// <summary>
    /// 初始化沙箱
    /// </summary>
    private void InitSandBox()
    {
        _invincible = true;

        EventDispatcher.AddEventListener(EventDefine.Event_SandBox_Can_Be_Spray, OnSandBoxCanUse);
    }

    /// <summary>
    /// 绑定出生特效
    /// </summary>
    /// <param name="effect"></param>
    private void AddBornEffect(string effect)
    {
        GameObject bornPoint = transform.Find("BornEffectPoint").gameObject;
        if (effect == "")
            return;
        _bornEffect = PoolManager.Instance.Spawn(effect);
        _bornEffect.transform.SetParent(bornPoint.transform);
        ResetTransform(_bornEffect);
    }

    /// <summary>
    /// 移除出生特效
    /// </summary>
    private void RemoveBornEffect()
    {
        if (_bornEffect != null)
            PoolManager.Instance.DeSpawn(_bornEffect);
    }

    private void OnSandBoxCanUse()
    {
        _invincible = false;
    }

    private bool IsCoin()
    {
        if (_agentType == E_AgentType.Coin)
            return true;
        return false;
    }

    private bool IsSandBox()
    {
        if (_agentType == global::E_AgentType.SandBox)
            return true;
        return false;
    }
    #endregion
}
