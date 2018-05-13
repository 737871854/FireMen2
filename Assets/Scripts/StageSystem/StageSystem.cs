/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   StageSystem.cs
 * 
 * 简    介:    关卡管理者
 * 
 * 创建标识：   Pancake 2018/3/5 9:21:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem
{
    // 屏幕攻击点
    private class Node
    {
        private Transform mTransform;
        private float mCoolTimer;
        private float mCoolTime = 2;
        private bool mCanUse;
        private int mLocation;

        public Node(Transform transform, int location)
        {
            mTransform = transform;
            mLocation = location;
            mCanUse = true;
        }
        public Transform transform { get { return mTransform; } }
        public int location { get { return mLocation; } }
        public bool canUse { get { return mCanUse; } }
        public void Use()
        {
            mCanUse = false;
            mCoolTimer = mCoolTime;
        }
        public void Update()
        {
            if (mCanUse) return;
            if (mCoolTimer > 0) mCoolTimer -= Time.deltaTime;
            if(mCoolTimer <= 0)
                mCanUse = true;
        }

    }
    // 当前关卡数
    private int mLv = 0;
    // 场景id
    private int mSceneID;
    // 场景名
    private string mSceneName;
    // 当前场景管理者
    private IStageHandler mRootHandler;
    private bool mIsPause;

    // 攻击点列表
    private List<List<Node>> mTargetNodeList;

    public int sceneID { get { return mSceneID; } }
    public string sceneName { get { return mSceneName; } }
    public bool isPuse { get { return mIsPause; } set { mIsPause = value; } }
    public int refreshIndex { get; set; }

    public StageSystem()
    {
        ioo.gameManager.RegisterUpdate(Update);
    }

    public void Release()
    {
        ioo.gameManager.UnregisterUpdate(Update);
    }

    /// <summary>
    /// 场景加载后调用进行初始化
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public void InitStageChain(int id, string name)
    {
        mLv = 0;
        mSceneID = id;
        mSceneName = name;
        refreshIndex = 0;      
        CreateStateHandler();
        InitNode();
    }
    // 进入下个阶段
    public void EnterNextStage()
    {
        mLv++;
        mIsPause = true;
        ioo.gameEventSystem.NotifySubject(GameEventType.NewStage, new int[] { mLv });
    }

    /// <summary>
    /// 获取当前场景当前关卡的HoldPoint
    /// </summary>
    /// <returns></returns>
    public List<Transform> GetHoldPointTran() { return ioo.battleScene.GetHoldPositionByLV(mLv); }
    
    private void Update()
    {
        if (mIsPause) return;

        UpdateNode();
        mRootHandler.Handle(mLv);
    }

    /// <summary>
    /// 更新Node状态
    /// </summary>
    private void UpdateNode()
    {
        if (mTargetNodeList == null) return;
        for (int i = 0; i < mTargetNodeList.Count; ++i)
        {
            List<Node> list = mTargetNodeList[i];
            foreach (Node node in list)
            {
                node.Update();
            }
        }
    }
    /// <summary>
    /// 获取攻击点
    /// </summary>
    /// <returns></returns>
    public Transform GetTargetTran(ref int area)
    {
        List<Player> players = ioo.playerManager.GetIsPlayingList();
        int index = 0;
        if(players.Count <= 1)
        {
            index = UnityEngine.Random.Range(0, mTargetNodeList.Count);
        }
        else
        {
            List<int> locationList = new List<int>();
            for (int i = 0; i < players.Count; ++i)
            {
                locationList.Add(players[i].location);
            }
            index = locationList[UnityEngine.Random.Range(0, locationList.Count)];
        }

        List<Node> list = mTargetNodeList[index];
        Node node = GetNode(list);
        if (node == null)
            GetTargetTran(ref area);
        else
        {
            node.Use();
            area = node.location;
            return node.transform;
        }
        return null;
    }
    /// <summary>
    /// 初始化攻击点
    /// </summary>
    private void InitNode()
    {
        GameObject cp = GameObject.Find("CameraParent/End");
        if (cp == null) return;

        mTargetNodeList = new List<List<Node>>();
        Transform[] childs0 = cp.transform.Find("Left").GetComponentsInChildren<Transform>();
        List<Node> list0 = new List<Node>();
        for (int i = 1; i < childs0.Length; ++i)
        {
            list0.Add(new Node(childs0[i], 0));
        }
        mTargetNodeList.Add(list0);

        Transform[] childs1 = cp.transform.Find("Midle").GetComponentsInChildren<Transform>();
        List<Node> list1 = new List<Node>();
        for (int i = 1; i < childs1.Length; ++i)
        {
            list1.Add(new Node(childs1[i], 1));
        }
        mTargetNodeList.Add(list1);

        Transform[] childs2 = cp.transform.Find("Right").GetComponentsInChildren<Transform>();
        List<Node> list2 = new List<Node>();
        for (int i = 1; i < childs2.Length; ++i)
        {
            list2.Add(new Node(childs2[i], 2));
        }
        mTargetNodeList.Add(list2);
    }
    /// <summary>
    /// 初始化通关条件
    /// </summary>
    private void CreateStateHandler()
    {
        mIsPause = true;
        if (mSceneID == 1)
        {
            mIsPause = false;
            mRootHandler = new SelectStageHandler(this, 0);
            return;
        }

        int lv = 0;
        LevelPO levelPO = LevelData.Instance.GetLevelPO(mSceneID * 100 + Define.GAME_CONFIG_DIFFICULTY);
        if (levelPO == null) return;
        List<IStageHandler> list = new List<IStageHandler>();
        switch (mSceneID)
        {
            case 2:
                for (int i = 0; i < levelPO.CheckPointScores.Length; ++i)
                {
                    IStageHandler handle = new TownStageHandler(this, lv++);
                    list.Add(handle);
                }
                break;
            case 4:
                for (int i = 0; i < levelPO.CheckPointScores.Length; ++i)
                {
                    IStageHandler handle = new BullDemonKingStageHandler(this, lv++);
                    list.Add(handle);
                }
                break;
            case 5:
                for(int i = 0; i < levelPO.CheckPointScores.Length;++i)
                {
                    IStageHandler handle = new FarmStageHandler(this, lv++);
                    list.Add(handle);
                }
                break;
            case 6:
                break;
        }

        for (int i = 0; i < list.Count - 1; ++i)
            list[i].SetNextHandler(list[i + 1]);
        mRootHandler = list[0];
    }
    /// <summary>
    /// 选取一个攻击点
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private Node GetNode(List<Node> list)
    {
        List<Node> canUseList = new List<Node>();
        foreach (Node node in list)
        {
            if (node.canUse)
                canUseList.Add(node);
        }
        if (canUseList.Count == 0) return null;
        int index = UnityEngine.Random.Range(0, canUseList.Count);
        return canUseList[index];
    }
}