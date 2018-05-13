/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BaseSettingData.cs
 * 
 * 简    介:    BaseSettingData后台存储数据信息
 * 
 * 创建标识：  
 * 
 * 修改描述：    Pancake 2016/7/28 14:11:21
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SettingConfigData
{
    // 校验ID
    public string CheckId { get; set; }

    // 0,1,2,3,4,5,6币率
    public int GameRate { get; set; }

    // 0代表中文，1代表英文。
    public int GameLanguage { get; set; }

    // 0 代表模式1,1 代表模式2
    public int TicketModel { get; set; }

    // 当前音量，分为10个等级
    public int GameVolume { get; set; }

    public int GameLevel { get; set; }

    // 月份信息
    public List<float[]> MonthList = new List<float[]>();

    // 总记录 游戏次数，游戏时间，开机时间，总币数，总出票数
    public float[] TotalRecord { get; set; }

    // 玩家剩余币数
    public int[] Coin { get; set; }

    public SettingConfigData()
    {
        this.CheckId = string.Empty;
        this.GameRate = 3;
        this.GameLanguage = 0;
        this.GameLevel = 0;
        this.TicketModel = 1;
        this.GameVolume = 5;
        for (int i = 0; i < 3; i++ )
        {
            this.MonthList.Add(new float[5]);
        }

        this.TotalRecord = new float[5];
        this.Coin = new int[3];
    }

}
