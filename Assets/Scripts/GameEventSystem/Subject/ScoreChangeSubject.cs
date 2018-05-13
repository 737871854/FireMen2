/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ScoreChangeSubject.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/8 9:58:10
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class ScoreChangeSubject : IGameEventSubject
{
    private int[] mPlayerScore = new int[Define.MAX_PLAYER_NUMBER];

    public int[] playerScore { get { return mPlayerScore; } }

    public override void Notify(params int[] args)
    {
        int playerID = args[0];
        int worth = args[2];
        AddScore(playerID, worth);
        base.Notify();
    }

    private void AddScore(int id, int worth)
    {
        mPlayerScore[id] += worth;
    }
}