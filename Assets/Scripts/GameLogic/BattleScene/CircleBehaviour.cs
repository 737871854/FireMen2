/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CircleBehaviour.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 10:19:15
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    public Transform Center;
    public float Speed { get; set; }
    public Vector3 Direction { get { return mDirection; } }

    private bool mCanRotation;
    private Vector3 mDirection;

    private float mAngle;

    private void Start()
    {
        Speed = 0.5f;
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Circle_Rotation, OnRotation);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Circle_Rotation, OnRotation);
    }

    private void Update()
    {
        mDirection = transform.position - Center.position;

        //Vector3 axis = Camera.main.transform.forward;
        //transform.position = RotateRound(transform.position, Center.position, axis);

        transform.RotateAround(Center.position, Camera.main.transform.forward, Speed);
        mAngle += Speed;
        if (mAngle >= 60 || mAngle <= -60) Speed = -Speed;
    }

    //private Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis)
    //{
    //    Vector3 point = Quaternion.AngleAxis(Speed, axis) * (position - center);
    //    Vector3 resultVec3 = center + point;
    //    return resultVec3;
    //}

    private void OnRotation(bool value)
    {
        mCanRotation = true;
    }
}