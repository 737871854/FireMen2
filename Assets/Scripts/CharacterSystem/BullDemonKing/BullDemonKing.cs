/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKing.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 15:17:00
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing : ICharacter
{
    public enum E_SkillType
    {
        Desk,
        Sofa,
        FireFist,
        FireCricle,
        OXHorn,
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

    public enum E_BossLocation
    {
        Null,
        Middle,
        Left,
        Right,
        Forward
    }

    protected string[] mHitBone;

    protected E_BossLocation mCurrentLocation;
    protected E_BossLocation mLastLocation;
    public E_BossLocation currentLocation { get { return mCurrentLocation; } }
    public E_BossLocation lastLocation { get { return mLastLocation; } }

    private int mLocationIndex;
    protected List<E_BossLocation> mLocationList = new List<E_BossLocation>() { E_BossLocation.Middle, E_BossLocation.Left, E_BossLocation.Middle, E_BossLocation.Right };

    private E_SkillType mUsedSkillType;
    public E_SkillType usedSkillType { get { return mUsedSkillType; } }
    private int mSkillIndex;
    protected Dictionary<E_BossLocation, List<SkillInfo>> mSkillDict = new Dictionary<E_BossLocation, List<SkillInfo>>();

    protected BullDemonKingFSMSystem mFSMSystem;

    private Dictionary<int, int> mDamageDict = new Dictionary<int, int>();

    private Dictionary<string, Vector3> mPathDict = new Dictionary<string, Vector3>();

    public BullDemonKing()
    {
        MakeFSM();       
    }

    protected override void Init()
    {
        base.Init();
        EnterInvincible();
        InitPath();
        InitSkill();
        InitHitPoint();
        CrashPoint = Util.FindTransformByName(mGameObject.transform,"CrashPoint").gameObject;
    }

    private void InitPath()
    {
        GameObject way = GameObject.FindGameObjectWithTag(GameTage.WayPoints);
        if (way == null) { Debug.LogError("初始化路径失败！"); return; }
        Vector3 pos = way.transform.Find("middle").transform.position;
        mPathDict.Add("middle", pos);
        pos = way.transform.Find("left").transform.position;
        mPathDict.Add("left", pos);
        pos = way.transform.Find("right").transform.position;
        mPathDict.Add("right", pos);
        pos = way.transform.Find("forward").transform.position;
        mPathDict.Add("forward", pos);
    }

    private void InitSkill()
    {
        List<SkillInfo> middle = new List<SkillInfo>();
        middle.Add(new SkillInfo(E_SkillType.Desk, true));
        middle.Add(new SkillInfo(E_SkillType.FireFist, false));
        middle.Add(new SkillInfo(E_SkillType.OXHorn, false));
        middle.Add(new SkillInfo(E_SkillType.FireCricle, false));
        mSkillDict.Add(E_BossLocation.Middle, middle);
        List<SkillInfo> left = new List<SkillInfo>();
        left.Add(new SkillInfo(E_SkillType.Sofa, true));
        left.Add(new SkillInfo(E_SkillType.FireFist, false));
        left.Add(new SkillInfo(E_SkillType.OXHorn, false));
        mSkillDict.Add(E_BossLocation.Left, left);
        List<SkillInfo> right = new List<SkillInfo>();
        right.Add(new SkillInfo(E_SkillType.Sofa, true));
        right.Add(new SkillInfo(E_SkillType.FireFist, false));
        right.Add(new SkillInfo(E_SkillType.OXHorn, false));
        mSkillDict.Add(E_BossLocation.Right, right);
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
                        tran.gameObject.AddComponent<BullFireFistHitPoint>();
                        break;
                    case 1:
                        tran.gameObject.AddComponent<BullOXHornHitPoint>();
                        break;
                }
            }
        }


        //string[] hitBone = attr.baseAttr.hitBone;
        //int spit = 0;
        //for(int i = 0; i < hitBone.Length;++i)
        //{
        //    if (hitBone[i].Equals("XXX"))
        //        spit = i;
        //}

        //for(int i = 0; i < hitBone.Length;++i)
        //{
        //    if (i < spit)
        //    {
        //        string name = hitBone[i];
        //        Transform tran = Util.FindTransformByName(mGameObject.transform, name);
        //        if(tran != null)
        //            tran.gameObject.AddComponent<BullFireFistHitPoint>();

        //    }else if(i > spit)
        //    {
        //        string name = hitBone[i];
        //        Transform tran = Util.FindTransformByName(mGameObject.transform, name);
        //        if (tran != null)
        //            tran.gameObject.AddComponent<BullOXHornHitPoint>();
        //    }
        //}
    }

    public void UseSkill()
    {
        List<SkillInfo> list = mSkillDict[mCurrentLocation];
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
            case E_SkillType.Desk:
                mUsedSkillType = E_SkillType.Desk;
                mFSMSystem.PerformTransition(BullDemonKingTransition.Desk);
                break;
            case E_SkillType.Sofa:
                mUsedSkillType = E_SkillType.Sofa;
                mFSMSystem.PerformTransition(BullDemonKingTransition.Sofa);
                break;
            case E_SkillType.FireCricle:
                mUsedSkillType = E_SkillType.FireCricle;
                mFSMSystem.PerformTransition(BullDemonKingTransition.FireCricle);
                break;
            case E_SkillType.FireFist:
                mUsedSkillType = E_SkillType.FireFist;
                mFSMSystem.PerformTransition(BullDemonKingTransition.FireFist);
                break;
            case E_SkillType.OXHorn:
                mUsedSkillType = E_SkillType.OXHorn;
                mFSMSystem.PerformTransition(BullDemonKingTransition.OXHorn);
                break;
        }
    }

    public void DoLensOP()
    {
        if (mUsedSkillType != E_SkillType.FireFist && mUsedSkillType != E_SkillType.FireCricle) return;

        switch(mCurrentLocation)
        {
            case E_BossLocation.Middle:
                ioo.cameraManager.BullMiddleSkilltLens();
                break;
            case E_BossLocation.Left:
                ioo.cameraManager.BullLeftSkillLens();
                break;
            case E_BossLocation.Right:
                ioo.cameraManager.BullRightSkillLens();
                break;
        }
    }

    public void DoLensReserve()
    {
        ioo.cameraManager.BullReversLens();
    }

    public bool CurrentLocationSkillIsOut()
    {
        int skillCount = mSkillDict[mCurrentLocation].Count;
        if (mSkillIndex < skillCount) return false;
        return true;
    }

    public void NextLocation()
    {
        mSkillIndex = 0;
        ++mLocationIndex;
        mLocationIndex %= mLocationList.Count;
        mCurrentLocation = mLocationList[mLocationIndex];
    }

    public string[] hitBone { set { mHitBone = value; } }
    public Vector3 MiddlePos() { return mPathDict["middle"]; }
    public Vector3 LeftPos() { return mPathDict["left"]; }
    public Vector3 RightPos() { return mPathDict["right"]; }
    public Vector3 Forward() { return mPathDict["forward"]; }
    public GameObject CrashPoint { get; set; }

    public Vector3 CurrentLocationPosition
    {
        get
        {
            Vector3 pos = Vector3.zero;
            switch(mCurrentLocation)
            {
                case E_BossLocation.Middle:
                    pos = MiddlePos();
                    break;
                case E_BossLocation.Left:
                    pos = LeftPos();
                    break;
                case E_BossLocation.Right:
                    pos = RightPos();
                    break;
                case E_BossLocation.Forward:
                    pos = Forward();
                    break;
            }
            return pos;
        }
    }

    public bool NeedBack()
    {
        if (mCurrentLocation == E_BossLocation.Forward)
        {
            mCurrentLocation = mLastLocation;
            return true;
        }
        return false;
    }

    public void ReachMiddle() { mLastLocation = mCurrentLocation; mCurrentLocation = E_BossLocation.Middle; }
    public void ReachLeft() { mLastLocation = mCurrentLocation; mCurrentLocation = E_BossLocation.Left; }
    public void ReachRight() { mLastLocation = mCurrentLocation; mCurrentLocation = E_BossLocation.Right; }
    public void ReachForward() { mLastLocation = mCurrentLocation; mCurrentLocation = E_BossLocation.Forward; }
    public bool IsInLocation(E_BossLocation location) { if (mCurrentLocation == location) return true; return false; }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        if (mIsKilled) return;

        if(mFSMSystem.currentState.stateID != BullDemonKingStateID.Appear)
        {
            if (ioo.gameMode.IsUsingHittingPartAllBreaked())
                DisInvincible();
            else
                EnterInvincible();
        }

        ioo.cameraManager.LookAt(CrashPoint.transform);

        mFSMSystem.currentState.Act(actionType);
        mFSMSystem.currentState.Reason(actionType);
    }

    private void MakeFSM()
    {
        mFSMSystem = new BullDemonKingFSMSystem();

        BullDemonKingAppearState appearState = new BullDemonKingAppearState(mFSMSystem, this);
        appearState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);

        BullDemonKingIdleState idleState = new BullDemonKingIdleState(mFSMSystem, this);
        idleState.AddTransition(BullDemonKingTransition.Desk, BullDemonKingStateID.Desk);
        idleState.AddTransition(BullDemonKingTransition.Sofa, BullDemonKingStateID.Sofa);
        idleState.AddTransition(BullDemonKingTransition.NotInRightPosition, BullDemonKingStateID.Move);
        idleState.AddTransition(BullDemonKingTransition.FireFist, BullDemonKingStateID.FireFist);
        idleState.AddTransition(BullDemonKingTransition.FireCricle, BullDemonKingStateID.FireCricle);
        idleState.AddTransition(BullDemonKingTransition.OXHorn, BullDemonKingStateID.OXHorn);
        idleState.AddTransition(BullDemonKingTransition.NoHealth, BullDemonKingStateID.Dead);

        BullDemonKingMoveState moveState = new BullDemonKingMoveState(mFSMSystem, this);
        moveState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);

        BullDemonKingDeskState deskState = new BullDemonKingDeskState(mFSMSystem, this);
        deskState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);

        BullDemonKingSofaState sofaState = new BullDemonKingSofaState(mFSMSystem, this);
        sofaState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);


        BullDemonKingFireFistState fireFistState = new BullDemonKingFireFistState(mFSMSystem, this);
        fireFistState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);
        fireFistState.AddTransition(BullDemonKingTransition.BreakFireFist, BullDemonKingStateID.BreakFireFist);

        BullDemonKingOXHornState oXHornState = new BullDemonKingOXHornState(mFSMSystem, this);
        oXHornState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);
        oXHornState.AddTransition(BullDemonKingTransition.BreakOXhorn, BullDemonKingStateID.BreakOXhorn);

        BullDemonKingFireCricleState fireCricleState = new BullDemonKingFireCricleState(mFSMSystem, this);
        fireCricleState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);
        fireCricleState.AddTransition(BullDemonKingTransition.BreakFireCricle, BullDemonKingStateID.BreakFireCricle);

        BullDemonKingBreakFireCricleState breakFireCricleState = new BullDemonKingBreakFireCricleState(mFSMSystem, this);
        breakFireCricleState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);

        BullDemonKingBreakFireFistState breakFireFistState = new BullDemonKingBreakFireFistState(mFSMSystem, this);
        breakFireFistState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);

        BullDemonKingBreakOXHornState breakOXHornState = new BullDemonKingBreakOXHornState(mFSMSystem, this);
        breakOXHornState.AddTransition(BullDemonKingTransition.Rest, BullDemonKingStateID.Idle);

        BullDemonKingDeadState deadState = new BullDemonKingDeadState(mFSMSystem, this);
        deadState.AddTransition(BullDemonKingTransition.Disappear, BullDemonKingStateID.Disappear);

        BullDemonKingDisappearState disappearState = new BullDemonKingDisappearState(mFSMSystem, this);

        mFSMSystem.AddState(appearState, idleState, moveState, deskState, sofaState, fireFistState, oXHornState, fireCricleState, breakFireCricleState, breakFireFistState, breakOXHornState, deadState, disappearState);
    }

    public void OnSkillBreaked()
    {
        attr.TakeDamage(20);
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
}