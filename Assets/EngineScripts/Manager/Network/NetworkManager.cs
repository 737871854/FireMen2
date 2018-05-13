using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class NetworkManager : BaseLua {
    private int count;
    //private TimerInfo timer;
    private bool islogging = false;
    private static Queue<KeyValuePair<int, ByteBuffer>> sEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

    new void Start() {
        base.Start();
    }

    public void OnInit() {
        if (uluaMgr == null) return;
        uluaMgr.CallLuaFunction("Network.Start");
    }

    ///------------------------------------------------------------------------------------
    public static void AddEvent(int _event, ByteBuffer data) {
        sEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
    }

    void Update(){
        if (sEvents.Count > 0) {
            while (sEvents.Count > 0) {
                KeyValuePair<int, ByteBuffer> _event = sEvents.Dequeue();
				byte[] temp = _event.Value.ToBytes();
                switch (_event.Key) {
				default: CallMethod("OnSocket", _event.Key, temp); break;
				}
			}
        }
    }

    public void Logout() {
        SocketClient.Logout();
    }

    /// <summary>
    /// 发送链接请�?
    /// </summary>
    public void SendConnect() {
        SocketClient.SendConnect();
    }

    public void SendConnect(string addr, int port) {
        SocketClient.SendConnect(addr, port);
    }

    /// <summary>
    /// 发送SOCKET消息
    /// </summary>
    public void SendMessage(ByteBuffer buffer) {
        SocketClient.SendMessage(buffer);
    }

    public bool IsConnectSuccess() {
        return SocketClient.ConnectSuccess();
    }

    public string GetErrorMsg() {
        return SocketClient.GetErrorMsg();
    }

    /// <summary>
    /// 析构函数
    /// </summary>
    new void OnDestroy() {
        base.OnDestroy();
        Debug.Log("~NetworkManager was destroy");
    }
}
