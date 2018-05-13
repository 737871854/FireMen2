using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;

public class IMGUIIdle : MonoBehaviour
{
    void OnEnable()
    {
        ioo.RegesterListener(EventLuaDefine.Coin_Event_Play_Idle_Movie, PlayIdleMovie, "IdleMoviePlay");
        ioo.RegesterListener(EventLuaDefine.Coin_Event_Stop_Idle_Movie, StopIdleMovie, "IdleMoviePlay");
    }

    void OnDisable()
    {
        ioo.RemoveListener(EventLuaDefine.Coin_Event_Play_Idle_Movie, "IdleMoviePlay");
        ioo.RemoveListener(EventLuaDefine.Coin_Event_Stop_Idle_Movie, "IdleMoviePlay");
    }

    [SerializeField]
    private MediaPlayer _mediaPlayer;

    [SerializeField]
    private DisplayIMGUI _iMGUI;
    private bool _isPlaying;
    /// <summary>
    /// 播放视频
    /// </summary>
    /// <param name="data"></param>
    private void PlayIdleMovie(object data)
    {
        if (_isPlaying)
            return;

        _mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "Idle0.mp4");
        _isPlaying = true;
        _iMGUI.enabled = true;
        _mediaPlayer.Control.Play();
    }

    /// <summary>
    /// 停止视频播放
    /// </summary>
    /// <param name="data"></param>
    private void StopIdleMovie(object data)
    {
        if (!_isPlaying)
            return;

        _isPlaying = false;
        _mediaPlayer.Stop();
        _iMGUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlaying)
            return;

        if (_mediaPlayer != null && _iMGUI != null && _mediaPlayer.Control.IsFinished())
        {
            _isPlaying = false;
            _iMGUI.enabled = false;
            ioo.TriggerListener(EventLuaDefine.Coin_Event_End_Idle_Movie);
        }

    }
}
