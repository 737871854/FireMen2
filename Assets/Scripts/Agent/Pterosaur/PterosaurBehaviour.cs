/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PterosaurBehaviour.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/1/6 8:56:23
 * 
 * 修改描述：   
 * 
 */


using System.Collections.Generic;
using UnityEngine;

public class PterosaurBehaviour : Boss
{
    // 阶段
    private Step _curStep;
    private E_PterosaurStep _step = E_PterosaurStep.UnKnow;
    private List<Step> _stepList = new List<Step>();
    public void AddStep(Step step)
    {
        _stepList.Add(step);
    }

    //  射击点
    private List<HittingPart> hpList = new List<HittingPart>();
    public List<HittingPart> HPList { get { return hpList; } }
    public override void AddHittingPart(HittingPart hp)
    {
        hpList.Add(hp);
    }
    public void ClearHitPoint()
    {
        hpIsActive = false;
        EventDispatcher.TriggerEvent(EventDefine.Event_DisActive_HitPoint);
        hpList.Clear();
    }

    #region Public Properties
    public Transform LookAtPoint { get; set; }
    #endregion

    #region Public Function
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="po0"></param>
    /// <param name="po1"></param>
    public override void InitPO(CharacterPO po0, CharacterRefreshPO po1)
    {
        _animator = transform.Find("Root").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        LookAtPoint = FindParentObject("LookAtPoint").transform;

        PterosaurStep1 step1 = new PterosaurStep1(this);
        PterosaurStep2 step2 = new PterosaurStep2(this);
        PterosaurStep3 step3 = new PterosaurStep3(this);
        PterosaurStep4 step4 = new PterosaurStep4(this);
        PterosaurStep5 step5 = new PterosaurStep5(this);
        PterosaurStep6 step6 = new PterosaurStep6(this);
        PterosaurStep7 step7 = new PterosaurStep7(this);
        PterosaurStep8 step8 = new PterosaurStep8(this);
        PterosaurStep9 step9 = new PterosaurStep9(this);
        PterosaurStep10 step10 = new PterosaurStep10(this);

        SetBodyActive();

        _baseHealth = po0.Health;
        _worth      = po0.Score;
        _baseSpeed = po0.BaseSpeed;
        _rotationSpeed = po0.RotationSpeed;
        _agentID    = po0.Id;
        _agentType  = (E_AgentType)po0.Type;
        _destroyEffectSound = po0.DestroyEffectSound;
        _explodeEffectSound = po0.ExplodeEffectSound;
        _destroySound   = po0.DestroySound;
        _attackDamage   = po0.DamageValue;
        _invincible     = false;
        _health         = _baseHealth;
        mMoveSpeed      = _baseSpeed;

        //#region Init Hit Point
        //int index = 0;
        //for (int i = 0; i < po0.HitBone.Length; ++i)
        //{
        //    string boneName = po0.HitBone[i];
        //    if (boneName.Equals("XXX"))
        //        ++index;
        //    GameObject go = FindParentObject(boneName);
        //    if (go != null)
        //    {
        //        HitPoint hitpoint = go.GetOrAddComponent<HitPoint>();
        //        switch (index)
        //        {
        //            case 0:
        //                hitpoint.SetParentType(HitPoint.E_Parent.Claw);
        //                break;
        //            case 1:
        //                hitpoint.SetParentType(HitPoint.E_Parent.Beat);
        //                break;
        //        }
        //    }
        //}
        //#endregion

        EventDispatcher.AddEventListener(EventDefine.Boss_Change_State, NextStep);
        EventDispatcher.AddEventListener<int, float>(EventDefine.Boss_Hit_Point_Break, AddDamageValue);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void DeSpawn()
    {
        //ScenesManager.Instance.DespawnBoss(this);
        ioo.TriggerListener(EventLuaDefine.Event_Boss_Dead);
        //EventDispatcher.TriggerEvent(EventDefine.Event_Agent_Death, AgentID);

        EventDispatcher.RemoveEventListener(EventDefine.Boss_Change_State, NextStep);
        EventDispatcher.RemoveEventListener<int, float>(EventDefine.Boss_Hit_Point_Break, AddDamageValue);
    }

    /// <summary>
    /// 响应射击
    /// </summary>
    /// <param name="player"></param>
    public override void OnSprayWaterHitting(Player player)
    {
        if (_invincible || _health <= 0)
            return;

        _health -= player.attackValue;
        AddDamageValue(player.id, player.attackValue);
        if (_health < 0)
        {
            _health = 0;
            DeSpawn();
        }
    }

    public void OnDamage(float damage = 5)
    {
        _health -= damage;
        if (_health < 0)
        {
            _health = 0;
            DeSpawn();
        }
    }

    /// <summary>
    /// 启动火雨技能
    /// </summary>
    public void ActiveRain()
    {
        if (hpIsActive)
            return;
        hpIsActive = true;
        //for (int i = 0; i < 6; ++i)
        //{
        //    GameObject ball = PoolManager.Instance.Spawn(PoolItemName.Ball);
        //    Quaternion toRotation = Quaternion.AngleAxis(i * 60, transform.forward);
        //    Vector3 offset = toRotation * transform.right;
        //    ball.transform.position = transform.position + offset * 3 + transform.up * 2;
        //    HitPoint hp = ball.GetComponent<HitPoint>();
        //    hp.SetParentType(HitPoint.E_Parent.FireBall);
        //}
        //EventDispatcher.TriggerEvent(EventDefine.Event_Active_Boss_Ball, 1.0f);
        //ioo.gameMode.AddHittingPart(hpList);
    }

    public void ActiveClaw()
    {
        if (hpIsActive)
            return;
        hpIsActive = true;
        //EventDispatcher.TriggerEvent(EventDefine.Event_Active_Boss_Body_HitPoint_Claw, 1.0f);
        //ioo.gameMode.AddHittingPart(hpList);
    }

    public void ActiveBall()
    {
        if (hpIsActive)
            return;
        hpIsActive = true;
        //EventDispatcher.TriggerEvent(EventDefine.Event_Active_Boss_Body_HitPoint_Ball, 1.0f);
        //ioo.gameMode.AddHittingPart(hpList);
    }

    public void ActiveBeat()
    {
        if (hpIsActive)
            return;
        hpIsActive = true;
        //EventDispatcher.TriggerEvent(EventDefine.Event_Active_Boss_Body_HitPoint_Beat, 1.0f);
        //ioo.gameMode.AddHittingPart(hpList);
    }

    public void ActiveCan(int step)
    {
        if (hpIsActive)
            return;
        hpIsActive = true;
        EventDispatcher.TriggerEvent(EventDefine.Event_Active_CanSkill, step);
        //ioo.gameMode.AddHittingPart(hpList);
    }

    /// <summary>
    /// 进入下个阶段
    /// </summary>
    public void NextStep()
    {
        switch (_step)
        {
            case E_PterosaurStep.UnKnow:
                _step = E_PterosaurStep.Step1;
                ChangeStep();
                //ioo.cameraManager.OceanSceneBossSpeed();
                ioo.TriggerListener(EventLuaDefine.Event_Boss_Born);
                break;
            case E_PterosaurStep.Step1:
                _step = E_PterosaurStep.Step2;
                ChangeStep();
                break;
            case E_PterosaurStep.Step2:
                _step = E_PterosaurStep.Step3;
                ChangeStep();
                break;
            case E_PterosaurStep.Step3:
                _step = E_PterosaurStep.Step4;
                ChangeStep();
                break;
            case E_PterosaurStep.Step4:
                _step = E_PterosaurStep.Step5;
                ChangeStep();
                break;
            case E_PterosaurStep.Step5:
                _step = E_PterosaurStep.Step6;
                ChangeStep();
                break;
        }
    }

    /// <summary>
    /// 进入无敌模式
    /// </summary>
    public void EnterInvincible(bool flag = true)
    {
        _invincible = flag;
    }

    #endregion

    #region Private Function
    protected override void UpdateFixedFrame()
    {

    }

    protected override void UpdatePreFrame()
    {
        float precent = _health / _baseHealth;
        ioo.gameMode.UpdateBossProgress(precent);

        if (_curStep == null)
            return;

        if (IsDead())
        {
            for (int index = 0; index < Renderer.Length; ++index)
            {
                float value = Renderer[index].material.GetFloat("_Cutoff");
                value += Time.fixedDeltaTime;
                value = value > 1 ? 1 : value;
                Renderer[index].material.SetFloat("_Cutoff", value);
                bool hasDisappear = value == 1 ? true : false;
                if (hasDisappear)
                    DeSpawn();
            }
        }
        else
            _curStep.UpdateStep();
    }

    private bool IsDead()
    {
        return _health == 0;
    }

    private void ChangeStep()
    {
        for (int i = 0; i < _stepList.Count; ++i)
        {
            if (_stepList[i].PterosaurStep == _step)
            {
                _curStep = _stepList[i];
                _curStep.RunStep();
                break;
            }
        }
    }

    private GameObject FindParentObject(string childName)
    {
        Transform father = null;
        foreach (Transform go in gameObject.transform.GetComponentsInChildren<Transform>())
        {
            if (go.name == childName)
            {
                father = go;
                return father.gameObject;
            }
        }
        return null;
    }
    #endregion
}