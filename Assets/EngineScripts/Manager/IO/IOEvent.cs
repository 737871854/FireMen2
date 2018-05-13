/**
* Copyright (广州纷享游艺设备有限公司-研发视频组) 2012,广州纷享游艺设备有限公司
* All rights reserved.
* 
* 文件名称：IOEvent.cs
* 简    述：每条协议包含的信息
* 创建标识：meij  2015/11/2
* 修改标识：meij  2015/11/06
* 修改描述：代码优化。
*/
using UnityEngine;
using System.Collections;

public class IOEvent
{
    private byte _ID ;                 // 角色ID
    private bool _isOutTicket;         // 出票
    private bool _isCoin;              // 投币
    private bool _isStart;             // 开始
    private bool _isPush1;             // 后台红色按钮
    private bool _isPush2;             // 后台绿色按钮
    private bool _isLeft;              // 左转
    private bool _isRight;             // 右转

    //角色id
    public  byte ID                              
    {
        get { return _ID; }
        set { _ID = value; }
    }

    //1: 出票 0：否
    public  bool IsOutTicket                      
    {
        get { return _isOutTicket; }
        set { _isOutTicket = value; }
    }

    //1：投票  0：否
    public  bool IsCoin                           
    {
        get { return _isCoin; }
        set { _isCoin = value; }
    }

    //
    public bool IsPush1
    {
        get { return _isPush1; }
        set { _isPush1 = value; }
    }

    //
    public bool IsPush2
    {
        get { return _isPush2; }
        set { _isPush2 = value; }
    }

    //
    public bool IsStart
    {
        get { return _isStart; }
        set { _isStart = value; }
    }

    //
    public bool IsLeft
    {
        get { return _isLeft; }
        set { _isLeft = value; }
    }

    //
    public bool IsRight
    {
        get { return _isRight; }
        set { _isRight = value; }
    }
}
