/********************************************************************
    Copyright 2015,广州擎天柱网络科技有限公司
    All rights reserved.

    文件名称：Log.cs
    简    述：用于把信息输出到屏幕上，方便在手机设备上查看错误和警告信息。
    创建标识：Lorry 2015/10/8
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Log : MonoBehaviour
{

    private static List<string> linesText = new List<string>();
    private static List<LogType> linesType = new List<LogType>();

    private static List<string> msglist = new List<string>(); //更改为msglist new List<string>();

    private GUIStyle errorGS = new GUIStyle();
    private GUIStyle normalGS = new GUIStyle();
    private GUIStyle warnGS = new GUIStyle();

    private Texture2D back = null;

    //是否继续记录信息
    static private bool m_stopLog = false;
    //按钮的尺寸及位置
    float btnX, btnY, btnW, btnH;

    float charH;
    // Use this for initialization
    void Start()
    {
        btnX = 0;// Screen.width / 4;
        btnY = 0;
        btnW = Screen.width / 10;
        btnH = Screen.height / 10;
        charH = Screen.height / 36;

        back = (Texture2D)Resources.Load("Textures/logo", typeof(Texture2D)); //  背景纹理

        errorGS.normal.background = null;
        errorGS.normal.textColor = new Color(1, 0, 0);
        errorGS.fontSize = (int)charH;

        normalGS.normal.background = null;
        normalGS.normal.textColor = new Color(1, 1, 1);
        normalGS.fontSize = (int)charH;

        warnGS.normal.background = null;
        warnGS.normal.textColor = new Color(1, 1, 0);
        warnGS.fontSize = (int)charH;


        PrintMsg("-----start-----Log-------");
    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector2 scrollpos = new Vector2(30, 50);


    /// <summary>
    /// 垂直属性值
    /// </summary>
    private static float verticalValue = 0;
    /// <summary>
    /// 最大行数,Lorry
    /// </summary>
    private const int MAX_PRIVATE = 25;

    private bool showLog = false;

    /// <summary>
    /// 向左移动的距离
    /// </summary>
    private float moveLeftDis = 0;

    void OnGUI()
    {
        if (showLog == false)
        {
            if (GUI.Button(new Rect(btnX, btnY, btnW, btnH), "Show"))
            {
                showLog = true;
            }
            return;
        }

        if (GUI.Button(new Rect(btnX, btnY, btnW, btnH), "Close"))
        {
            showLog = false;
            return;
        }

        if (Time.timeScale != 0)
        {
            if (GUI.Button(new Rect(btnX + btnW, btnY, btnW, btnH), "Pause"))
            {
                Time.timeScale = 0;
            }
        }
        else
        {
            if (GUI.Button(new Rect(btnX + btnW, btnY, btnW, btnH), "Play"))
            {
                Time.timeScale = 1;
            }
        }

        if (m_stopLog)
        {
            if (GUI.Button(new Rect(btnX + btnW, btnY, btnW, btnH), "ReLog"))
            {
                m_stopLog = false;
            }
        }
        else
        {
            if (GUI.Button(new Rect(btnX + btnW, btnY, btnW, btnH), "StopLog"))
            {
                m_stopLog = true;
            }
        }

        //可以向左移动整个输出文字
        if (GUI.Button(new Rect(btnX + 2 * btnW, btnY, btnW, btnH), "ToLeft"))
        {
            moveLeftDis += btnW;
        }
        //还原整个输出文字的位置
        if (GUI.Button(new Rect(btnX + 3 * btnW, btnY, btnW, btnH), "Reset"))
        {
            moveLeftDis = 0;
        }

        if (showLog)
        {
            //GUI.Label(new Rect(0, 20, 30, 200), back);
            GUI.DrawTexture(new Rect(0, btnH, Screen.width, charH * MAX_PRIVATE), back, ScaleMode.StretchToFill);
            int maxLine = linesText.Count;
            int startIndex = 0;
            if (maxLine > MAX_PRIVATE)
            {
                verticalValue = GUI.VerticalSlider(new Rect(0, btnH, btnW / 2, charH * MAX_PRIVATE), verticalValue, 0, maxLine - MAX_PRIVATE);
                startIndex = (int)verticalValue;
            }
            //输出Debug信息;
            for (int i = 0; i < MAX_PRIVATE; i++)
            {
                if (startIndex + i < maxLine)
                {
                    GUIStyle tempTs = normalGS;
                    switch (linesType[startIndex + i])
                    {
                        case LogType.Error:
                        case LogType.Exception:
                            tempTs = errorGS;
                            break;
                        case LogType.Warning:
                            tempTs = warnGS;
                            break;
                        default:
                            break;
                    }
                    GUI.Label(new Rect(btnW / 2 - moveLeftDis, btnH + charH * i, Screen.width + moveLeftDis, charH), linesText[startIndex + i], tempTs);
                }
                else
                {
                    break;
                }
            }
        }
    }

    static int enterIndex = 0;
    static string m_LastString = "";

    public static void PrintMsg(string msg, LogType type = LogType.Log)
    {
        if (m_LastString == msg)
            return;

        if (m_stopLog)
            return;
        //m_LastString考虑用来
        m_LastString = msg;

        string text = enterIndex + ":" + msg;
        enterIndex++;

        msglist.Add(text);
        //NGUIText.WrapText(text, out text);
        //**//text = UITool.FONT.WrapText(text, Screen.width / 14, 0, false, UIFont.SymbolStyle.None);
        string[] strArr = text.Split('\n');
        linesText.AddRange(strArr);
        for (int i = 0; i < strArr.Length; i++)
        {
            linesType.Add(type);
        }
        //if (debugInfos.Count > 50000)
        if (msglist.Count > 500)
        {
            msglist.RemoveAt(0);
        }
        verticalValue = linesText.Count - MAX_PRIVATE;
        //Debugger.Log(msg);
    }
}
