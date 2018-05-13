/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   TimerManager.cs
 * 
 * 简    介:    时间管理类，注：参考了几个项目的时间管理器功能与设计方式
 * 
 * 创建标识：   Pancake 2016/10/28 16:38:41
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


#region TimerObject
public delegate void TimerTriggerCallback(TimerObject timerObj);

/// <summary>
/// 定时器对象;
/// </summary>
public class TimerObject
{
    ////////////////////////////////数据变量//////////////////////////////////
    /// <summary>
    /// 定时器GUID;
    /// </summary>
    private int Guid;
    /// <summary>
    /// 起点;
    /// </summary>
    private int startTick;
    /// <summary>
    /// 起点;
    /// </summary>
    private int endTick;

    /// <summary>
    /// 间隔多长时间触发;
    /// </summary>
    private int triggerTick;

    /// <summary>
    /// 触发回调;
    /// </summary>
    private TimerTriggerCallback callback;
    //////////////////////////////////////////////////////////////////////////


    ////////////////////////////////内部逻辑变量/////////////////////////////
    /// <summary>
    /// 当前时间;
    /// </summary>
    private int curTick;
    /// <summary>
    /// 过去时间
    /// </summary>
    private int oldTick;

    /// <summary>
    /// 暂停计时器
    /// </summary>
    private bool pause = false;

    /// <summary>
    /// 过期
    /// </summary>
    private bool isOver = false;
    //////////////////////////////////////////////////////////////////////////

