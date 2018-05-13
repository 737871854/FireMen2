/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   VolumeByDistance.cs
 * 
 * 简    介:    不想把AudioManager做的过于复杂，单独把需要近大远小的功能独立出来; 后期有时间再统一整合（也可独立让对象自动生成）
 * 
 * 创建标识：   Pancake 2017/7/31 16:48:30
 * 
 * 修改描述：   
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class VolumeByDistance : MonoBehaviour
{
    public AudioClip Clip;
    public GameObject FollowObj;
    public float Radius = 150;

    // 听音着
    public Transform Target;

    private AudioSource Source;

    void Start()
    {
        Source = gameObject.GetOrAddComponent<AudioSource>();
    }

    void Update()
    {
        if (Target == null)
            return;

        if (Clip == null || FollowObj == null || Source == null)
            return;

        if (FollowObj.activeSelf)
        {
            if (Source.clip == null)
            {
                Source.clip = Clip;
            }

            if (!Source.isPlaying)
            {
                Source.loop = true;
                Source.Play();
            }

            transform.position = FollowObj.transform.position;
            float distance = Vector3.Distance(transform.position, Target.position);

            float spatialBlend = distance / Radius;

            Source.spatialBlend = spatialBlend;
        }
        else
        {
            if (Source.isPlaying)
            {
                Source.Stop();
            }
        }

    }

}
