/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Bear.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 16:57:30
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : ICharacter
{
    public enum E_SkillType
    {
        Claw,
        Beat,
        FireBall,
        SpeedUp,
        Follow,
        ToHome,
    }
    public class SkillInfo
    {
        public SkillInfo(E_SkillType type, bool once)
        {
            skillType = type;
            isOnce = once;
        }
        public E_SkillType skillType;
        public bool isOnce;
    }
    public enum E_BossStep
    {
        Step1,
        Step2,
        Step3,
        Step4,
        Step5,
        Step6,
    }

    protected BearFSMSystem mFSMSystem;
    protected E_SkillType mUsedSkillType;
    protected Dictionary<int, int> mDamageDict = new Dictionary<int, int>();
    protected Dictionary<E_BossStep, List<SkillInfo>> mSkillDict = new Dictionary<E_BossStep, List<SkillInfo>>();
    protected E_BossStep mCurrentStep;

    private Vector3 mJumpPos;
    private float mJumpSpeed;
    private int mSkillIndex;

    public GameObject crashPoint { get; set; }
    public E_BossStep currentStep { get { return mCurrentStep; } }
    public Vector3 jumpPos { get { return mJumpPos; } }
    public float jumpSpeed { get { return mJumpSpeed; } }

    public Bear()
    {
        MakeFSM();
    }
    protected override void Init()
    {
        base.Init();
        EnterInvincible();
        InitSkill();
        InitHitPoint();
        crashPoint = Util.FindTransformByName(mGameObject.transform, "CrashPoint").gameObject;
        UseGravityAndNMA(false);
        mJumpPos = GameObject.Find("JumpPoint").transform.position;
        mJumpSpeed = (mJumpPos - position).magnitude;
    }

    private void InitSkill()
    {
        List<SkillInfo> step1 = new List<SkillInfo>();
        step1.Add(new SkillInfo(E_SkillType.Claw, true));
        mSkillDict.Add(E_BossStep.Step1, step1);
        List<SkillInfo> step2 = new List<SkillInfo>();
        step2.Add(new SkillInfo(E_SkillType.Beat, true));
        mSkillDict.Add(E_BossStep.Step2, step2);
        List<SkillInfo> step3 = new List<SkillInfo>();
        step3.Add(new SkillInfo(E_SkillType.SpeedUp, true));
        step3.Add(new SkillInfo(E_SkillType.Beat, true));
        step3.Add(new SkillInfo(E_SkillType.SpeedUp, true));
        step3.Add(new SkillInfo(E_SkillType.Beat, true));
        step3.Add(new SkillInfo(E_SkillType.SpeedUp, true));
        step3.Add(new SkillInfo(E_SkillType.Beat, true));
        step3.Add(new SkillInfo(E_SkillType.Follow, true));
        mSkillDict.Add(E_BossStep.Step3, step3);
        List<SkillInfo> step4 = new List<SkillInfo>();
        step4.Add(new SkillInfo(E_SkillType.Beat, true));
        step4.Add(new SkillInfo(E_SkillType.ToHome, true));
        step4.Add(new SkillInfo(E_SkillType.Claw, true));
        step4.Add(new SkillInfo(E_SkillType.ToHome, true));
        step4.Add(new SkillInfo(E_SkillType.FireBall, true));
        mSkillDict.Add(E_BossStep.Step4, step4);
        List<SkillInfo> step5 = new List<SkillInfo>();
        step5.Add(new SkillInfo(E_SkillType.Claw, true));
        step5.Add(new SkillInfo(E_SkillType.Beat, true));
        step5.Add(new SkillInfo(E_SkillType.Follow, true));
        mSkillDict.Add(E_BossStep.Step5, step5);
        List<SkillInfo> step6 = new List<SkillInfo>();
        step6.Add(new SkillInfo(E_SkillType.Beat, false));
        step6.Add(new SkillInfo(E_SkillType.ToHome, false));
        step6.Add(new SkillInfo(E_SkillType.Claw, false));
        step6.Add(new SkillInfo(E_SkillType.ToHome, false));
        step6.Add(new SkillInfo(E_SkillType.FireBall, false));
        mSkillDict.Add(E_BossStep.Step6, step6);
    }

    private void InitHitPoint()
    {
        string[] hitBone = attr.baseAttr.hitBone;
        int index = 0;
        for (int i = 0; i < hitBone.Length; ++i)
        {
            string boneName = hitBone[i];
            if (boneName.Equals("XXX"))
                ++index;
            Transform tran = Util.FindTransformByName(mGameObject.transform, boneName);
            if (tran != null)
            {
                switch (index)
                {
                    case 0:
                        tran.gameObject.AddComponent<BearClawHitPoint>();
                        break;
                    case 1:
                        tran.gameObject.AddComponent<BearBeatHitPoint>();
                        break;
                    case 2:
                        tran.gameObject.AddComponent<BearBallHitPoint>();
                        break;
                }
            }
        }
    }

    public bool CurrentStepSkillIsOut()
    {
        int skillCount = mSkillDict[mCurrentStep].Count;
        if (mSkillIndex < skillCount) return false;
        return true;
    }

    public void NexStep()
    {
        mSkillIndex = 0;
        switch (mCurrentStep)
        {
            case E_BossStep.Step1:
                mCurrentStep = E_BossStep.Step2;
                ioo.cameraManager.PlayCPA();
                break;
            case E_BossStep.Step2:
                mCurrentStep = E_BossStep.Step3;
                break;
            case E_BossStep.Step3:
                mCurrentStep = E_BossStep.Step4;
                break;
            case E_BossStep.Step4:
                mCurrentStep = E_BossStep.Step5;
                break;
            case E_BossStep.Step5:
                mCurrentStep = E_BossStep.Step6;
                break;
        }
    }

    public float CheckDistance()
    {
        float ret = 0;
        List<SkillInfo> list = mSkillDict[mCurrentStep];
        if (list.Count == 0) return ret;
        SkillInfo skill = list[mSkillIndex];
        switch (skill.skillType)
        {
            case E_SkillType.Beat:
                ret = 1.5f;
                break;
            case E_SkillType.SpeedUp:
                ret = 10.0f;
                break;
            case E_SkillType.Claw:
                ret = 1.2f;
                break;
            case E_SkillType.Follow:
                ret = 10.0f;
                break;
        }
        return ret;
    }

    public bool UseSkillImmediately()
    {
        List<SkillInfo> list = mSkillDict[mCurrentStep];
        SkillInfo skill = list[mSkillIndex];
        if (skill.skillType == E_SkillType.FireBall)
            return true;
        return false;
    }

    public void UseSkill()
    {
        List<SkillInfo> list = mSkillDict[mCurrentStep];
        mSkillIndex %= list.Count;
        SkillInfo skill = list[mSkillIndex];
        if (skill.isOnce)
        {
            list.RemoveAt(mSkillIndex);
        }
        else
        {
            ++mSkillIndex;
        }
        switch (skill.skillType)
        {
            case E_SkillType.Beat:
                mUsedSkillType = E_SkillType.Beat;
                mFSMSystem.PerformTransition(BearTransition.Beat);
                break;
            case E_SkillType.SpeedUp:
                mUsedSkillType = E_SkillType.SpeedUp;
                mFSMSystem.PerformTransition(BearTransition.SpeedUp);
                break;
            case E_SkillType.Claw:
                mUsedSkillType = E_SkillType.Claw;
                mFSMSystem.PerformTransition(BearTransition.Claw);
                break;
            case E_SkillType.FireBall:
                mUsedSkillType = E_SkillType.FireBall;
                mFSMSystem.PerformTransition(BearTransition.FireBall);
                break;
            case E_SkillType.Follow:
                mUsedSkillType = E_SkillType.Follow;
                mFSMSystem.PerformTransition(BearTransition.Follow);
                break;
            case E_SkillType.ToHome:
                mUsedSkillType = E_SkillType.ToHome;
                mFSMSystem.PerformTransition(BearTransition.ToHome);
                break;
        }
    }

    private void MakeFSM()
    {
        mFSMSystem = new BearFSMSystem();

        BearAppearState appearState = new BearAppearState(mFSMSystem, this);
        appearState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearIdleState idleState = new BearIdleState(mFSMSystem, this);
        idleState.AddTransition(BearTransition.Howl, BearStateID.Howl);
        idleState.AddTransition(BearTransition.Claw, BearStateID.Claw);
        idleState.AddTransition(BearTransition.Beat, BearStateID.Beat);
        idleState.AddTransition(BearTransition.FireBall, BearStateID.FireBall);
        idleState.AddTransition(BearTransition.SeaEnemy, BearStateID.Chase);
        idleState.AddTransition(BearTransition.NoHealth, BearStateID.Dead);
        idleState.AddTransition(BearTransition.Follow, BearStateID.Follow);
        idleState.AddTransition(BearTransition.ToHome, BearStateID.ToHome);

        BearChaseState chaseState = new BearChaseState(mFSMSystem, this);
        chaseState.AddTransition(BearTransition.Rest, BearStateID.Idle);
        chaseState.AddTransition(BearTransition.Claw, BearStateID.Claw);
        chaseState.AddTransition(BearTransition.Beat, BearStateID.Beat);
        chaseState.AddTransition(BearTransition.FireBall, BearStateID.FireBall);
        chaseState.AddTransition(BearTransition.SpeedUp, BearStateID.SpeedUp);
        chaseState.AddTransition(BearTransition.Follow, BearStateID.Follow);

        BearSpeedUpState speedUpState = new BearSpeedUpState(mFSMSystem, this);
        speedUpState.AddTransition(BearTransition.SeaEnemy, BearStateID.Chase);

        BearToHomeState toHomeState = new BearToHomeState(mFSMSystem, this);
        toHomeState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearFollowState followState = new BearFollowState(mFSMSystem, this);
        followState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearClawState clawState = new BearClawState(mFSMSystem, this);
        clawState.AddTransition(BearTransition.BreakClaw, BearStateID.BreakClaw);
        clawState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearBreakClawState breakClawState = new BearBreakClawState(mFSMSystem, this);
        breakClawState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearBeatState beatState = new BearBeatState(mFSMSystem, this);
        beatState.AddTransition(BearTransition.BreakBeat, BearStateID.BreakBeat);
        beatState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearBreakBeatState breakBeatState = new BearBreakBeatState(mFSMSystem, this);
        breakBeatState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearFireBallState fireBallState = new BearFireBallState(mFSMSystem, this);
        fireBallState.AddTransition(BearTransition.Rest, BearStateID.Idle);
        fireBallState.AddTransition(BearTransition.FireBallSuccess, BearStateID.FireBallSuccess);

        BearFireBallSuccessState fireBallSuccessState = new BearFireBallSuccessState(mFSMSystem, this);
        fireBallSuccessState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearHowlState howlState = new BearHowlState(mFSMSystem, this);
        howlState.AddTransition(BearTransition.Rest, BearStateID.Idle);

        BearDeadState deadState = new BearDeadState(mFSMSystem, this);
        deadState.AddTransition(BearTransition.Disappear, BearStateID.Disappear);

        BearDisappearState disappearState = new BearDisappearState(mFSMSystem, this);

        mFSMSystem.AddState(appearState, idleState, chaseState, speedUpState, clawState, toHomeState, followState, breakClawState, beatState, breakBeatState, fireBallState, fireBallSuccessState, howlState, deadState, disappearState);
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        if (mIsKilled) return;

        if (mFSMSystem.currentState.stateID != BearStateID.Appear)
        {
            if (ioo.gameMode.IsUsingHittingPartAllBreaked())
                DisInvincible();
            else
                EnterInvincible();
        }

        ioo.cameraManager.LookAt(crashPoint.transform);

        mFSMSystem.currentState.Act(actionType);
        mFSMSystem.currentState.Reason(actionType);
    }

    // 无敌状态
    public void OnSkillBreaked() { attr.TakeDamage(20); }
    public void UseGravityAndNMA(bool value) { mRigidbody.useGravity = value; mNMA.enabled = value; }
    public bool IsStep1() { return mCurrentStep == E_BossStep.Step1; }
    public bool IsStep3() { return mCurrentStep == E_BossStep.Step3; }
    public bool IsStep4() { return mCurrentStep == E_BossStep.Step4; }
    public bool IsStep6() { return mCurrentStep == E_BossStep.Step6; }
    public void NormalSpeed() { mNMA.speed = 2.5f; }
    public void SpeedUp() { mNMA.speed = 3.0f; }
    public void FollowSpeed() { mNMA.speed = 1.5f; }

    public void NMAMove()
    {
        Vector3 pos = Vector3.zero;
        List<SkillInfo> list = mSkillDict[mCurrentStep];
        int count = list.Count;
        switch(mCurrentStep)
        {
            case E_BossStep.Step1:
                break;
            case E_BossStep.Step2:
                pos = ioo.cameraManager.position - ioo.cameraManager.forward * 0.5f;
                break;
            case E_BossStep.Step3:
                if(count == 6 || count == 4 || count == 2)
                    pos = ioo.cameraManager.position - ioo.cameraManager.forward * 0.5f;
                else if(count == 0)
                    pos = ioo.cameraManager.position - ioo.cameraManager.forward * 2.0f;
                break;
            case E_BossStep.Step4:
            case E_BossStep.Step6:
                if (count == 5 || count == 0)
                    pos = ioo.cameraManager.position - ioo.cameraManager.forward * 0.5f;
                else if (count == 3)
                    pos = ioo.cameraManager.position - ioo.cameraManager.forward * 1.1f;
                break;
            case E_BossStep.Step5:
                    pos = ioo.cameraManager.position - ioo.cameraManager.forward * 0.5f;
                break;
        }

        Ray ray = new Ray(pos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MASK_DEFAULT))
        {
            if (hit.collider != null)
            {
                mNMA.SetDestination(hit.point);
            }
        }
    }

    private bool mIsDead;
    public bool isDead { get { return mIsDead; } }
    public override void UnderAttack(Player player)
    {
        if (mIsKilled || mIsInvincible || mIsDead) return;
        base.UnderAttack(player);

        int heath = mAttr.currentHP;
        int damage = player.attackValue;
        int hurt = heath < damage ? heath : damage;
        if (!mDamageDict.ContainsKey(player.id)) mDamageDict.Add(player.id, hurt);
        else mDamageDict[player.id] += hurt;

        DoPlayBeAttackedEffect();
        if (heath <= 0)
        {
            mIsDead = true;
            foreach (KeyValuePair<int, int> kv in mDamageDict)
            {
                int worth = (int)(1.0f * kv.Value / attr.baseAttr.maxHP) * attr.baseAttr.worth;
                int[] args = new int[] { kv.Key, attr.baseAttr.id, worth };
                ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
            }
        }
    }

    public override void Killed()
    {
        base.Killed();
        DoPlayBeDestroyEffectSound();
        DoPlayBeDestroySound();
    }

    public override void LookAtCamera()
    {
        Vector3 direction = ioo.cameraManager.cTransform.position - position;

        Quaternion toRotation = Quaternion.LookRotation(direction);
        mGameObject.transform.rotation = Quaternion.Lerp(mGameObject.transform.rotation, toRotation, Time.deltaTime * attr.baseAttr.baseRotationSpeed);
        mGameObject.transform.localEulerAngles = new Vector3(0, mGameObject.transform.localEulerAngles.y, 0);
    }

    public float RotationAngleToCamera()
    {
        Quaternion toRotation = Quaternion.LookRotation(ioo.cameraManager.cTransform.position - position);
        return Quaternion.Angle(toRotation, mGameObject.transform.rotation);
    }

}