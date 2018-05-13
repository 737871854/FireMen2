/**
* Copyright (广州纷享游艺设备有限公司-研发视频组) 2012,广州纷享游艺设备有限公司
* All rights reserved.
* 
* 文件名称：IOManager.cs
* 简    述：获取模拟器操作信息
* 创建标识：meij  2015/10/28
* 修改标识：meij  2015/11/10
* 修改描述：采用1个COM端口进行协议收发。
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class IOManager : MonoBehaviour
{
    public E_InputType inputType = E_InputType.Builtin;

    private SerialIOHost serialIoHost;
    private IOEvent ie;
    private IOEvent[] ioEvent;                       //协议信息数组   每帧最多包含MAX_PLAYER_NUMBER个协议数,
    private int ioCount;

    private byte[] byteHost = new byte[14];

    #region  Unity Call Back
    void Awake()
    {
        ioo.gameManager.RegisterUpdate(UpdateInputEvent);
        ioo.gameManager.RegisterUpdate(UpdateIOEvent);

        ioo.gameManager.RegisterFixedUpdate(FixedUpdateInputEvent);
    }
    void Destroy()
    {
        ioo.gameManager.UnregisterUpdate(UpdateInputEvent);
        ioo.gameManager.UnregisterUpdate(UpdateIOEvent);

        ioo.gameManager.UnregisterFixedUpdate(FixedUpdateInputEvent);
    }

    void Start()
    {
        Init(Define.MAX_PLAYER_NUMBER, inputType);
        ioo.safeNet.InitDog();
    }

    #endregion
    
    #region MyRegion
    ///// <summary>
    /////  进入后台通知IO Board
    ///// </summary>
    //public void SendMessageEnterSetting()
    //{
    //    if (ioo.gameController.Type == InputType.Mouse)
    //        return;

    //    byte[] data = new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x60 };

    //    serialiohost.WriteAndSendMessage(0, data);

    //    //Debug.Log("发送进入后台信号给IO Board");
    //}

    ///// <summary>
    ///// 退出后台
    ///// </summary>
    //public void SendMessageExitSetting()
    //{
    //    if (ioo.gameController.Type == InputType.Mouse)
    //        return;

    //    byte[] data = new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x40 };

    //    serialiohost.WriteAndSendMessage(0, data);

    //    //Debug.Log("发送退出后台信号给IO Board");
    //}

    ///// <summary>
    ///// 游戏开始信号
    ///// </summary>
    //public void SendMessageGameBegine()
    //{
    //    if (ioo.gameController.Type == InputType.Mouse)
    //        return;

    //    byte[] data = new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x80 };

    //    serialiohost.WriteAndSendMessage(0, data);
    //    //serialiohost.Send();
    //    //CoroutineController.Instance.StartCoroutine(SendGameStartMessageToIOBoard(data));
    //}

    ///// <summary>
    ///// 游戏结束信号
    ///// </summary>
    //public void SendMessageGameEnd()
    //{
    //    if (ioo.gameController.Type == InputType.Mouse)
    //        return;

    //    byte[] data = new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x40 };

    //    serialiohost.WriteAndSendMessage(0, data);
    //}
    #endregion

    /// <summary>
    /// 应用程序退出操作
    /// </summary>
    public void Close()
    {
        if (serialIoHost != null)
        {
            serialIoHost.Close();
        }    
    }

    //初始化操作，打开端口，初始化ioEvent数组
    public void Init(int portCount, E_InputType type)
    {
        ioCount          = portCount;
        ioEvent          = new IOEvent[portCount];
        for (byte i = 0; i < ioCount; i++)
        {
            ioEvent[i] = new IOEvent();
        }

        if (inputType == E_InputType.External)
        {
            serialIoHost = new SerialIOHost();
            serialIoHost.Init(7, 7, 10);
        }
    }

    public void ReadDatas()
    {
        //byte h = serialiohost.Read(-1);

        //byte k = serialiohost.Read(0);
        //byte q = serialiohost.Read(1);

        //ioevent[0].IsGather = (IOParser.GetBit(0, k) == 1);
        //ioevent[0].IsPullDown = (IOParser.GetBit(1, k) == 1);
        //ioevent[0].IsPullUp = (IOParser.GetBit(2, k) == 1);
        //ioevent[0].IsTurnRight = (IOParser.GetBit(3, k) == 1);
        //ioevent[0].IsTurnLeft = (IOParser.GetBit(4, k) == 1);
        //ioevent[0].IsMissile = (IOParser.GetBit(5, k) == 1);
        //ioevent[0].IsStart = (IOParser.GetBit(6, k) == 1);
        //ioevent[0].IsCoin = (IOParser.GetBit(7, k) == 1);

        //ioevent[0].IsConfirm = (IOParser.GetBit(7, q) == 1);
        //ioevent[0].IsSelect = (IOParser.GetBit(6, q) == 1);
        //ioevent[0].IsResetEye = (IOParser.GetBit(5, q) == 1);
        //ioevent[0].IsUpEye = (IOParser.GetBit(4, q) == 1);
        //ioevent[0].IsDownEye = (IOParser.GetBit(3, q) == 1);
        //ioevent[0].IsTicket = (IOParser.GetBit(2, q) == 1);

        //IOManager.Instance.NeedOutPutTicket = System.BitConverter.ToInt32(new byte[] { serialiohost.Read(4), serialiohost.Read(3), 0, 0 }, 0);

        //serialiohost.ResetRead();

        //if (h == 0xAA)
        //{
        //    // 投币
        //    if (ioevent[0].IsCoin)
        //    {
        //        ioo.gameMode.Normal();
        //        SettingManager.Instance.AddCoin();
        //        SettingManager.Instance.Save();
        //        EventDispatcher.TriggerEvent(EventDefine.Event_Update_Coin);
        //    }

        //    if (ioevent[0].IsMissile)
        //    {
        //        if (ioo.gameMode.State == GameState.Coin)
        //            ioo.gameMode.Normal();
        //        EventDispatcher.TriggerEvent(EventDefine.Event_Sure_Or_Missile);
        //    }

        //    //if (ioo.gameMode.State == GameState.Coin)
        //    //{
        //    //    // 开始游戏
        //    //    if (ioevent[0].IsStart)
        //    //    {
        //    //        ioo.gameMode.Normal();
        //    //        EventDispatcher.TriggerEvent(EventDefine.Event_Button_Sure);
        //    //    }
        //    //}

        //    // 射击
        //    if (ioo.gameMode.State == GameState.Play)
        //    {
        //        if (ioevent[0].IsStart)
        //        {
        //            //EventDispatcher.TriggerEvent(EventDefine.Event_Player_Fire);
        //            ioo.gameMode.Player.IsPress = true;
        //            ioo.gameMode.CanFire = true;
        //        }
        //        else
        //        {
        //            ioo.gameMode.Player.IsPress = false;
        //            ioo.gameMode.CanFire = false;
        //        }

        //        if (ioevent[0].IsGather)
        //        {
        //            EventDispatcher.TriggerEvent(EventDefine.Event_Player_Gather);
        //        }
        //    }

        //    //if (ioo.gameMode.State == GameState.Continue)
        //    //{
        //    //    if (ioevent[0].IsStart)
        //    //    {
        //    //        EventDispatcher.TriggerEvent(EventDefine.Event_Button_Sure);
        //    //    }
        //    //}

        //    //if (ioo.gameMode.State == GameState.Select)
        //    //{
        //    //    // 确认地图
        //    //    if (ioevent[0].IsStart)
        //    //    {
        //    //        EventDispatcher.TriggerEvent(EventDefine.Event_Button_Sure);
        //    //    }
        //    //}

        //    // 后台A
        //    if (ioevent[0].IsConfirm)
        //    {
        //        ioo.gameMode.Normal();
        //        EventDispatcher.TriggerEvent(EventDefine.Event_Button_A);
        //    }

        //    // 后台B
        //    if (ioevent[0].IsSelect)
        //    {
        //        ioo.gameMode.Normal();
        //        EventDispatcher.TriggerEvent(EventDefine.Event_Button_B);
        //    }

        //    IOManager.Instance.ResetEvent_0(0);
        //}
    }

    /// <summary>
    /// 每帧分别读取一次协议队列中的协议，并存入协议信息类对象数组ioEvent
    /// </summary>
    public void UpdateIOEvent()
    {
        if(serialIoHost != null && serialIoHost.HadDevice())
        {
            serialIoHost.Update();
         
            //向下位机发协议
            //// 操作：左
            //if (ioevent[0].IsTurnLeft)
            //{
            //    EventDispatcher.TriggerEvent(EventDefine.Event_Turn_Left);
            //}

            //// 操作：右
            //if (ioevent[0].IsTurnRight)
            //{
            //    EventDispatcher.TriggerEvent(EventDefine.Event_Turn_Right);
            //}

            //// 操作：上
            //if (ioevent[0].IsPullUp)
            //{
            //    EventDispatcher.TriggerEvent(EventDefine.Event_Turn_Up);
            //}

            //// 操作：下
            //if (ioevent[0].IsPullDown)
            //{
            //    EventDispatcher.TriggerEvent(EventDefine.Event_Turn_Down);
            //}

            //if (ioo.gameMode.State == GameState.Select)
            //{
            //    IOManager.Instance.ResetEvent_1(0);
            //}
        }            
     }
    
    private object[] objs = new object[2];
    /// <summary>
    /// 键盘控制
    /// </summary>
    public void UpdateInputEvent()
    {
        // Player0
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ioEvent[0].IsCoin = true;
                IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onCoin, 0);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                ioEvent[0].IsStart = true;
                EventDispatcher.TriggerEvent(EventDefine.Event_Key_Sure, 0);
                if (!ioo.gameMode.IsGameOver())
                    IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onSure, 0);
            }
        }
        
        // Player1
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                ioEvent[1].IsCoin = true;
                IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onCoin, 1);
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                ioEvent[1].IsStart = true;
                EventDispatcher.TriggerEvent(EventDefine.Event_Key_Sure, 1);
                if (!ioo.gameMode.IsGameOver())
                    IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onSure, 1);
            }
        }

        // Player2
        {
            if (Input.GetKeyDown(KeyCode.F9))
            {
                ioEvent[2].IsCoin = true;
                IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onCoin, 2);
            }

            if (Input.GetKeyDown(KeyCode.F11))
            {
                ioEvent[2].IsStart = true;
                EventDispatcher.TriggerEvent(EventDefine.Event_Key_Sure, 2);
                if (!ioo.gameMode.IsGameOver())
                    IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onSure, 2);
            }
           
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            ioEvent[0].IsPush1 = true;
            IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onButtonA);
        }
        else if (Input.GetKeyUp(KeyCode.PageUp))
        {
            ioEvent[0].IsPush1 = false;
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            ioEvent[0].IsPush2 = true;
            IOLuaHelper.Instance.TriggerListener(EnumIOEvent.onButtonB);
        }else if (Input.GetKeyUp(KeyCode.PageUp))
        {
            ioEvent[0].IsPush2 = false;
        }
    }

    /// <summary>
    /// 反馈玩家水标位置
    /// </summary>
    public void FixedUpdateInputEvent()
    {
        // 键盘测试玩，只给0号玩家控制鼠标的权利
        if (inputType == E_InputType.Builtin)
        {
            ioo.gameMode.ScreenPosition(0, Input.mousePosition);
        }
        else
        {

        }
        
    }
}

