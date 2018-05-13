/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FSMState.cs
 * 
 * 简    介:    状态共性 FSM基类
 * 
 * 创建标识：   Pancake 2017/4/2 15:01:43
 * 
 * 修改描述：   将每局类型转为int类型，目的便于拓展  Pancake 2014/4/24
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMState
{
    public struct Info
    {
        public string name;
        public int id;
    }
    protected Info info;

    //存储转换过程
    protected Dictionary<int, int> map = new Dictionary<int, int>();
    //当前所处的行为
    protected int actionID;
    public int ID
    {
        get
        {
            return actionID;
        }
    }

    /// <summary>
    /// 目标
    /// </summary>
    protected Transform destTran;
    /// <summary>
    /// 目标点
    /// </summary>
    protected Vector3 destPos = Vector3.zero;
    /// <summary>
    /// 所有巡逻点
    /// </summary>
    protected Vector3[] wayPoints;
     

    ///// <summary>
    ///// 选择速度
    ///// </summary>
    //protected float curRotSpeed;
    ///// <summary>
    ///// 移动速度
    ///// </summary>
    //protected float curSpeed;

    //距离限定
    protected float chaseDistance   = 40;
    protected float attackDistance  = 0.05f;
    protected float arriveDistance  = 3;

    protected float animatorSpeed;

    public AdvanceFSM fsm;

    protected Animation animation;
    protected Animator animator;

    /// <summary>
    /// 向字典中添加状态
    /// </summary>
    public void AddTransition(int trans, int actionID)
    {
        //如果包含就停止
        if (map.ContainsKey(trans))
        {
            Debug.LogError("State " + actionID + " is already has transition " + trans);
            return;
        }

        map.Add(trans, actionID);
    }

    /// <summary>
    /// 删除字典中指定的状态
    /// </summary>
    public void DeletTransition(int trans)
    {
        map.Remove(trans);
    }

    /// <summary>
    /// 根据传递的转换条件，判断是否可以发生转换
    /// </summary>
    /// <param name="trans"></param>
    public int GetOutAction(int trans)
    {
        if (map.ContainsKey(trans))
            return map[trans];

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    public void FindNextPoint()
    {
        int rndIndex            = Random.Range(0, wayPoints.Length);
        Vector3 rndPos          = wayPoints[rndIndex];
        Vector3 rndPosition     = new Vector3(Random.Range(0, 0.5f), 0, Random.Range(0, 0.5f));
        destPos                 = rndPos + rndPosition;
    }

    public bool FindNextPoint(int index)
    {
        if (index < wayPoints.Length - 1)
        {
            destPos = wayPoints[index + 1];
            return false;
        }
        else
            return true;
    }

    //抽象状态转换的原因
    public abstract void Reason(Transform player, Transform npc);

    //抽象转换的行为
    public abstract void Act(Transform player, Transform npc);
}

