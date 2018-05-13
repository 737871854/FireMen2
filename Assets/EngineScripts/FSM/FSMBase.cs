/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FSMBase.cs
 * 
 * 简    介:    状态机管理类的基类
 * 
 * 创建标识：   Pancake 2017/4/3 16:18:26
 * 
 * 修改描述：
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;

public class FSMBase : MonoBehaviour
{
    /// <summary>
    /// 单位ID
    /// </summary>
    protected int _agentID;

    /// <summary>
    /// 血量
    /// </summary>
    protected float _health;

    /// <summary>
    /// 基础血量
    /// </summary>
    protected float _baseHealth;

    /// <summary>
    /// 无敌
    /// </summary>
    protected bool _invincible;

    /// <summary>
    /// 存在时间
    /// </summary>
    protected float _lifeTime;

    /// <summary>
    /// 价值（杀死该单位，收益）
    /// </summary>
    protected int _worth;

    /// <summary>
    /// 基础移动速度
    /// </summary>
    protected float _baseSpeed;

    /// <summary>
    /// 移动速度
    /// </summary>
    protected float mMoveSpeed;

    /// <summary>
    /// 旋转速度
    /// </summary>
    protected float _rotationSpeed;

    /// <summary>
    /// 普通攻击伤害
    /// </summary>
    protected int _attackDamage;

    /// <summary>
    /// 单位类型
    /// </summary>
    protected E_AgentType _agentType;

  
    /// <summary>
    /// 出生后，最大存在时间
    /// </summary>
    protected float _disappearTime;

    /// <summary>
    /// 消失类型
    /// </summary>
    protected E_DisappearType _disappearType;

    ///// <summary>
    ///// 技能组件
    ///// </summary>
    //protected SkillComponent _skillComponent;

    /// <summary>
    /// 技能冷却表
    /// </summary>
    //protected Dictionary<SkillsPO, float> _coolDic = new Dictionary<SkillsPO, float>();

    /// <summary>
    /// 可以播放技能
    /// </summary>
    protected bool _canPlayNext;

    /// <summary>
    /// 连续播放技能的时间间隔
    /// </summary>
    protected float _interval;

    /// <summary>
    /// 冻结时间
    /// </summary>
    protected float _freezeTime;

    /// <summary>
    /// 被冻结
    /// </summary>
    protected bool _isFreeze;

    /// <summary>
    /// 刚体组件
    /// </summary>
    protected Rigidbody _rigidbody;

    /// <summary>
    /// 释放技能队列
    /// </summary>
    //protected Queue<SkillsPO> _skillQueue = new Queue<SkillsPO>();

    /// <summary>
    /// 
    /// </summary>
    //protected SkillsPO _skillPO;
   
    /// <summary>
    /// 所有者
    /// </summary>
    protected FSMBase _parentFSM;

    /// <summary>
    /// 被消灭音效
    /// </summary>
    protected string _destroyEffectSound;
    /// <summary>
    /// 自行毁灭音效
    /// </summary>
    protected string _explodeEffectSound;

    /// <summary>
    /// 出生绑定特效
    /// </summary>
    protected GameObject _bornEffect;

    /// <summary>
    /// 被消灭语音
    /// </summary>
    protected string[] _destroySound;

    /// <summary>
    /// 移动或嘲讽全职
    /// </summary>
    protected float[] _moveOrRate;

    /// <summary>
    /// 攻击后死亡
    /// </summary>
    protected bool _deadAfterAttack;

    /// <summary>
    /// 掉落物品
    /// </summary>
    protected DropPropInfo _dropInfo;

    protected Animation _animation;
    protected Animator _animator;
    protected Renderer[] _renderer;

    public float Health                 { get { return _health; } }
    public float LiefeTime              { get { return _lifeTime; } }
    public E_AgentType AgentType          { get { return _agentType; } }
    public FSMBase ParentFSM            { get { return _parentFSM; } }
    //public Queue<SkillsPO> SkillQueue   { get { return _skillQueue; } }
    //public SkillsPO CurrSkillPO         { get { return _skillPO; }  }
    public bool CanPlayNext             { get { return _canPlayNext; } }
    public Animator Animator            { get { return _animator; } }
    public Animation Animation          { get { return _animation; } }
    public int Worth                    { get { return _worth; } }
    public float BaseSpeed              { get { return _baseSpeed; } }
    public float MoveSpeed              { get { return mMoveSpeed; } }
    public float RotationSpeed          { get { return _rotationSpeed; } }
    public int AgentID                  { get { return _agentID; }  }
    public float[] MoveOrRate           { get { return _moveOrRate; } }
    public string DestroyEffectSound    { get { return _destroyEffectSound; } }
    public string ExplodeEffectSound    { get { return _explodeEffectSound; } }
    public string[] DestroySound        { get { return _destroySound; }     set { _destroySound = value; } }
    public bool Invincible              { get { return _invincible; }       set { _invincible = value; } }
    public GameObject BornEffect        { get { return _bornEffect; }       set { _bornEffect = value; } }
    public int AttackDamage             { get { return _attackDamage; }     set { _attackDamage = value; } }
    public float DisaperaTime           { get { return _disappearTime; } }
    public Renderer[] Renderer          { get { return _renderer; } }
    public bool DeadAfterAttack         { get { return _deadAfterAttack; } }
    public E_ActionType ActionType        { get; set; }

    /// <summary>
    /// 玩家
    /// </summary>
    protected Transform _playerTransfrom;

    /// <summary>
    /// 巡逻点
    /// </summary>
    protected GameObject[] pointList;

    /// <summary>
    /// 目标点
    /// </summary>
    protected Vector3 destPos;

    protected float shootRate;

    /// <summary>
    /// 射击间隔时间
    /// </summary>
    protected float elapseTime;

    /// <summary>
    /// 初始化   
    /// </summary>
    protected virtual void Initialize() { }
    /// <summary>
    /// 初始化数值  MonsterPO 后面可改，将玩家属性也陪到这里面来，统一整合为CharacterPO
    /// </summary>
    public virtual void InitPO(CharacterPO po0, CharacterRefreshPO po1) { }
    /// <summary>
    /// 更新
    /// </summary>
    protected virtual void FSMUpdate() { }

    /// <summary>
    /// 固定更新
    /// </summary>
    protected virtual void FSMFixedUpdate() { }
    /// <summary>
    /// 销毁
    /// </summary>
    protected virtual void FSMOnDestroy() { }
    /// <summary>
    /// 响应射击
    /// </summary>
    public virtual void OnSprayWaterHitting(Player player) { }
    /// <summary>
    /// 回收
    /// </summary>
    public virtual void DeSpawn()
    {
        ActionType = global::E_ActionType.UnKonw;
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    protected virtual void FSMOnDisable() { }

    protected void ResetTransform(GameObject obj)
    {
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
    }

    protected void SetBodyActive()
    {
        for (int index = 0; index < _renderer.Length; ++index )
        {
            _renderer[index].material.SetFloat("_Cutoff", 0.0f);
        }
    }
    
    void OnDisable()
    {
        FSMOnDisable();
    }

    void Awake()
    {
        _renderer = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void Start()
    {
        //Initialize();
    }

    void Update()
    {
        //if (ioo.gameMode.IsPause)
        //    return;
        FSMUpdate();
    }

    void FixedUpdate()
    {
        //if (ioo.gameMode.IsPause)
        //    return;
        FSMFixedUpdate();
    }

    void OnDestroy()
    {
        FSMOnDestroy();
    }
}
