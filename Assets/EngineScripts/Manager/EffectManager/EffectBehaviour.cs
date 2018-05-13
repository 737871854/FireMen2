/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EffectBehaviour.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/5/27 9:21:37
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections;

public class EffectBehaviour : MonoBehaviour
{
    // 特效跟随目标
    private Transform toFollow;

    public Transform ToFollow { set { toFollow = value; } }

    private ParticleSystem[] Particles;

    private float Duration;

    private float PlayTime;

    #region Unity CallBack
    void OnDisable()
    {
        PlayTime = 0;
        toFollow = null;
    }

    void Start()
    {
        Particles = transform.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < Particles.Length; ++i )
        {
            if (Particles[i].main.duration > Duration)
                Duration = Particles[i].main.duration;
        }
    }

    void Update()
    {
        if (PlayTime < Duration)
            PlayTime += Time.deltaTime;
        else
            DeSpawn();

        if (toFollow == null)
            return;

        transform.position = toFollow.position;
    }
    #endregion

    #region Private Function

    private void DeSpawn()
    {
        PoolManager.Instance.DeSpawn(gameObject);
    }
    #endregion
}
