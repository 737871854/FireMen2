/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IMGUIBull.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 10:38:13
 * 
 * 修改描述：   
 * 
 */
using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;
public class IMGUIBull : MonoBehaviour
{
    [SerializeField]
    private MediaPlayer _mediaPlayer;

    [SerializeField]
    private DisplayIMGUI _iMGUI;
    private bool _isPlaying;

    private void Start()
    {
        EventDispatcher.AddEventListener<int>(EventDefine.Event_Key_Sure, OnSkip);

        _mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "bull.mp4");
        _isPlaying = true;
        _iMGUI.enabled = true;
        _mediaPlayer.Control.Play();
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveEventListener<int>(EventDefine.Event_Key_Sure, OnSkip);
    }

    /// <summary>
    /// 停止视频播放
    /// </summary>
    /// <param name="data"></param>
    private void OnSkip(int index)
    {
        if (!_isPlaying)
            return;

        _isPlaying = false;
        _mediaPlayer.Stop();
        _iMGUI.enabled = false;
        EventDispatcher.TriggerEvent(EventDefine.Event_Game_Success);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlaying)
            return;

        if (_mediaPlayer != null && _iMGUI != null && _mediaPlayer.Control.IsFinished())
        {
            _isPlaying = false;
            EventDispatcher.TriggerEvent(EventDefine.Event_Game_Success);
        }

    }
}