/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PlayerManager.cs
 * 
 * 简    介:    管理玩家
 * 
 * 创建标识：   Pancake 2017/4/27 11:43:50
 * 
 * 修改描述：
 * 
 */

using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// 玩家列表
    /// </summary>
    private List<Player> mPlayerList = new List<Player>();

    /// <summary>
    /// 最大玩家数量
    /// </summary>
    private int mPlyerCount = Define.MAX_PLAYER_NUMBER;

    private List<int> mHeadList;

    #region Unity Call Back
    void Awake()
    {
        ioo.gameManager.RegisterUpdate(UpdatePreFrame);
        ioo.gameManager.RegisterFixedUpdate(UpdateFixedFrame);

        EventDispatcher.AddEventListener<int, int>(EventDefine.Event_Player_Select_Character, ListeningSelecCharacter);

        ioo.gameEventSystem.RegisterObserver(GameEventType.ScoreChange, new ScoreChangeObserverPlayerManager(this));
        ioo.gameEventSystem.RegisterObserver(GameEventType.PlayerOnDamage, new PlayerOnHurtObserverPlayerManager(this));
    }

    /// <summary>
    /// 创建玩家
    /// </summary>
    void Start()
    {
        mHeadList = new List<int>() { 0, 1, 2 };

        for (int i = 0; i < mPlyerCount; ++i )
        {
            Player player = new Player();
            player.Init(i);
            mPlayerList.Add(player);
        }
    }

    void Destroy()
    {
        ioo.gameManager.UnregisterUpdate(UpdatePreFrame);
        ioo.gameManager.UnregisterFixedUpdate(UpdateFixedFrame);

        EventDispatcher.RemoveEventListener<int, int>(EventDefine.Event_Player_Select_Character, ListeningSelecCharacter);

    }
    #endregion

    #region Public Function
    /// <summary>
    /// 选中图像操作是否已经完成
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HasHead(int id) { return mPlayerList[id].data.head < 0 ? false : true; }
    public void SetWaterPosition(int id, Vector3 pos) { mPlayerList[id].position = pos; }
    public float HPProgress(int id) { return mPlayerList[id].HPProgress; }
    public int Score(int id) { return mPlayerList[id].Score; }
    public int ContinueTime(int id) { return mPlayerList[id].continueTime; }
    public int playerCount { get { return mPlayerList.Count; } }
    public Vector3 Position(int id) { return mPlayerList[id].position; }
    public void AddCoin(int id) { mPlayerList[id].AddCoin(); }

    /// <summary>
    /// 响应怪物伤害
    /// </summary>
    /// <param name="damagedID"></param>
    /// <param name="damageValue"></param>
    public void OnDamage(int damagedID,int damageValue)
    {
        List<Player> list = GetIsPlayingList();
        if (list.Count == 0) return;

        if (damagedID < 0 || damagedID >= mPlayerList.Count)
        {
            for(int i = 0; i < list.Count; ++i)
                list[i].OnDamage(damageValue);
            return;
        }
        else if (list.Count == 1) list[0].OnDamage(damageValue);
        else if (list.Count == 2)
        {
            for(int i = 0; i < list.Count;++i)
            {
                if (list[i].id == damagedID)
                    list[i].OnDamage(damageValue);
            }
        }
        else
            mPlayerList[damagedID].OnDamage(damageValue);
    }

    /// <summary>
    /// 重置所有玩家数据
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < mPlayerList.Count; ++i )
        {
            mPlayerList[i].Reset();
        }
    }

    public void UpdateScores(int[] scores)
    {
        for (int i = 0; i < scores.Length; ++i)
        {
            mPlayerList[i].UpdateScore(scores[i]);
        }
    }

    public int GetTotalSoce
    {
        get
        {
            int ret = 0;
            for(int i = 0; i < playerCount; ++i)
            {
                ret += mPlayerList[i].Score;
            }
            return ret;
        }
    }

    /// <summary>
    /// 可刷水炮数量
    /// </summary>
    /// <returns></returns>
    public int CanDropSupperWaterNum()
    {
        int count = 0;
        int num = 0;
        for (int i = 0; i < Define.MAX_PLAYER_NUMBER; ++i)
        {
            if (mPlayerList[i].IsPlaying())
                ++count;
            if (mPlayerList[i].IsSupperWater())
                ++num;
        }

        count -= num;

        return count;
    }

    /// <summary>
    /// 给指定玩家加水
    /// </summary>
    /// <param name="index"></param>
    /// <param name="water"></param>
    public void FillWater(int index, int water)
    {
        mPlayerList[index].FillWater(water);
    }

    /// <summary>
    /// 玩家是否在玩
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsPlaying(int id)
    {
        if (mPlayerList.Count > id)
        {
            if (mPlayerList[id].IsPlaying())
                return true;
        }

        return false;
    }

    /// <summary>
    /// 续币倒计时
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsContinue(int id)
    {
        if (mPlayerList.Count > id)
        {
            if (mPlayerList[id].IsContinue())
                return true;
        }

        return false;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsDead(int id)
    {
        if (mPlayerList.Count > id)
        {
            if (mPlayerList[id].IsDead())
                return true;
        }

        return false;
    }

    /// <summary>
    /// 玩游戏，消耗币数
    /// </summary>
    /// <param name="id"></param>
    public void UseCoin(int id)
    {
        Player player = GetPlayer(id);
        player.UseCoin();
    }

    /// <summary>
    /// 获取指定玩家
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Player GetPlayer(int index)
    {
        if (index > mPlayerList.Count)
        {
            Debug.LogError("The player of " + index + " is not exit");
            return null;
        }
        return mPlayerList[index];
    }

    /// <summary>
    /// 结束角色选择
    /// </summary>
    public void CharacterEnd()
    {
        for (int i = 0; i < mPlyerCount; ++i)
        {
            if (IsPlaying(i) && !HasHead(i))
            {
                int head = mHeadList[Random.Range(0, mHeadList.Count)];
                mHeadList.Remove(head);
                mPlayerList[i].SetHead(head);
            }
        }
    }

    /// <summary>
    /// 获取当前正在玩的玩家数量
    /// </summary>
    /// <returns></returns>
    public List<Player> GetIsPlayingList()
    {
        List<Player> list = new List<Player>();
        for (int i = 0; i < mPlayerList.Count; ++i)
        {
            if (mPlayerList[i].IsPlaying() || mPlayerList[i].IsHold())
                list.Add(mPlayerList[i]);
        }
        return list;
    }
    #endregion

    #region Private Function
    /// <summary>
    /// 帧更新
    /// </summary>
    private void UpdatePreFrame()
    {
        //for (int i = 0; i < mPlayerList.Count; ++i )
        //{
        //    mPlayerList[i].UpdatePreFrame();
        //}
    }

    /// <summary>
    /// 固定帧更新
    /// </summary>
    private void UpdateFixedFrame()
    {
        if (ioo.gameMode.State != E_GameState.Play)
            return;
        bool gameOver = true;
        for (int i = 0; i < mPlayerList.Count; ++i)
        {
            mPlayerList[i].UpdateFexedFrame();
            if (mPlayerList[i].IsPlaying() || mPlayerList[i].IsContinue() || mPlayerList[i].IsHold())
                gameOver = false;
        }

        // 应该先进入结算阶段，这里测试用
        if (gameOver)
        {
            EventDispatcher.TriggerEvent(EventDefine.Event_Game_Defeat);
        }
    }

    /// <summary>
    /// ID为id的玩家，选择了index号角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
    private void ListeningSelecCharacter(int id, int index)
    {
        GetPlayer(id).SetHead(index);
        mHeadList.Remove(index);

        for (int i = 0; i < mPlyerCount; ++i )
        {
            if (IsPlaying(i) && !HasHead(i))
                return;
        }

        ioo.gameMode.RunState(E_GameState.SelectMap);      
    }

  
    #endregion
}
