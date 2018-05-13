/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IHitPoint.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/20 13:44:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHitPoint : MonoBehaviour
{
    protected int mCurrentHealth;
    protected int mMaxHealth = 3;

    protected SphereCollider mSphereCollider;

    protected float mMoveSpeed;

    protected HittingPart mHP = new HittingPart();

    private void Start()
    {
        Rigidbody rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        ioo.characterSystem.AddHitPoint(this);
        ioo.gameMode.AddHPToAllHitPoint(mHP);
        mSphereCollider = GetComponent<SphereCollider>();
        mSphereCollider.isTrigger = true;
        mSphereCollider.enabled = false;
        BindEvent();
    }

    private void OnDestroy()
    {
        ioo.characterSystem.RemoveHitPoint(this);
        ioo.gameMode.RemoveHPFromAllHitPoint(mHP);
        RemoveEvent();
    }

    public virtual void BindEvent()
    {
        EventDispatcher.AddEventListener(EventDefine.Event_DisActive_HitPoint, OnDisActive);
    }
    public virtual void RemoveEvent()
    {
        EventDispatcher.RemoveEventListener(EventDefine.Event_DisActive_HitPoint, OnDisActive);
    }

    public virtual void UnderAttack(Player player)
    {
        if (mCurrentHealth > 0)
        {
            ioo.audioManager.PlaySound2D("sfx_sound_hit_cricle");
            mCurrentHealth -= player.attackValue;
            mHP.curHp = mCurrentHealth;
            EventDispatcher.TriggerEvent(EventDefine.Boss_Hit_Point_Break, player.id, player.attackValue);
        }

        if (mCurrentHealth <= 0)
        {
            mCurrentHealth = 0;
            mHP.curHp = 0;
        }
    }

    public virtual void Active()
    {
        mCurrentHealth = mMaxHealth;
        mHP.maxHp = mMaxHealth;
        mHP.curHp = mHP.maxHp;
        mHP.scale = 1.0f;
        mSphereCollider.enabled = true;
    }

    void OnDisActive()
    {
        mSphereCollider.enabled = false;
        mHP.curHp = 0;
    }

    public virtual void UpdatePreFrame()
    {

    }

    private void Update()
    {
        Canvas canvas = ioo.gameMode.UICanvas;
        Vector2 pos0 = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 pos1;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, pos0, canvas.worldCamera, out pos1))
        {
            mHP.uiPos = pos1;
        }

        UpdatePreFrame();
    }
}