    #region 属性访问器;
    /// <summary>
    /// 定时器GUID;
    /// </summary>
    public int TimerGuid
    {
        get
        {
            return Guid;
        }
    }
    /// <summary>
    /// 起点;
    /// </summary>
    public int StartTick
    {
        get
        {
            return startTick;
        }
    }
    /// <summary>
    /// 起点;
    /// </summary>
    public int EndTick
    {
        get
        {
            return endTick;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public int OldTick
    {
        get
        {
            return oldTick;
        }
        set
        {
            oldTick = value;
        }
    }
    /// <summary>
    /// 当前时间;
    /// </summary>
    public int CurTick
    {
        get
        {
            //return (int)Mathf.Round((curTick + 499.9f) / 1000.0f);
            return curTick;
        }
        set
        {
            curTick = value;
        }
    }
    /// <summary>
    /// 间隔多长时间触发;
    /// </summary>
    public int TriggerTick
    {
        get
        {
            return triggerTick;
        }
    }
    /// <summary>
    /// 暂停
    /// </summary>
    public bool Pause
    {
        get
        {
            return pause;
        }

        set
        {
            pause = value;
        }
    }

    /// <summary>
    /// 过期
    /// </summary>
    public bool IsOver
    {
        get
        {
            return isOver;
        }
        set
        {
            isOver = value;
        }
    }

    /// <summary>
    /// 回调
    /// </summary>
    public TimerTriggerCallback CallBack
    {
        get
        {
            return callback;
        }
    }
    #endregion

    public TimerObject(int guid, int st, int et, int tt, TimerTriggerCallback callback)
    {
        this.Guid = guid;
        this.startTick = st;
        this.endTick = et;
        this.curTick = st;
        this.triggerTick = tt;
        this.callback = callback;
        this.pause = false;
        this.isOver = false;
        this.oldTick = st;
    }
}
#endregion

public class TimerManager : SingletonBehaviour<TimerManager>
{
    private int GuidIndex = 0;
    private float interval = 0;
    protected List<TimerObject> TimerList = new List<TimerObject>();
    protected List<TimerObject> RemoveList = new List<TimerObject>();

    public float Interval
    {
        get { return interval; }
        set { interval = value; }
    }

    // Use this for initialization
    void Start()
    {
        StartTimer(1);
    }

    /// <summary>
    /// 启动计时器
    /// </summary>
    /// <param name="interval"></param>
    public void StartTimer(float value)
    {
        interval = value;
        InvokeRepeating("Run", 0, interval);
    }

    /// <summary>
    /// 停止计时器
    /// </summary>
    public void StopTimer()
    {
        CancelInvoke("Run");
    }

    #region ------------------------------------添加计时器------------------------------------
    /// <summary>
    /// 添加一个定时器;
    /// </summary>
    public void AddTimerEvent(TimerObject timerObj)
    {
        if (!TimerList.Contains(timerObj))
        {
            TimerList.Add(timerObj);
        }
    }

    /// <summary>
    /// 添加一个定时器
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public int AddTimerEvent(int trigger, TimerTriggerCallback callback)
    {
        int guid = GetGuid();
        TimerObject timerObj = new TimerObject(guid, 0, -1, trigger, callback);
        TimerList.Add(timerObj);
        return guid;
    }

    /// <summary>
    /// 添加一个定时器;
    /// </summary>
    /// <param name="name"></param>
    /// <param name="o"></param>
    public int AddTimerEvent(int start, int end, int trigger, TimerTriggerCallback callback, bool startTrigger = false)
    {
        int guid = GetGuid();
        TimerObject timerObj = new TimerObject(guid, start, end, trigger, callback);
        TimerList.Add(timerObj);
        if (startTrigger)
        {
            //第一次触发一下;
            callback(timerObj);
        }
        return guid;
    }

    #endregion

    #region ------------------------------------删除定时器------------------------------------
    /// <summary>
    /// 删除一个定时器;
    /// </summary>
    /// <param name="name"></param>
    public TimerObject RemoveTimerEvent(int guid)
    {
        TimerObject tobeRemObj = GetTimerObject(guid);

        if (tobeRemObj != null)
        {
            TimerList.Remove(tobeRemObj);
        }

        return tobeRemObj;
    }

    /// <summary>
    /// 删除一个定时器;
    /// </summary>
    public void RemoveTimerEvent(TimerObject timerObj)
    {
        if (TimerList.Contains(timerObj))
        {
            TimerList.Remove(timerObj);
        }
    }
    #endregion

    #region ------------------------------------暂停计时器------------------------------------
    /// <summary>
    ///  暂停一个计时器事件
    /// </summary>
    /// <param name="timerObj"></param>
    public void PauseTimerEvent(TimerObject timerObj)
    {
        if (TimerList.Contains(timerObj))
        {
            timerObj.Pause = true;
        }
    }

    /// <summary>
    /// 暂停一个计时器事件
    /// </summary>
    /// <param name="guid"></param>
    public void PauseTimerEvent(int guid)
    {
        TimerObject timerObject = GetTimerObject(guid);
        if (null == timerObject)
        {
            return;
        }
        timerObject.Pause = true;
    }
    #endregion

    #region ------------------------------------继续计时-------------------------------
    /// <summary>
    /// 继续计时器事件
    /// </summary>
    /// <param name="info"></param>
    public void ResumeTimerEvent(TimerObject timerObj)
    {
        if (TimerList.Contains(timerObj))
        {
            timerObj.Pause = false;
        }
    }

    /// <summary>
    /// 继续计时器事件
    /// </summary>
    /// <param name="guid"></param>
    public void ResumeTimerEvent(int guid)
    {
        TimerObject timerObject = GetTimerObject(guid);
        if (null == timerObject)
        {
            return;
        }
        timerObject.Pause = false;
    }
    #endregion


    /// <summary>
    /// 获取一个定时器;
    /// </summary>
    public TimerObject GetTimerObject(int guid)
    {
        TimerObject tobeRemObj = null;
        for (int i = 0; i < TimerList.Count; ++i)
        {
            TimerObject to = TimerList[i];
            if (to.TimerGuid == guid)
            {
                tobeRemObj = to;
                break;
            }
        }

        return tobeRemObj;
    }

    /// <summary>
    /// 计时器运行
    /// </summary>
    void Run()
    {
        if (TimerList.Count == 0) return;
        for (int i = 0; i < TimerList.Count; i++)
        {
            TimerObject o = TimerList[i];
            if (o.Pause)
            {
                continue;
            }

            // 正常计时 间隔回调
            if (o.StartTick <= o.EndTick)
            {
                o.CurTick += 1;
                if ((o.CurTick - o.OldTick) == o.TriggerTick)
                {
                    o.CallBack(o);
                    o.OldTick = o.CurTick;
                }

                if (o.CurTick >= o.EndTick)
                {
                    o.IsOver = true;
                }
            }
            else// 倒计时 间隔回调
            {
                o.CurTick -= 1;
                if ((o.OldTick - o.CurTick) == o.TriggerTick)
                {
                    o.CallBack(o);
                    o.OldTick = o.CurTick;
                }

                if (o.CurTick <= o.EndTick)
                {
                    o.IsOver = true;
                }
            }

            // 在规定时间触发一次回调
            if (-1 == o.EndTick)
            {
                o.CurTick += 1;
                if (o.CurTick == o.TriggerTick)
                {
                    o.CallBack(o);
                    o.IsOver = true;
                }
            }

            if (o.IsOver)
            {
                RemoveList.Add(o);
            }
        }

        for (int i = 0; i < RemoveList.Count; i++)
        {
            TimerObject o = RemoveList[i];
            TimerList.Remove(o);
        }

        RemoveList.Clear();
    }

    #region 内部逻辑;

    private int GetGuid()
    {
        return GuidIndex++;
    }
    #endregion
}
