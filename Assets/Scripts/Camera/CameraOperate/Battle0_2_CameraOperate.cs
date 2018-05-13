/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Battle0_2_CameraOperate.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 14:15:13
 * 
 * 修改描述：   
 * 
 */

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Battle0_2_CameraOperate : ICameraOperate
{
    private Vector3 mStartPos;
    private Vector3 mEndPos;
    private Vector3 mMiddlePos;
    private Vector3 mLeftPos;
    private Vector3 mRightPos;
    public Battle0_2_CameraOperate(CameraManager cameraManager) : base(cameraManager)
    {
        GameObject pathParcent = GameObject.FindGameObjectWithTag(GameTage.WayParent);
        if (pathParcent == null) { Debug.LogError("WayParent for camera is not exit"); return; }
        mStartPos = pathParcent.transform.Find("one").transform.position;
        mEndPos = pathParcent.transform.Find("two").transform.position;
        mMiddlePos = pathParcent.transform.Find("three").transform.position;
        mLeftPos = pathParcent.transform.Find("four").transform.position;
        mRightPos = pathParcent.transform.Find("five").transform.position;

        parcent.position = mStartPos;
    }


    public void BullCameraBackLens()
    {
        mParcent.DOMove(mEndPos, 8.0f);
    }

    public void BullMiddleSkilltLens()
    {
        mParcent.DOMove(mMiddlePos, 1.5f);
    }

    public void BullLeftSkillLens()
    {
        mParcent.DOMove(mLeftPos, 1.5f);
        //Vector3 direction = mLeftPos - mEndPos;
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(mParcent.DOMove(mLeftPos, 1.5f));
        //sequence.Join(mParcent.DOLookAt(direction, 1.5f));
    }

    public void BullRightSkillLens()
    {
        mParcent.DOMove(mRightPos, 1.5f);
        //Vector3 direction = mRightPos - mEndPos;
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(mParcent.DOMove(mRightPos, 1.5f));
        //sequence.Join(mParcent.DOLookAt(direction, 1.5f));
    }

    public void BullReversLens()
    {
        mParcent.DOMove(mEndPos, 1.5f);
        //Vector3 direction = mStartPos - mEndPos;
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(mParcent.DOMove(mEndPos, 1.5f));
        //sequence.Join(mParcent.DOLookAt(direction, 1.5f));
    }

    public override float GetCameraSpeed(string eventName)
    {
        throw new NotImplementedException();
    }

    public override void OnCustomEvent(string eventName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateState(int state)
    {
        throw new NotImplementedException();
    }
}