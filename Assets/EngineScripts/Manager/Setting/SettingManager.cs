/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SettingManager.cs
 * 
 * 简    介:    SettingManager数据管理
 * 
 * 创建标识：  Pancake 2016/7/28 17:54:50
 * 
 * 修改描述：    2016.9.3 PlayerPrefs转Json
 * 
 */

using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;
using System.Text;

public class SettingManager
{

    private static readonly object mObject = new object();

    private static SettingManager mInstance;
    public static SettingManager Instance
    {
        get
        {
            if (null == mInstance)
            {
                lock(mObject)
                {
                    if (null == mInstance)
                        mInstance = new SettingManager();
                }
            }
            return mInstance;
        }
    }


    private SettingConfigData po;

    private string _checkID         = string.Empty;
    private int _gameRate           = 3;                                // 游戏币率
    private int _gameVolume         = 10;                               // 游戏音量
    private int _gameLanguage       = 0;                                // 游戏语言
    private int _ticketModel        = 0;                                // 出票模式
    private int _gameLevel         = 0;                                // 游戏难度

    private int[] _hasCoin;                                             // 玩家剩余币数

    private List<float[]> _monthList = new List<float[]>();             // 月份信息

    private float[] _totalRecord;                                       // 总记录

    private int _gameTime;  // 游戏时间
    private int _upTime;    // 开机时间
    private int _gameCount; // 游戏次数
    private int _addCoin;   // 投币
    private int _ticket;

    //public void Init()
    //{
    //    string filePath = Application.persistentDataPath + "/" + Const.Setting_Coinfig_Path;
    //    if (!File.Exists(filePath))
    //    {
    //        CoroutineController.Instance.StartCoroutine(ToCopy(Const.Setting_Coinfig_Path, Init));
    //    }
    //    else
    //    {
    //        PreLoadData();
    //    }
    //}
      
    //IEnumerator ToCopy(string fileName, Action handle)
    //{
    //    string src = Const.getStreamingPath_for_www(fileName);
    //    string des = Application.persistentDataPath + "/" + fileName;
    //    WWW www = new WWW(src);
    //    yield return www;
    //    if (!string.IsNullOrEmpty(www.error))
    //    {
    //        Debug.Log("www.error: " + www.error);
    //    }
    //    else
    //    {
    //        FileStream fsDes = new FileStream(des, FileMode.Create);
    //        fsDes.Write(www.bytes, 0, www.bytes.Length);
    //        fsDes.Flush();
    //        fsDes.Close();
    //    }
    //    www.Dispose();
    //    handle();
    //}

    public SettingManager()
    {
        // 取出后台存储所有数据
        this.po = LoadJson(Application.persistentDataPath + "/" + Const.Setting_Coinfig_Path);
        if (null == this.po)
        {
            this.po = new SettingConfigData();
        }

        // CheckID
        this._checkID = po.CheckId;

        // 游戏币率
        this._gameRate = po.GameRate;

        // 游戏语言版本 0中文 1英文
        this._gameLanguage = po.GameLanguage;

        // 检查点模式
        this._ticketModel = po.TicketModel;

        // 游戏音量
        this._gameVolume = po.GameVolume;

        // 游戏难度
        this._gameLevel = po.GameLevel;

        // 当前剩余币数
        this._hasCoin = po.Coin;

        // 月份信息
        this._monthList = po.MonthList;

        // 总记录
        this._totalRecord = po.TotalRecord;

        CheckIsNewMonth();
    }
   
    #region  -----------------后台提供借口-----------------------begin------------------
    // 校验ID
    public string CheckID
    {
        get { return _checkID; }
        set { _checkID = value; }
    }

    //获取游戏币率
    public int GameRate
    {
        get { return _gameRate; }
        set { _gameRate = value;}
    }
    //获取游戏音量
    public int GameVolume
    {
        get { return _gameVolume; }
        set { _gameVolume = value; }
    }
 
    //获取游戏语言
    public int GameLanguage
    {
        get { return _gameLanguage; }
        set { _gameLanguage = value; }
    }


    //获取1票需要多少分或出票模式
    public int TicketModel
    {
        get { return _ticketModel; }
        set { _ticketModel = value; }
    }
    
    // 游戏难度
    public int GameLevel
    {
        get { return _gameLevel; }
        set { _gameLevel = value; }
    }

    // 总记录
    public float[] TotalRecord()
    {
        return this._totalRecord;
    }

    public int[] HasCoin
    {
        get { return this._hasCoin; }
        set { this._hasCoin = value; }
    }

