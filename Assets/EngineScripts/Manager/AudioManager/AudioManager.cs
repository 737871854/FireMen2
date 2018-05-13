/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   AudioManager.cs
 * 
 * 简    介:    音效管理，音效资源的创建和管理，但不负责音效的具体功能操作          TODO: 功能还要进一步完善
 * 
 * 创建标识：   Pancake 2017/4/2 10:12:28
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// 用于缓存AudioClip以及Asset
    /// </summary>
    private class AudioCache
    {
        // 未使用的音效缓存列表
        private List<AudioClip> mClipList;
        /// <summary>
        /// 允许拥有音效的最大数量
        /// </summary>
        private int mMaxCount;

        public int Count()
        {
            return mClipList.Count;
        }

        /// <summary>
        ///  音效资源
        /// </summary>
        private UnityEngine.Object mAsset;
        public UnityEngine.Object Asset { set { mAsset = value; } }

        public AudioCache()
        {
            mMaxCount = 10;
            mClipList = new List<AudioClip>();
        }

        /// <summary>
        /// 将暂不使用的音效回收
        /// </summary>
        /// <param name="clip"></param>
        public void AddClip(AudioClip clip)
        {
            if (mClipList.Count >= mMaxCount)
            {
                GameObject.Destroy(clip);
                return;
            }
            mClipList.Add(clip);
        }

        /// <summary>
        /// 获取一个音效
        /// </summary>
        /// <returns></returns>
        public AudioClip GetClip()
        {
            while(mClipList.Count > 0)
            {
                AudioClip clip = mClipList[0];
                mClipList.RemoveAt(0);

                if (clip == null)
                    continue;
                else
                    return clip;
            }

            return CreateClip();

            //if (mClipList.Count > 0)
            //{
            //    AudioClip clip = mClipList[0];
            //    mClipList.RemoveAt(0);
            //    return clip;
            //}
            //else
            //    return CreateClip();
        }

        /// <summary>
        /// 清理内存
        /// </summary>
        public void ClearMemory()
        {
            GC.Collect();
            Resources.UnloadAsset(mAsset);
        }

        /// <summary>
        /// 创建一个音效
        /// </summary>
        /// <returns></returns>
        private AudioClip CreateClip()
        {
            AudioClip clip = (AudioClip)GameObject.Instantiate(mAsset);
            return clip;
        }
    }

    /// <summary>
    /// 所有音效根组件
    /// </summary>
    private GameObject mAudioParent;

    /// <summary>
    /// 允许最大AudioChannel数量
    /// </summary>
    private int mMaxChannel = 20;

    /// <summary>
    /// 配置文件
    /// </summary>
    private Dictionary<string, string> mConfigDic = new Dictionary<string, string>();
    /// <summary>
    /// 缓存频道，正在使用
    /// </summary>
    private List<AudioChannel> mUseList = new List<AudioChannel>();
    /// <summary>
    /// 缓存频道，不使用
    /// </summary>
    private List<AudioChannel> mFreeList = new List<AudioChannel>();
    /// <summary>
    /// 即将要被释放的
    /// </summary>
    private List<AudioChannel> mToFreeList = new List<AudioChannel>();
    /// <summary>
    /// 缓存没被使用的音效
    /// </summary>
    private Dictionary<string, AudioCache> mClipCacheDic = new Dictionary<string, AudioCache>();
    /// <summary>
    /// 需要播放的音效列表（防止同一帧播放过多的音效）
    /// </summary>
    private List<AudioChannel> mPlayList = new List<AudioChannel>();

    void Awake()
    {
        mAudioParent = new GameObject("AudioParent");
        mAudioParent.AddComponent<AudioListener>();
        DontDestroyOnLoad(mAudioParent);
        Init();
    }

    #region 提供外部调用方法 无需玩家调用
    /// <summary>
    /// 由AudioChannel自动调用
    /// </summary>
    /// <param name="channel"></param>
    public void ToFree(AudioChannel channel)
    {
        // 防止同一帧里多次调用停止播放同一音效
        if (mToFreeList.Contains(channel))
            return;
        mToFreeList.Add(channel);
    }

    /// <summary>
    /// 切换场景时，有场景管理器自动调用
    /// </summary>
    public void Clear()
    {
        //mUseList.Clear();
        //mFreeList.Clear();
        mClipCacheDic.Clear();
    }
    #region 背景音乐
    /// <summary>
    /// 播放背景音效
    /// </summary>
    /// <param name="name"></param>
    public void PlayBackMusic(string name, bool loop = true)
    {
        if (string.IsNullOrEmpty(name)) return;
        AudioChannel item = GetAudioItem();
        AudioClip clip = LoadAudicClip(name);
        item.Init(name, clip, false);
        item.SetMusic(loop);
        mPlayList.Add(item);
    }

    /// <summary>
    /// 停止播放背景音效
    /// </summary>
    public void StopBackMusic(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        for (int i = 0; i < mUseList.Count; ++i )
        {
            if (name.Equals(mUseList[i].Name))
            {
                AudioChannel item = mUseList[i];
                item.Stop();
            }
        }
    }
    #endregion

    #region 音效
    /// <summary>
    /// 在指定位置上播放音效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pos"></param>
    /// <param name="spatial"></param>
    public void PlaySoundOnPoint(string name, Vector3 pos)
    {
        if (string.IsNullOrEmpty(name)) return;
        AudioChannel item = GetAudioItem();
        AudioClip clip = LoadAudicClip(name);
        item.Init(name, clip, pos, true);
        item.SetSound();
        mPlayList.Add(item);
    }

   /// <summary>
   /// 在指定对象上播放音效
   /// </summary>
   /// <param name="name">音效名</param>
   /// <param name="go">指定对象</param>
   /// <param name="spatial">是否是3D音效</param>
    public void PlaySoundOnObj(string name, GameObject go)
    {
        if (string.IsNullOrEmpty(name)) return;
        AudioChannel item = GetAudioItem();
        AudioClip clip = LoadAudicClip(name);           
        item.Init(name, clip, go, true);
        item.SetSound();
        mPlayList.Add(item);
    }

    /// <summary>
    /// 无需指定位置音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound2D(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        AudioChannel item = GetAudioItem();
        AudioClip clip = LoadAudicClip(name);
        item.Init(name, clip);
        item.SetSound();
        mPlayList.Add(item);
    }
    #endregion

    #region 人物语音
    /// <summary>
    /// 播放任务语音（中文版本和英文版本），不可中断播放
    /// </summary>
    /// <param name="name"></param>
    public void PlayPersonSound(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        int language = SettingManager.Instance.GameLanguage;
        string extend = language == 0 ? "_ch" : "en";
        name += extend;
        PlaySound2D(name);
    }

    /// <summary>
    /// 播放人物语音（中文版本和英文版本），以便后面可以强行中断播放
    /// </summary>
    /// <param name="name"></param>
    public void PlayPersonMusic(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        int language = SettingManager.Instance.GameLanguage;
        string extend = language == 0 ? "_ch" : "_en";
        name += extend;
        PlayBackMusic(name, false);
    }

    /// <summary>
    /// 中断人物语音播放（中文版本和英文版本）
    /// </summary>
    /// <param name="name"></param>
    public void StopPersonMusic(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        int language = SettingManager.Instance.GameLanguage;
        string extend = language == 0 ? "_ch" : "_en";
        name += extend;
        for (int i = 0; i < mUseList.Count; ++i)
        {
            if (name.Equals(mUseList[i].Name))
            {
                AudioChannel item = mUseList[i];
                item.Stop();
            }
        }
    }

    #endregion


    /// <summary>
    /// 停止所有音效
    /// </summary>
    public void StopAll()
    {
        foreach(AudioChannel channel in mUseList)
            channel.Stop();
    }

    #endregion
    private void Init()
    {
        Prepare();
        LoadAudioConfig();
    }

    private void Prepare()
    {
        for (int i = 0; i < 10; ++i )
        {
            AudioChannel item  = CreateAudioItem();
            mFreeList.Add(item);
        }
    }

    /// <summary>
    /// 加载配置文件
    /// </summary>
    private void LoadAudioConfig()
    {
        mConfigDic.Clear();
        if (!File.Exists(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path)))
        {
            Debugger.LogError("缺少音效配置文件");
            return;
        }

        string[] lines = File.ReadAllLines(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path));
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue   = line.Split(',');
            mConfigDic.Add(keyvalue[0], keyvalue[1]);
        }
    }

    /// <summary>
    /// 载入一个音频
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private AudioClip LoadAudicClip(string name)
    {
        if (!ClipIsExit(name))
        {
            Debugger.LogError("The audio name: " + name + " is not exit!");
            return null;
        }

        AudioClip clip = null;

        if (mClipCacheDic.ContainsKey(name))
        {
            if (mClipCacheDic[name] != null)
            {
                AudioCache cache = mClipCacheDic[name];
                clip = cache.GetClip();
            }
            else
            {
                mClipCacheDic.Remove(name);
            }
        }

        if (clip != null)
            return clip;    

        ResourceMisc.AssetWrapper aw = ioo.resourceManager.LoadAsset(mConfigDic[name], typeof(AudioClip));
        if (null == aw)
        {
            Debugger.LogError("The AudioClip name: " + name + " is not exit, please check the assetbundle");
            return null;
        }
        clip = (AudioClip)aw.GetAsset();

        AudioCache temp = new AudioCache();
        mClipCacheDic.Add(name, temp);
        temp.Asset = aw.GetAsset();
        clip = temp.GetClip();

        return clip;
    }

    /// <summary>
    /// 指定音效是否存在于配置文件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private bool ClipIsExit(string name)
    {
        if (mConfigDic.ContainsKey(name))
            return true;
        return false;
    }

    /// <summary>
    /// 获取一个AudioChannel
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private AudioChannel GetAudioItem()
    {
        AudioChannel item = null;
        if (mFreeList.Count > 0)
            item = mFreeList[0];

        if (null != item)
            mFreeList.Remove(item);
        else
            item = CreateAudioItem();

        mUseList.Add(item);
        item.gameObject.SetActive(true);
        return item;
    }

    /// <summary>
    /// 创建AudioChannel
    /// </summary>
    /// <returns></returns>
    private AudioChannel CreateAudioItem()
    {
        int count = mUseList.Count + mFreeList.Count;
        GameObject obj = new GameObject("AudioChannel" + count);
        obj.transform.SetParent(mAudioParent.transform);
        obj.transform.localPosition = Vector3.zero;
        AudioChannel item = obj.GetOrAddComponent<AudioChannel>();
        obj.SetActive(false);
        return item;
    }

    void Update()
    {       
        for (int i = 0; i < mUseList.Count; ++i )
        {
            AudioChannel channel = mUseList[i];
            channel.UpdateChannel();
        }

        for (int i = 0; i < mToFreeList.Count; ++i )
        {
            AudioChannel channel = mToFreeList[i];

            // 回收AudioClip
            Dictionary<string, AudioCache>.Enumerator er = mClipCacheDic.GetEnumerator();
            while (er.MoveNext())
            {
                if (er.Current.Key.Equals(channel.Name))
                {
                    AudioCache cache = er.Current.Value;
                    cache.AddClip(channel.Clip);
                    channel.Reset();
                    break;
                }
            }

            // 回收AudioChannel
            mFreeList.Add(channel);
            mUseList.Remove(channel);

            if (mUseList.Count + mFreeList.Count > mMaxChannel && mFreeList.Count > 0)
            {
                GameObject obj = mFreeList[0].gameObject;
                mFreeList.RemoveAt(0);
                GameObject.Destroy(obj);
            }
        }

        mToFreeList.Clear();

        if(mPlayList.Count != 0)
        {
            AudioChannel channel = mPlayList[0];
            mPlayList.RemoveAt(0);
            channel.Play();
        }
    }

    /////// <summary>
    /////// 使用范例
    /////// </summary>
    //void OnGUI()
    //{
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 40;
    //    if (mClipCacheDic["sfx_sound_crash_screen"] != null)
    //    {
    //        GUI.Label(new Rect(100, 350, 100, 100), "数量：" + mClipCacheDic["sfx_sound_crash_screen"].Count().ToString());

    //    }

    //    //GUI.Label(new Rect(100, 400, 100, 100), mUseList.Count + "  " + mFreeList.Count, style);
    //    //if (GUI.Button(new Rect(100, 100, 100, 100), "PlayMusic"))
    //    //{
    //    //    ioo.audioManager.PlayBackMusic("standby_sound");
    //    //}

    //    //if (GUI.Button(new Rect(100, 200, 100, 100), "StopMusic"))
    //    //{
    //    //    ioo.audioManager.StopBackMusic("standby_sound");
    //    //}

    //    //if (GUI.Button(new Rect(100, 300, 100, 100), "PlaySound"))
    //    //{
    //    //    ioo.audioManager.PlaySound2D("insert_coin_sound");
    //    //}
    //}
}
