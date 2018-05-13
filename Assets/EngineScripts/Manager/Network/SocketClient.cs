using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public enum DisType { 
    Exception,
    Disconnect,
}

public enum NetActionType {
    Connect,
    Message,
    Logout
}

public class SocketClient : MonoBehaviour {
	private enum Session
	{
		SESSION_SIGN,
		SESSION_VERIFY,
		SESSION_VERIFY_FAILED,
		SESSION_RECV_KEY,
		SESSION_TRAFFIC,
	}

    private TcpClient client = null;
    private NetworkStream outStream = null;
    private MemoryStream memStream;
    private BinaryReader reader;
	private const int MAX_READ = 8192;
    private string _errorMsg = "";
    private byte[] byteBuffer = new byte[MAX_READ];

    public static bool loggedIn = false;
    private static Queue<KeyValuePair<NetActionType, ByteBuffer>> _events = new Queue<KeyValuePair<NetActionType, ByteBuffer>>();

	//服务器的公钥
	private static string java_publickey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDeyg3TGNnmgDbr0j2gFhGy93WB
m0ciBatzUt7vhZMBdUUYoqDTHK5wtV/dabPDaipGeVkzFdUG6GRL+s++z5F6lUtB
gEr8fidNfcCIfShqmR5L4So76W5cOIPsGoM2igWFJQL4QDjVmML4NsqLc8OaZ9ej
1CurS3aoROcCXoAw5QIDAQAB";
	//客户端的私钥
	private static string java_privatekey = @"MIICXQIBAAKBgQC7SmozcaeKk8Okmd22fmrZjY0i+W/IBdUgWcQzLipYN2TZmd9t
NgcSFBd9dfRcYmaX3Lyjpdm5cKXLr1ctlYIN2+TkL22AnKSi/4SNunT4t+i0Olzs
Zez+QwRUh/EbQAWq9bAOWMqfyswdx5fcGerKvDpxgQs2m4vDxbX/ZFRFRwIDAQAB
AoGAWh1S/g+oal/wmYlDCWTIKocWKobUBuzvgBJQ+cMzsqBskNqdiyGcw1ERgFc5
zR23eUhHJ4JMQRJ3Y4qpKpCuMwQs0jGvWY4Jf+B5mh6RmHVA3Olmv8bHLFh2iJOH
LEgV5/ICyi++2z50tohx3vn3wzWhhYoW1GIb9QJMBdPOJHkCQQDbCZ8cmp2uH61/
8G3rmywICzhpl4BYH7dtHCMg3Z6Hqi5JEs2neneTvmZfGwCY51YL9ms9cssmw2XB
DDwopzotAkEA2uVVb6/Hu068gcAfH+qF/hN0NueGwFXzRmffOhXmYUsUTcSqk932
cCjIlehHGXbkGnUl44CIBo4qOGK9vQDpwwJAY+lhoKSOZEyi0YcUPLJNRWYI13F5
47ij7Ks3AtjUZUGlV0Oyd0CPpt7kx2EDxrtPLqm6hQ8Fx6q9kW9JSanuCQJBAIS6
yNqHUOof7SgUEgttTsBolXBxZYEc3P3VIEN9Ygue1fnuBazRy4vo/u//P5WORPRS
Ep5nopOvAqTcIscHVbcCQQCdMJvncpw3K1cISx3bHrOLmE4Vh0V7CdEp1fjIF+VG
Xp+GtR7mlA6e00gzG7xCuOGi0zis4KzpUKLi2Tpij9d9";

	private RSACryptoService rsaServer = null;
	private Session session_state = Session.SESSION_SIGN;
	private byte[] verify_data = new byte[8];
	private zreader z_reader = null;
    private uint sessionid = 0;

    // Use this for initialization
    void Awake() {
        memStream = new MemoryStream();
        reader = new BinaryReader(memStream);
		Setup ();
    }

	public void Setup()
	{
		if (rsaServer == null)
		{
			rsaServer = new RSACryptoService (java_privatekey, java_publickey);
		}
		session_state = Session.SESSION_SIGN;
	}

    private void InitZReader() {
		z_reader = null;
		z_reader = new zreader ();
		z_reader.zreader_init (12);
    }

