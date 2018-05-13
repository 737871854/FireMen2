/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UIPlayAnimCtrl.cs
 * 简    述：Unity 序列帧播放控制
 * 创建标识：DengFan  2015/12/02
 * 
 * 修改标识：
*/

using UnityEngine;
using System.Collections;

public class UIPlayAnimCtrl : MonoBehaviour
{
    public GameObject[] _objList;
    public float[] _time;
    public bool _isForever = false;  //是否一直循环
    public int _times = 1;     //循环的次数
    public bool _isAutoPlay = false; //是否自动播放

    private float[] _totalTimes;
    private bool _isFinish = true;
    private float _beginTime = 0;
    private float _endTime = 0;
    // Use this for initialization
    void Start () {
        _totalTimes = new float[_time.Length];
	    for(int i = 0; i < _time.Length; i++)
        {
            if(i > 0){
                _totalTimes[i] = _time[i] + _totalTimes[i - 1];
            }
            else{
                _totalTimes[i] = _time[i];
            }
            _endTime = _endTime + _totalTimes[i];
        }

        if (_isAutoPlay)
        {
            Play();
        }
	}
	void InitObj()
    {
        for (int i = 0; i < _objList.Length; i++)
        {
            _objList[i].SetActive(false);
        }
    } 
	// Update is called once per frame
	void Update ()
    {
        if (_isFinish) return;

        _beginTime += FightTime.deltaTime;
        if ((_isForever || _times > 0) && _beginTime >= _endTime)
        {
            _beginTime = 0;
            InitObj();
        }
        
        for (int i = 0; i < _totalTimes.Length; i++)
        {
            if(_beginTime >= _totalTimes[i] && !_objList[i].activeSelf)
            {
                if (i == _totalTimes.Length - 1 && !_isForever)
                {
                    _times -= 1;
                }
                _objList[i].SetActive(true);
            }

            if(_objList[_totalTimes.Length - 1].activeSelf && !_isForever)
            {
                if (_times <= 0)
                { 
                    _isFinish = true;
                }
            }       
        }
    }

    public void Destroy()
    {
        if (!_isDestroy)
        {
            for (int i = 0; i < _objList.Length; ++i)
            {
                GameObject.Destroy(_objList[i]);
            }
            _isDestroy = true;
        }
    }

    public void Play()
    {
        InitObj();
        _isFinish = false;
    }

    public void Stop()
    {
        _isFinish = true;
        _beginTime = 0;
    }

    public void SetPlayForever(bool _forever)
    {
        _isForever = _forever;
    }

    public void SetPlayTimes(int times)
    {
        _times = times;
    }

    private bool _isDestroy = false;
}
