/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ICharacter.cs
 * 
 * 简    介:    角色基类
 * 
 * 创建标识：   Pancake 2018/3/2 11:05:32
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ICharacter
{
    // Character的唯一标识
    protected int mGUID;
    // 属性
    protected ICharacterAttr mAttr;
    // 控制的游戏中显示的对象
    protected GameObject mGameObject;
    protected Animator mAnim;
    protected Rigidbody mRigidbody;
    protected NavMeshAgent mNMA;
    protected Collider mCollider;
    protected Renderer[] mRenderers;
    protected Transform mAttackPoint;
    protected Vector3 mBornPosition;
    protected LayerMask MASK_DEFAULT;
    #region 刷新点特定属性
    // 刷新区域
    protected string mAppearArea;
    // 停留区域
    protected string mStayArea;
    // 速度因子
    protected float mFactorSpeed;   
    // 被控制对象的行为类型
    protected E_ActionType mActionType;
    #endregion
    // 击杀该对象的玩家
    protected Player mPlayerKill;
    // 是否被击杀
    protected bool mIsKilled = false;
    // 是否可以销毁对象
    protected bool mCanDestroy = false;
    // 攻击范围
    protected float mAttackRange;
    // 攻击对象
    protected Transform mTargetTran;
    // 攻击对象所在区域
    protected int mLocation;
    // 站停逻辑处理，并保持当前动作
    protected bool mIsPause;
    protected float mPauseTime = 1;
    protected float mPauseTimer;

    // 攻击目标
    protected ICharacter mTargetCharacter;
    // 当前攻击自身的敌人列表
    protected List<ICharacter> mEnemyList;

    // 出生无敌时间
    private float mProtectedTime = 0.5f;
    // 无敌状态不可被攻击
    protected bool mIsInvincible = true;

    // 存在时间
    private float mDisappearTimer;

    // 绑定特列表
    protected List<GameObject> mEffectList = new List<GameObject>();

    public Vector3 position
    {
        get
        {
            if (mGameObject == null)
            {
                Debug.LogError("mGameObject为空");return Vector3.zero;
            }
            return mGameObject.transform.position;
        }
    }
    public int guid { get { return mGUID; } }
    public ICharacterAttr attr { get { return mAttr; } set { mAttr = value; } }
    public bool canDestroy { get { return mCanDestroy; } }
    public bool isKilled { get { return mIsKilled; } }
    public E_ActionType actionType { get { return mActionType; } }
    public Transform targetTran { get { return mTargetTran; } set { mTargetTran = value; } }
    public int location { get { return mLocation; } set { mLocation = value; } }
    public float attackRange { get { return mAttackRange; } set { mAttackRange = value; } }
    public Vector3 bornPosition { get { return mBornPosition; }set { mBornPosition = value; } }
    public ICharacter taretCharacter { get { return mTargetCharacter; } }
    public string appearArea { get { return mAppearArea; } set{ mAppearArea = value; } }
    public string stayArea { get { return mStayArea; } set { mStayArea = value; } }
    public Transform attackPoint { get { return mAttackPoint; } }
    public float factorSpeed { get { return mFactorSpeed; } set { mFactorSpeed = value; } }
    public Rigidbody rigidbody { get { return mRigidbody; } }
    public bool IsInvincible { get { return mIsInvincible; } }
    public bool isOnNavMesh { get { return mNMA.isOnNavMesh; } }
    public void CanWalk(bool value) { mNMA.enabled = value; }

    /// <summary>
    /// 销毁(只供给CharacterSystem调用)
    /// </summary>
    public void Destroy() { mCanDestroy = true; }
    public GameObject gameObject
    {
        set
        {
            mGameObject = value;            
            Init();
        }
        get
        {
            return mGameObject;
        }
    }

    protected void SetBodyActive()
    {
        for (int index = 0; index < mRenderers.Length; ++index)
        {
            mRenderers[index].material.SetFloat("_Cutoff", 0.00001f);
        }
    }

    public void BodyDisappear(float value)
    {
        for (int index = 0; index < mRenderers.Length; ++index)
        {
            mRenderers[index].material.SetFloat("_Cutoff", value);
        }
    }

    /// <summary>
    /// 初始化刷新点Character拥有的属性
    /// </summary>
    /// <param name="type"></param>
    /// <param name="appearArea"></param>
    /// <param name="factorSpeed"></param>
    /// <param name="stayArea"></param>
    public void InitRefreshData(E_ActionType type, string appearArea, float factorSpeed,float disappearTime, string stayArea = null)
    {
        mActionType = type;
        mAppearArea = appearArea;
        mFactorSpeed = factorSpeed;
        mDisappearTimer = disappearTime == -1 ? mDisappearTimer : disappearTime;
        mStayArea = stayArea;
        InitActionType(type); 
    }

    // 无敌状态
    public void EnterInvincible() { mIsInvincible = true; }
    public void DisInvincible() { mIsInvincible = false; }

    public void Update()
    {
        if (mIsPause && attr.currentHP > 0)
        {
            mPauseTimer += Time.deltaTime;
            mIsPause = mPauseTimer >= mPauseTime ? false : true;
            mPauseTimer = mIsPause ? mPauseTimer : 0;
            mAnim.speed = mIsPause ? mAnim.speed : 1;
            return;
        }

        if (mIsKilled)
        {
            mCanDestroy = true;
            return;
        }

        UpdateExtra();
    }

    public int EnemyCount
    {
        get
        {
            if (mEnemyList == null) return 0;
            else return mEnemyList.Count;
        }
    }

    // 添加攻击者
    public void AddEnemy(ICharacter enemy)
    {
        mEnemyList = mEnemyList == null ? new List<ICharacter>() : mEnemyList;
        mEnemyList.Add(enemy);
    }
    // 移除攻击者
    public void RemvoeEnemy(ICharacter enemy)
    {
        if (!mEnemyList.Contains(enemy)) return;
        mEnemyList.Remove(enemy);
    }

    public void RemoveSelfFromTarget()
    {
        if (mTargetCharacter == null) return;
        mTargetCharacter.RemvoeEnemy(this);
    }

   
    /// <summary>
    /// 初始化行为状态
    /// </summary>
    /// <param name="actionType"></param>
    public virtual void InitActionType(E_ActionType actionType) { }

    /// <summary>
    /// 更新状态机
    /// </summary>
    /// <param name="actionType"></param>
    public abstract void UpdateFSMAI(E_ActionType actionType);

    protected virtual void UpdateExtra()
    {
        if (mDisappearTimer <= 0) return;
        mDisappearTimer -= Time.deltaTime;
        if (mDisappearTimer <= 0)
        {
            Killed();
        }
    }

    /// <summary>
    /// 播放并判断指定动画是否播放完毕
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool AnimIsOver(string name, int id)
    {
        AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName(name))
            PlayAnim("State", id);
        else
        {
            if (info.normalizedTime >= 0.9f)
                return true;
        }
        return false;
    }

    public bool AnimIsOver(string name)
    {
        AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(name) && info.normalizedTime >= 0.9f)
            return true;
        return false;
    }

    public float AnimNormalizedTime(string name)
    {
        AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName(name))
            return -1;
        return info.normalizedTime;
    }

    /// <summary>
    /// 攻击效果
    /// </summary>
    public void Hurt()
    {
        if (Util.Random()) return;

        ioo.cameraManager.NormalShake();
        mGameObject.AddScreenCrash();
        int[] args = new int[] { -1, attr.baseAttr.id, attr.baseAttr.damageValue };
        ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);
    }
    /// <summary>
    /// 自爆
    /// </summary>
    public void Explode(bool forceDamage = false)
    {
        mIsKilled = true;
        DoPlayExplodeEffectSound();

        if(forceDamage == false)
        {
            // 50%概率Miss
            if (Util.Random()) return;

            // 离攻击点过远Miss
            float distance = Vector3.Distance(position, targetTran.position);
            if (distance > Define.PATH_STEP * 10) return;
        }

        ioo.cameraManager.NormalShake();
        mGameObject.AddScreenCrash();
        int[] args = new int[] { (int)mLocation, attr.baseAttr.id, attr.baseAttr.damageValue };
        ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);
    }

    protected virtual void Init()
    {
        mGUID = UtilCommon.GenGUID();

        MASK_DEFAULT = 1 << LayerMask.NameToLayer(SceneLayerMask.Terrain);

        mNMA = mGameObject.GetComponent<NavMeshAgent>();

        GameObject root = GameObject.Find("SimplePool/" + mGameObject.name + "/Root");
        if (root != null)
        {
            mAnim = mGameObject.transform.Find("Root").GetComponent<Animator>();
            AnimSpeed(1);
        }
        GameObject ap = GameObject.Find("SimplePool/" + mGameObject.name + "/Root/AttackPoint");
        if (ap != null)
            mAttackPoint = ap.transform;
        mRenderers = mGameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        mRigidbody = mGameObject.GetComponent<Rigidbody>();

        SetBodyActive();

        mDisappearTimer = attr.baseAttr.disappearTime;
        InitCollider();
        BindEffect();
        CoroutineController.Instance.StartCoroutine(DisProtected(mProtectedTime));
    }
    /// <summary>
    /// 初始化碰撞器
    /// </summary>
    /// 
    protected virtual void InitCollider() { }
    /// <summary>
    /// 绑定特效
    /// </summary>
    protected virtual void BindEffect()
    {
        Transform bornEffectPoint = Util.FindTransformByName(mGameObject.transform, "BornEffectPoint");
        if (bornEffectPoint == null) return;
        string bornEffectName = attr.baseAttr.bornEffectName;
        if (string.IsNullOrEmpty(bornEffectName)) return;
        GameObject effect = PoolManager.Instance.Spawn(bornEffectName);
        if (effect == null) return;
        effect.transform.SetParent(bornEffectPoint);
        effect.transform.localScale = Vector3.one;
        effect.transform.localPosition = Vector3.zero;

        mEffectList.Add(effect);
    }
    /// <summary>
    ///  被攻击
    /// </summary>
    /// <param name="player"></param>
    public virtual void UnderAttack(Player player)
    {
        mAttr.TakeDamage(player.attackValue);
    }
    /// <summary>
    ///  被击杀
    /// </summary>
    public virtual void Killed()
    {
        mIsKilled = true;
        RemoveSelfFromTarget();
    }
 
    /// <summary>
    ///  回收对象
    /// </summary>
    public virtual void Release()
    {
        foreach(GameObject effect in mEffectList)
        {
            PoolManager.Instance.DeSpawn(effect);
        }
        PoolManager.Instance.DeSpawn(mGameObject);
    }
    #region Animator
    public void PlayAnim(string animName, int id)
    {
        AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName(animName))
            mAnim.SetInteger("State", id);
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        if (!CanPause()) return;

        mIsPause = true;
        AnimSpeed(0);
    }
    /// <summary>
    /// 激活
    /// </summary>
    public void Activate()
    {
        mIsPause = false;
        AnimSpeed(1);
    }
    public void AnimSpeed(float value)
    {
        if (mAnim == null) return;
        mAnim.speed = value;
    }

    #endregion

    #region Move
    /// <summary>
    /// 向攻击目标移动
    /// </summary>
    /// <param name="transform"></param>
    public bool MoveTo(Transform transform, float checkRadius = 0.1f)
    {
        return MoveTo(transform.position, checkRadius);
    }

    public bool MoveTo(Vector3 targetPos, float checkRadius = 0.1f)
    {
        float speed = attr.baseAttr.baseSpeed * factorSpeed;
        Vector3 direction = targetPos - position;
        Vector3 step = direction.normalized * Time.deltaTime * speed;
        if (direction.magnitude < step.magnitude)
            mGameObject.transform.position = Vector3.Lerp(mGameObject.transform.position, targetPos, Time.deltaTime * speed);
        else
            mGameObject.transform.position += step;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        mGameObject.transform.rotation = Quaternion.Lerp(mGameObject.transform.rotation, toRotation, Time.deltaTime * attr.baseAttr.baseRotationSpeed);

        PlayAnim("run", 1);
        if (direction.magnitude < checkRadius) return true;
        else return false;
    }

    public bool MoveStraight(Vector3 targetPos, float checkRadius = 0.01f)
    {
        targetPos.y = mGameObject.transform.position.y;
        float speed = attr.baseAttr.baseSpeed * factorSpeed;
        Vector3 direction = targetPos - position;
        Vector3 step = direction.normalized * Time.deltaTime * speed;
        if (direction.magnitude < step.magnitude)
            mGameObject.transform.position = Vector3.Lerp(mGameObject.transform.position, targetPos, Time.deltaTime * speed);
        else
            mGameObject.transform.position += step;

        PlayAnim("run", 1);
        if (direction.magnitude < checkRadius) return true;
        else return false;
    }


    public void MoveToTarget(Vector3 pos, out Vector3 destination)
    {
        destination = Vector3.zero;
        Ray ray = new Ray(pos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MASK_DEFAULT))
        {
            if (hit.collider != null)
            {
                destination = hit.point;
                mNMA.SetDestination(destination);
            }
        }
    }

    public Vector3 GetTerrainPos(Vector3 pos)
    {
        Ray ray = new Ray(pos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MASK_DEFAULT))
        {
            if (hit.collider != null)
            {
                return hit.point;
            }
        }

        return Vector3.zero;
    }

    public virtual void LookAtCamera()
    {
        Quaternion toRotation = Quaternion.LookRotation(ioo.cameraManager.cTransform.position - position);
        mGameObject.transform.rotation = Quaternion.Lerp(mGameObject.transform.rotation, toRotation, Time.deltaTime * attr.baseAttr.baseRotationSpeed);
    }

    /// <summary>
    /// 依据路径移动
    /// </summary>
    public void MoveByPath(List<Vector3> path, ref int index)
    {
        Vector3 direction = path[index] - position;
        Vector3 step = direction.normalized * Time.deltaTime * attr.baseAttr.baseSpeed;
        if (direction.magnitude < 0.1f || direction.magnitude < step.magnitude)
            ++index;
        else
        {
            mGameObject.transform.position += step;
            float distance = Vector3.Distance(mGameObject.transform.position, path[path.Count - 1]);
            Quaternion toRotation = Quaternion.identity;
            if (distance > 20.0f * Define.PATH_STEP)
                toRotation = Quaternion.LookRotation(direction);
            else
                toRotation = Quaternion.LookRotation(ioo.cameraManager.position - position);
            mGameObject.transform.rotation = Quaternion.Lerp(mGameObject.transform.rotation, toRotation, Time.deltaTime * attr.baseAttr.baseRotationSpeed);
        }

        if (index == path.Count - 1)
            mGameObject.transform.position = Vector3.Lerp(mGameObject.transform.position, path[index], Time.deltaTime * attr.baseAttr.baseSpeed);

        index = index >= path.Count ? path.Count - 1 : index;
    }

    #endregion

    #region Auto Use

    /// <summary>
    /// 播放被攻击时的特效和音效
    /// </summary>
    protected void DoPlayBeAttackedEffect()
    {
        string name = attr.baseAttr.beAttackedEffectName;
        if (string.IsNullOrEmpty(name)) return;
        ioo.audioManager.PlaySound2D(name);
    }

    /// <summary>
    /// 播放被摧毁特效和音效
    /// </summary>
    protected void DoPlayBeDestroyEffectSound()
    {
        string name = attr.baseAttr.beDestroyEffectSoundName;
        if (string.IsNullOrEmpty(name)) return;
        ioo.audioManager.PlaySound2D(name);
    }

    /// <summary>
    /// 播放被摧毁角色语音
    /// </summary>
    protected void DoPlayBeDestroySound()
    {
        string[] names = attr.baseAttr.beDestroySoundName;
        if (names[0] == "NULL") return;
        int index = UnityEngine.Random.Range(0, names.Length);
        string name = names[index];
        ioo.audioManager.PlayPersonSound(name);
    }

    /// <summary>
    /// 播放自爆音效
    /// </summary>
    protected void DoPlayExplodeEffectSound()
    {
        string name = attr.baseAttr.explodeEffectSoundName;
        if (string.IsNullOrEmpty(name)) return;
        ioo.audioManager.PlaySound2D(name);
    }

    #endregion

    #region Private Function
    /// <summary>
    /// 被射击后，是否需要暂停动画播放
    /// </summary>
    /// <returns></returns>
    private bool CanPause()
    {
        switch (attr.baseAttr.characterType)
        {
            case E_CharacterType.Coin:
            case E_CharacterType.Box:
            case E_CharacterType.SandBox:
                return false;
        }
        return true;
    }
    /// <summary>
    /// 解除受保护状态
    /// </summary>
    IEnumerator DisProtected(float time)
    {
        yield return new WaitForSeconds(time);
        DisInvincible();
    }
    #endregion
}