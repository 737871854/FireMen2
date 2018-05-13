/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   LifeBoatController.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/19 16:55:04
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;
using System.Collections.Generic;

public class LifeBoatController : AdvanceFSM
{
    /// <summary>
    /// 状态转换条件
    /// </summary>
    public enum Transition
    {
        Idle,
        Drop,
        Wait,
        Disappear,
    }

    /// <summary>
    /// 状态对应的行为
    /// </summary>
    public enum FSMActionID
    {
        Idle,
        Drop,
        Wait,
        Disappear,
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(_playerTransfrom, transform);
        CurrentState.Act(_playerTransfrom, transform);
    }

    protected override void FSMUpdate()
    {

    }

    public override void DeSpawn()
    {
        //ScenesManager.Instance.DesPawnLifeBoat(this);
    }

    public override void OnSprayWaterHitting(Player player)
    {
        if (_health == 0)
            return;

        _health -= player.attackValue;
        if (_health < 0)
            _health = 0;
        if (_health == 0)
        {
            _rigidbody.useGravity = true;
            PerformTransition((int)Transition.Drop);
        }
    }

    private int targetAgentID;
    public int TargetAgentID { get { return targetAgentID; } }

    public override void InitPO(CharacterPO po0, CharacterRefreshPO po1)
    {
        _agentID = po0.Id;
        _health = po0.Health;
        _worth = po0.Score;
        _baseSpeed = po0.BaseSpeed;
        _rotationSpeed = po0.RotationSpeed;
        _agentType = (E_AgentType)po0.Type;
        _destroyEffectSound = po0.DestroyEffectSound;
        _explodeEffectSound = po0.ExplodeEffectSound;
        _destroySound = po0.DestroySound;
        _attackDamage = po0.DamageValue;
        _worth = po0.Score;

        targetAgentID = po1.TargetAgentID;
        ActionType = (global::E_ActionType)po1.ActionType;
        //mMoveSpeed = _baseSpeed * Random.Range(po1.FactorSpeed[0], po1.FactorSpeed[1]);
        _disappearTime = po1.DisappearTime;
        _disappearType = _disappearTime == 0 ? E_DisappearType.Normal : E_DisappearType.CanDisappear;

        _rigidbody = GetComponent<Rigidbody>();

        SetBodyActive();

        ConstructFSM();
    }

    void ConstructFSM()
    {
        LifeBoatIdle idle = new LifeBoatIdle(this);
        idle.AddTransition((int)Transition.Drop, (int)FSMActionID.Drop);

        LifeBoatDrop drop = new LifeBoatDrop(this);
        drop.AddTransition((int)Transition.Wait, (int)FSMActionID.Wait);

        LifeBoatWait wait = new LifeBoatWait(this);
        wait.AddTransition((int)Transition.Disappear, (int)FSMActionID.Disappear);

        LifeBoatDisappear disappear = new LifeBoatDisappear(this);

        AddFSMState(idle);
        AddFSMState(drop);
        AddFSMState(wait);
        AddFSMState(disappear);

        StartFSM(idle);
    }

}