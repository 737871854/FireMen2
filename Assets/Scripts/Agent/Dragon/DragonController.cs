/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonController.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:48:23
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : AdvanceFSM
{
    /// <summary>
    /// 状态转换条件
    /// </summary>
    public enum Transition
    {
        Idle = 0,
        SawPlayer =1,
        Hold = 2,
        Attack = 3,
        Success = 4,
        Fail = 5,
        Disappear = 6,
        Explode = 7,
    }

    /// <summary>
    /// 状态对应的行为
    /// </summary>
    public enum FSMActionID
    {
        Idle = 0,
        Chase =1,
        Hold = 2,
        Attack = 3,
        Retreat = 4,
        Beat = 5,
        Disappear = 6,
        Explode = 7,
    }

    private int _row;
    public int Row { get { return _row; } }
    public void SetRow(int value)
    {
        _row = value;
    }

    public bool StateChange { get; set; }
    private List<Vector3> pathList = new List<Vector3>();

    private bool _active;
    public bool Actived { get { return _active; } }

    public float stayTime;
    public void StayTimeUpdate()
    {
        stayTime -= Time.fixedDeltaTime;
        if (stayTime < 0)
            stayTime = 0;
    }

    // 延迟攻击
    private float _delayTimeAttack;
    public float DelayTimeAttack { get { return _delayTimeAttack; } }
    public void DelayTimeUpdate()
    {
        _delayTimeAttack -= Time.fixedDeltaTime;
        if (_delayTimeAttack < 0)
            _delayTimeAttack = 0;
    }

    public string stayArea;

    /// <summary>
    /// 初始化路径
    /// </summary>
    /// <param name="path"></param>
    public void InitPath(List<Vector3> path)
    {
        pathList = new List<Vector3>();
        for (int i = 0; i < path.Count; ++i)
            pathList.Add(path[i]);

        transform.position = pathList[0];
    }

    /// <summary>
    /// 
    /// </summary>
    public void ActiveShake()
    {
        _active = true;
    }

    /// <summary>
    /// 进入挟持手臂状态
    /// </summary>
    public void EnterHold()
    {
        _invincible = true;
        ioo.gameMode.RunState(E_GameState.Hold);
    }

    /// <summary>
    /// 被击退动画播放完毕
    /// </summary>
    public void RetreatEnd()
    {
        _health = 0;
    }

    protected override void FSMFixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        CurrentState.Reason(_playerTransfrom, transform);
        if (_isFreeze)
            return;
        CurrentState.Act(_playerTransfrom, transform);
    }

    protected override void FSMUpdate()
    {
        if (_isFreeze)
        {
            if (_freezeTime > 0)
            {
                _freezeTime -= Time.deltaTime;
            }
            else
            {
                _isFreeze = false;
                _freezeTime = 0;
                _animator.speed = 1;
            }
        }
    }

    protected override void Initialize()
    {
        
    }

    /// <summary>
    /// 响应冰冻道具
    /// </summary>
    /// <param name="time"></param>
    public void OnFreezeProp(float time)
    {
        _freezeTime = _freezeTime < time ? time : _freezeTime;
    }

    public override void OnSprayWaterHitting(Player player)
    {
        if (Invincible || _health == 0)
            return;

        _isFreeze = true;
        _animator.speed = 0;
        _freezeTime = Define.FREEZE_TIME;

        _health -= player.attackValue;
        if (_health <= 0)
        {
            // 销毁音效
            ioo.audioManager.PlaySound2D(DestroyEffectSound);
            // 死亡语音
            if (UnityEngine.Random.Range(0, 100) > 70)
            {
                int rand = UnityEngine.Random.Range(0, DestroySound.Length);
                ioo.audioManager.PlayPersonSound(DestroySound[rand]);
            }
            _health = 0;
            //EventDispatcher.TriggerEvent(EventDefine.Event_Add_Score, player, _worth);
            DeSpawn();
        }
    }

    public override void DeSpawn()
    {
        RemoveEvent();
        //ScenesManager.Instance.DesPawnDragon(this);
        //if(ActionType == global::E_ActionType.ShakeScreen)
        //    ScenesManager.Instance.ActiveDragonShakeScreen();
    }

    public override void InitPO(CharacterPO po0, CharacterRefreshPO po1)
    {
        _health = po0.Health;
        _worth = po0.Score;
        _baseSpeed = po0.BaseSpeed;
        _rotationSpeed = po0.RotationSpeed;
        _agentID = po0.Id;
        _agentType = (E_AgentType)po0.Type;
        _destroyEffectSound = po0.DestroyEffectSound;
        _explodeEffectSound = po0.ExplodeEffectSound;
        _destroySound = po0.DestroySound;
        _attackDamage = po0.DamageValue;
        _invincible = false;

        //_deadAfterAttack = po1.DadAfterDead == 1 ? true : false;
        //mMoveSpeed = _baseSpeed * UnityEngine.Random.Range(po1.FactorSpeed[0], po1.FactorSpeed[1]);

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody != null)
            _rigidbody.isKinematic = po1.IsKinematic == 1 ? true : false;

        //_reachTarget = false;

        _animator = GetComponent<Animator>();
        _animator.speed = 1;

        //// 绑定出生特效
        //GameObject bornPoint = transform.Find("Root/BornEffectPoint").gameObject;
        //_bornEffect = PoolManager.Instance.Spawn(po0.BornEffect);
        //_bornEffect.transform.SetParent(bornPoint.transform);
        //ResetTransform(_bornEffect);

        ActionType = (global::E_ActionType)po1.ActionType;
        if (ActionType == global::E_ActionType.AttackCitizen)
            _row = -1;

        if (ActionType == global::E_ActionType.SpecialCircle)
        {
            stayArea = po1.StayArea;
            stayTime = UnityEngine.Random.Range(po1.StayTime[0], po1.StayTime[1]);
            InsertPosToPath(0, 1);
        }

        if(ActionType == global::E_ActionType.ShakeScreen)
        {
            _active = false;
            stayArea = po1.StayArea;
            pathList.Clear();
            //pathList.AddRange(ioo.cameraManager.ShakeScreenList());
        }

        //_delayTimeAttack = po1.DelayTimeAttack;

        BindEvent();

        ConstructFSM(); ;
    }

  
    private void ConstructFSM()
    {
        Vector3[] wayPoints = null;
        if (pathList != null)
        {
            wayPoints = new Vector3[pathList.Count];
            pathList.CopyTo(wayPoints, 0);
        }

        DragonIdle idle = new DragonIdle(wayPoints,this);
        idle.AddTransition((int)Transition.SawPlayer, (int)FSMActionID.Chase);
        idle.AddTransition((int)Transition.Disappear, (int)FSMActionID.Disappear);

        DragonChase chase = new DragonChase(wayPoints, this);
        chase.AddTransition((int)Transition.Disappear, (int)FSMActionID.Disappear);
        chase.AddTransition((int)Transition.Hold, (int)FSMActionID.Hold);
        chase.AddTransition((int)Transition.Attack, (int)FSMActionID.Attack);

        DragonAttack attack = new DragonAttack(wayPoints, this);
        attack.AddTransition((int)Transition.Explode, (int)FSMActionID.Explode);
        attack.AddTransition((int)Transition.Disappear, (int)FSMActionID.Disappear);

        DragonDisappear disappear = new DragonDisappear(wayPoints, this);

        DragonHold hold = new DragonHold(wayPoints, this);
        hold.AddTransition((int)Transition.Success, (int)FSMActionID.Retreat);
        hold.AddTransition((int)Transition.Fail, (int)FSMActionID.Beat);

        DragonBeat beat = new DragonBeat(wayPoints, this);
        beat.AddTransition((int)Transition.Disappear, (int)FSMActionID.Disappear);

        DragonRetreat retreat = new DragonRetreat(wayPoints, this);
        retreat.AddTransition((int)Transition.Disappear, (int)FSMActionID.Disappear);

        DragonExplode explode = new DragonExplode(wayPoints, this);

        AddFSMState(idle);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(disappear);
        AddFSMState(hold);
        AddFSMState(beat);
        AddFSMState(retreat);
        AddFSMState(explode);

        StartFSM(chase);
    }

    private void InsertPosToPath(int start, int end)
    {
        Vector3 pos = Vector3.zero;
        AreaManager.Instance.GetRandomPositionInArea(stayArea, ref pos);
        pathList.Insert(end, pos);
    }

    private void OnHoldBreak(bool flag)
    {
        if (CurrentActionID != (int)FSMActionID.Hold)
            return;
        if (flag)
        {
            _health = 0;
            PerformTransition((int)Transition.Success);
            EventDispatcher.TriggerEvent(EventDefine.Event_Struggle_Hold_Success, true);
        }
        else
        {
            PerformTransition((int)Transition.Fail);
            EventDispatcher.TriggerEvent(EventDefine.Event_Struggle_Hold_Success, false);
            //EventDispatcher.TriggerEvent(EventDefine.Event_Player_Damage, AttackDamage);
        }
    }

    private void BindEvent()
    {
        if (ActionType == global::E_ActionType.ShakeScreen)
            EventDispatcher.AddEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnHoldBreak);
    }

    private void RemoveEvent()
    {
        //if (ActionType == global::E_ActionType.ShakeScreen)
            //EventDispatcher.RemoveEventListener<bool>(EventDefine.Boss_Skill_Use_End_Or_Break, OnHoldBreak);
    }
}