    public void UseCoin(int id)
    {
        this._hasCoin[id] -= this._gameRate;
        ioo.playerManager.UseCoin(id);
    }
    #endregion         -----------------后台提供借口-----------------end------------------------

    // 修改po的Coin值
    public void ClearCoin()
    {
        for (int i = 0; i < Define.MAX_PLAYER_NUMBER; ++i)
        {
            this._hasCoin[i] = 0;
        }       
    }

    // 清除月份信息
    public void ClearMonthInfo()
    {
        for (int i = 0; i < this._monthList.Count; i++ )
        {
            for (int j = 1; j < this._monthList[i].Length; j++ )
            {
                this._monthList[i][j] = 0;
            }
        }
    }

    // 清除总记录
    public void ClearTotalRecord()
    {
        for (int i = 0; i < this._totalRecord.Length; i++ )
        {
            this._totalRecord[i] = 0;
        }
    }


    public List<float[]> GetMonthData()
    {
        return this._monthList;
    }

    // 获取指定月份信息
    public float[] GetMonthData(int index)
    {
        return this._monthList[index];
    }

    // 游戏时间
    public void LogGameTimes(int value)
    {
        this._gameTime += value;
    }

    // 开机时间
    public void LogUpTime(int value)
    {
        this._upTime += value;
    }

    // 增加游戏次数
    public void LogNumberOfGame(int value)
    {
        this._gameCount += value;
    }

    // 增加出票数
    public void AddTicket(int value)
    {
        this._ticket = value;
    }

    // 投币
    public void AddCoin(int value)
    {
        this._addCoin += 1;
        this._hasCoin[value] += 1;
    }

   //----------------------------------------------

    // 保存后台信息
    public void Save()
    {
        CopyToPo();
        SaveJson(this.po, Application.persistentDataPath + "/" + Const.Setting_Coinfig_Path);
    }

    public void CopyToPo()
    {
      
        // 修改当期那月份信息
        {
            this._monthList[0][1] += this._addCoin;
            this._monthList[0][2] += this._ticket;
            this._monthList[0][3] += this._gameTime;
            this._monthList[0][4] += this._upTime;
        }

        // 修改总记录
        {
            this._totalRecord[0] += this._gameCount;
            this._totalRecord[1] += this._gameTime;
            this._totalRecord[2] += this._upTime;
            this._totalRecord[3] += this._addCoin;
            this._totalRecord[4] += this._ticket;
        }

        // 临时数据清除
        {
            this._gameCount = 0;
            this._gameTime  = 0;
            this._upTime    = 0;
            this._addCoin   = 0;
            this._ticket    = 0;
        }

        // 自改po的值
        {
            this.po.CheckId         = this._checkID;
            this.po.GameRate        = this._gameRate;
            this.po.GameLanguage    = this._gameLanguage;
            this.po.TicketModel     = this._ticketModel;
            this.po.GameVolume      = this._gameVolume;
            this.po.GameLevel       = this._gameLevel;
            this.po.MonthList       = this._monthList;
            this.po.Coin            = this._hasCoin;
            this.po.TotalRecord     = this._totalRecord;
        }
    }

    private void CheckIsNewMonth()
    {
        DateTime now    = DateTime.Now;
        int month       = now.Month;
        bool isNewMonth = true;
        for (int i = 0; i < this._monthList.Count; i++ )
        {
            if (month == this._monthList[i][0])
            {
                isNewMonth = false;
                break;
            }
        }

        if (!isNewMonth)
            return;

        for (int i = 0; i < this._monthList[1].Length; ++i )
        {
            this._monthList[2][i] = this._monthList[1][i];
        }

        for (int i = 0; i < this._monthList[0].Length; ++i)
        {
            this._monthList[1][i] = this._monthList[0][i];
        }

        for (int i = 0; i < this._monthList[0].Length; i++ )
        {
            this._monthList[0][i] = 0;
        }
        this._monthList[0][0] = month;
    }

    private void SaveJson(SettingConfigData data, string path)
    {
        string str = JsonWriter.Serialize(data);
        if (File.Exists(path))
            File.Delete(path);

        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, FileOptions.WriteThrough);
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        fs.Write(bytes, 0, bytes.Length);
        fs.Flush();
        fs.Close();
        fs.Dispose();

    }

    private  SettingConfigData LoadJson(string path)
    {
        SettingConfigData data = new SettingConfigData();
        if (!File.Exists(path))
            return null;
        string text = File.ReadAllText(path);

        if (text.Length == 0)
        {
            File.Delete(path);
            return null;
        }
        try
        {
            data = JsonReader.Deserialize<SettingConfigData>(text);
        }
        catch (Exception e)
        {
            File.Delete(path);
        }

        return data;
    }
}