    /// <summary>
    /// 消息循环
    /// </summary>
    void Update() {
        while (_events.Count > 0) {
            KeyValuePair<NetActionType, ByteBuffer> _event = _events.Dequeue();
            switch (_event.Key) {
                case NetActionType.Connect:
                    string addr = _event.Value.ReadString();
                    int port = _event.Value.ReadInt();
                    Debugger.Log(addr + ":" + port);
                    ConnectServer(addr, port);
                break;
                case NetActionType.Message: 
                    SessionSend(_event.Value.ToBytes());
                break;
                case NetActionType.Logout: Close(); break;
            }
            if (_event.Value != null) _event.Value.Close();
        }
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    void ConnectServer(string host, int port) {
        sessionid++;
        client = null;
        client = new TcpClient();
        client.SendTimeout = 1000;
        client.ReceiveTimeout = 1000;
        client.NoDelay = true;
        InitZReader();
        try {
            client.BeginConnect(host, port, new AsyncCallback(OnConnect), sessionid);
        } catch (Exception e) {
            _errorMsg = e.ToString();
            Close(); Debug.LogError(e.Message);
        }
    }

    /// <summary>
    /// 连接上服务器
    /// </summary>
    void OnConnect(IAsyncResult asr) {
        if ((uint)asr.AsyncState != sessionid || !client.Connected) {
            _errorMsg = "connect time out";
		    client.EndConnect (asr);
            return;
        }
		client.EndConnect (asr);
        outStream = client.GetStream();
        client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), asr.AsyncState);
		ByteBuffer buffer = new ByteBuffer();
		buffer.WriteInt ((int)Protocal.Connect);
		NetworkManager.AddEvent(Protocal.Connect, buffer);
    }

    /// <summary>
    /// 写数据
    /// </summary>
    void WriteMessage(byte[] message) {
        MemoryStream ms = null;
		byte[] rc4_message = message;
		if (session_state == Session.SESSION_TRAFFIC)
		{
			rc4_message = rc4.RC4_Transform(rc4_message);
		}
		using (ms = new MemoryStream()) {
            ms.Position = 0;
            BinaryWriter writer = new BinaryWriter(ms);
			UInt32 msglen = (UInt32)rc4_message.Length;
            writer.Write(msglen);
			writer.Write(rc4_message);
            writer.Flush();
            if (client != null && client.Connected) {
                //NetworkStream stream = client.GetStream(); 
                byte[] payload = ms.ToArray();
                outStream.BeginWrite(payload, 0, payload.Length, new AsyncCallback(OnWrite), sessionid);
            } else {
                //Debug.LogError("client.connected----->>false");
				Debug.LogWarning("client.connected----->>false");
            }
        }
    }

    /// <summary>
    /// 读取消息
    /// </summary>
    void OnRead(IAsyncResult asr) {

        int bytesRead = 0;
        try {
            //对不上了，这说明是前一次的数据，这时候TCP CLIENT已经被清掉，数据得丢掉
            if ((uint)asr.AsyncState != sessionid) {
                return;
            }
            lock (client.GetStream()) {         //读取字节流到缓冲区
                bytesRead = client.GetStream().EndRead(asr);
            }
            if (bytesRead < 1) {                //包尺寸有问题，断线处理
                OnDisconnected(DisType.Disconnect, "bytesRead < 1");
                return;
            }
			OnReceive(byteBuffer, bytesRead);   //分析数据包内容，抛给逻辑层

            lock (client.GetStream()) {         //分析完，再次监听服务器发过来的新消息
                Array.Clear(byteBuffer, 0, byteBuffer.Length);   //清空数组
                client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), asr.AsyncState);
            }
        } catch (Exception ex) {
            //PrintBytes();
			if (client != null)
			{
            	OnDisconnected(DisType.Exception, ex.GetType().ToString() + " : " + ex.Message);
                Debug.LogError(ex.ToString());
			}
        }
    }

    /// <summary>
    /// 丢失链接
    /// </summary>
    void OnDisconnected(DisType dis, string msg) {
        Close();   //关掉客户端链接
        int protocal = dis == DisType.Exception ? 
        Protocal.Exception : Protocal.Disconnect;

        ByteBuffer buffer = new ByteBuffer();
		buffer.WriteInt((int)protocal);
        NetworkManager.AddEvent(protocal, buffer);
    }

    /// <summary>
    /// 打印字节
    /// </summary>
    /// <param name="bytes"></param>
    void PrintBytes(byte[] bytes) { 
        string returnStr = string.Empty; 
        for (int i = 0; i < bytes.Length; i++) {
            returnStr += bytes[i].ToString("X2"); 
        }
        Debug.LogError(returnStr);
    }

    /// <summary>
    /// 向链接写入数据流
    /// </summary>
    void OnWrite(IAsyncResult r) {
        
        try {
            if ((uint)r.AsyncState != sessionid) {
                return;
            }
            outStream.EndWrite(r);
        } catch (Exception ex) {
            Debug.LogError("OnWrite--->>>" + ex.Message);
        }
    }

	void OnSessionSign(byte[] bytes)
	{
		if (rsaServer == null)
			return;
		byte[] res = rsaServer.Decrypt (bytes);

		// 将解密出来的数据直接回发给服务器，不加数据长度
		WriteMessage (res);

		System.Random r = new System.Random();
		for (int i = 0; i < 8; ++i)
		{
			verify_data[i] = (byte)r.Next(0, 255);
		}
		byte[] change_data = rsaServer.Encrypt (verify_data);
		WriteMessage (change_data);
		session_state = Session.SESSION_VERIFY;
	}

	void OnSessionVerify(byte[] bytes)
	{
		bool succeed = true;
		for (int i = 0; i < 8; ++i)
		{
			if (verify_data[i] != bytes[i])
			{
				succeed = false;
				break;
			}
		}
		if (succeed)
		{
			session_state = Session.SESSION_RECV_KEY;
		}
		else
		{
			session_state = Session.SESSION_VERIFY_FAILED;
		}
	}

	void OnSessionRecvKey(byte[] bytes)
	{
		if (rsaServer == null)
			return;
		byte[] rc4_key_byte = rsaServer.Decrypt (bytes);
		rc4.rc4_key = (int)((rc4_key_byte[0] & 0xFF)
		                    | ((rc4_key_byte[1] & 0xFF) << 8)
		                    | ((rc4_key_byte[2] & 0xFF) << 16)
		                    | ((rc4_key_byte[3] & 0xFF) << 24));

		rc4.CreateContext();
		session_state = Session.SESSION_TRAFFIC;
	}

	/// <summary>
	/// 接收到消息
	/// </summary>
	void OnReceive(byte[] bytes, int length)
	{
		z_reader.zreader_read (bytes, (UInt16)length);
		int i = 0;
		int start = 0;

		for(i = 0, start = 0; i < z_reader.eobcnt; start = z_reader.eob[i], ++i)
		{
			int packetlen = z_reader.eob[i] - start;
			byte[] data = new byte[packetlen];
			Array.ConstrainedCopy (z_reader.buf, start, data, 0, packetlen);
			switch(session_state)
			{
			case Session.SESSION_SIGN:
				OnSessionSign(data);
				break;
			case Session.SESSION_VERIFY:
				OnSessionVerify(data);
				break;
			case Session.SESSION_RECV_KEY:
				OnSessionRecvKey(data);
				break;
			case Session.SESSION_TRAFFIC:
				// 进入正常的协议传输，这里开始无加密
				OnReceivedMessage(data);

				break;
			case Session.SESSION_VERIFY_FAILED:
				break;
			}
		}
		z_reader.zreader_clear ();
		//memStream.SetLength(0);
	}

    /// <summary>
    /// 剩余的字节
    /// </summary>
    private long RemainingBytes() {
        return memStream.Length - memStream.Position;
    }

	void OnReceivedMessage(byte[] message)
	{
		ByteBuffer buffer = new ByteBuffer (message);
		int mainId = buffer.ReadInt ();
		NetworkManager.AddEvent (mainId, buffer);
	}

    /// <summary>
    /// 接收到消息
    /// </summary>
    /// <param name="ms"></param>
    //void OnReceivedMessage(MemoryStream ms) {
        //BinaryReader r = new BinaryReader(ms);
        //byte[] message = r.ReadBytes((int)(ms.Length - ms.Position));

        //ByteBuffer buffer = new ByteBuffer(message);
		//int mainId = buffer.ReadInt();
        //NetworkManager.AddEvent(mainId, buffer);
    //}


    /// <summary>
    /// 会话发送
    /// </summary>
    void SessionSend(byte[] bytes) {
        WriteMessage(bytes);
    }

    /// <summary>
    /// 关闭链接
    /// </summary>
    void Close() { 
        if (client != null) {
            sessionid++;
            if (client.Connected) {
                client.Close();
            }
            outStream = null;
            client = null;

        }
        loggedIn = false;
		session_state = Session.SESSION_SIGN;

		if (z_reader != null)
		{
			z_reader.zreader_clear ();
			z_reader.zreader_deinit();
			z_reader = null;
		}
		memStream.SetLength(0);
    }

	void OnDestroy()
	{
		Close ();
	}

    /// <summary>
    /// 登出
    /// </summary>
    public static void Logout() { 
        _events.Enqueue(new KeyValuePair<NetActionType, ByteBuffer>(NetActionType.Logout, null));
    }

    /// <summary>
    /// 发送连接请求
    /// </summary>
    public static void SendConnect() {
        //_events.Enqueue(new KeyValuePair<NetActionType, ByteBuffer>(NetActionType.Connect, null));
        SendConnect(Const.SocketAddress, Const.SocketPort);
    }

    public static void SendConnect(string addr, int port) {
        ioo.gameManager.GetComponent<SocketClient>()._errorMsg = "";
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteString(addr);
        buffer.WriteInt(port);
        _events.Enqueue(new KeyValuePair<NetActionType, ByteBuffer>(NetActionType.Connect, new ByteBuffer(buffer.ToBytes())));
        buffer.Close();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public static void SendMessage(ByteBuffer buffer) {
        _events.Enqueue(new KeyValuePair<NetActionType, ByteBuffer>(NetActionType.Message, buffer));
    }

    //只能这样获取状态了
    public static bool ConnectSuccess() {
        return ioo.gameManager.GetComponent<SocketClient>().session_state == Session.SESSION_TRAFFIC;
    }

    public static string GetErrorMsg() {
        return ioo.gameManager.GetComponent<SocketClient>()._errorMsg;
    }
}
