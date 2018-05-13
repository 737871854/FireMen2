/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   AudioChannel.cs
 * 
 * 简    介:    负责音效资源的播放停止和销毁，以及播放位置
 * 
 * 创建标识：   Pancake 2017/4/20 11:48:00
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class AudioChannel : MonoBehaviour
{
    private bool mIsFree = true;

    /// <summary>
    /// 音效名字
    /// </summary>
    public string mName;

    private AudioSource mAudioSource;
    /// <summary>
    /// 是否是Music
    /// </summary>
    public bool mIsMusic;

    /// <summary>
    /// 是否循环播放
    /// </summary>
    private bool mIsLoop;

    /// <summary>
    /// 指定播放音效的位置
    /// </summary>
    private Vector3 mTargetPos;

    /// <summary>
    /// 指定音效播放到哪个物体
    /// </summary>
    private GameObject mTargetObj;

    /// <summary>
    /// 更随指定物体
    /// </summary>
    private bool mIsFollow;

    private float mStartVolume;
    private float mEndVolume;

    /// <summary>
    /// 音效
    /// </summary>
    private AudioClip mClip;

    public bool IsFree { get { return mIsFree; } }
    public string Name { get { return mName; } }
    public AudioClip Clip   { get { return mClip; } }


    public void Init(string name, AudioClip clip, bool spatial = false)
    {
        if (spatial)
            mAudioSource.spatialBlend = 1;
        else
            mAudioSource.spatialBlend = 0;

        mName = name;
        mClip = clip;
    }

    public void Init(string name, AudioClip clip, GameObject obj, bool spatial = false)
    {
        if (spatial)
            mAudioSource.spatialBlend = 1;
        else
            mAudioSource.spatialBlend = 0;
        mName       = name;
        mClip       = clip;
        mTargetObj  = obj;
        mIsFollow   = true;
    }

    public void Init(string name, AudioClip clip, Vector3 pos, bool spatial = false)
    {
        if (spatial)
            mAudioSource.spatialBlend = 1;
        else
            mAudioSource.spatialBlend = 0;
        mName       = name;
        mClip       = clip;
        mTargetPos  = pos;
        mIsFollow   = false;
    }

    /// <summary>
    /// 被回收后重置数据
    /// </summary>
    public void Reset()
    {
        mName       = string.Empty;
        mIsMusic    = false;
        mTargetObj  = null;
        mTargetPos  = Vector3.zero;
        mAudioSource.clip = null;
        mClip = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 是否正在播放
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        if (mClip == null)
            return false;
        if (mAudioSource.isPlaying)
            return true;
        return false;
    }

    /// <summary>
    /// Music必须设置
    /// </summary>
    /// <param name="ismusic"></param>
    /// <param name="isloop"></param>
    public void SetMusic(bool isloop, float start = 0, float end = 1)
    {
        mIsMusic                = true;
        mIsLoop                 = isloop;
        mStartVolume            = start * SettingManager.Instance.GameVolume * 0.1f;
        mEndVolume              = end * SettingManager.Instance.GameVolume * 0.1f;
        mAudioSource.volume     = mStartVolume;
        mAudioSource.loop       = mIsLoop;
    }

    public void SetSound()
    {
        mIsMusic = false;
        mStartVolume = mEndVolume = SettingManager.Instance.GameVolume * 0.1f;

        mAudioSource.volume = mStartVolume;
        mAudioSource.loop   = false;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void Play()
    {
        if (mTargetObj == null)
            transform.position = mTargetPos;

        mIsFree              = false;
        mAudioSource.clip    = mClip;
        mAudioSource.Play();
    }

    /// <summary>
    /// 停止播放音效
    /// </summary>
    public void Stop()
    {
        mIsFree = true;
        mIsLoop = false;
        mAudioSource.Stop();
        ioo.audioManager.ToFree(this);
    }

    //public void EndLife()
    //{
    //    _lifeTime = 0;
    //}

    //private void Clear()
    //{
    //    mName               = string.Empty;
    //    mAudioSource.clip   = null;
    //    mIsMusic            = false;
    //    mTargetObj          = null;
    //    mTargetPos          = Vector3.zero;
    //    gameObject.SetActive(false);
    //}

    void Awake()
    {
        mAudioSource = gameObject.GetOrAddComponent<AudioSource>();
        mAudioSource.playOnAwake = false;
    }

    public void UpdateChannel()
    {
        if (mIsFollow)
        {
            if (mTargetObj != null)
                transform.position = mTargetObj.transform.position;
            else
            {
                mIsFollow = false;
                Stop();
                return;
            }
        }

        if (!mIsFree)
        {
            if (!mAudioSource.isPlaying)
            {
                if (!mIsFree)
                    Stop();
            }
            else
            {
                if (mAudioSource.volume < mEndVolume)
                    mAudioSource.volume += Time.deltaTime;
                else
                    mAudioSource.volume = mEndVolume;
            }
        }
    }
}
