/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FSMBase.cs
 * 
 * 简    介:    状态机管理类，有限状态机系统类，使用状态机的单位（角色，和怪物续继承自该类）
 * 
 * 创建标识：   Pancake 2017/4/3 17:28:26
 * 
 * 修改描述：   Pancake 2017/4/24 加入FSM消息管理（防止类与类之间的频繁调用）
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public delegate void HandleFSMController();

public class AdvanceFSM : FSMBase
{
    #region FSM消息
    //protected Dictionary<FSMMesgType, HandleFSMController> _handDic = new Dictionary<FSMMesgType, HandleFSMController>();

    //public void RegesterHandle(FSMMesgType key, HandleFSMController handle)
    //{
    //    if (_handDic.ContainsKey(key))
    //    {
    //        Debugger.LogError("key " + key + " has been regestered");
    //        return;
    //    }

    //    _handDic.Add(key, handle);
    //}

    //public void RemoveHandle(FSMMesgType key)
    //{
    //    if (_handDic.ContainsKey(key))
    //    {
    //        _handDic.Remove(key);
    //    }
    //}

    //public void TriggerHandle(FSMMesgType key)
    //{
    //    if (_handDic.ContainsKey(key) && _handDic[key] != null)
    //    {
    //        _handDic[key]();
    //    }
    //}

    #endregion
   
    /// <summary>
    /// 存储所有状态
    /// </summary>
    private List<FSMState> _fsmStates;

    /// <summary>
    /// 当前行为ID
    /// </summary>
    private int _currentActionID;
    public int CurrentActionID
    {
        get
        {
            return _currentActionID;
        }
    }

    /// <summary>
    /// 当前状态
    /// </summary>
    private FSMState currentState;
    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public AdvanceFSM()
    {
        _fsmStates = new List<FSMState>();
    }

    /// <summary>
    /// 清空状态
    /// </summary>
    protected override void FSMOnDisable()
    {
        _fsmStates.Clear();
    }

    /// <summary>
    /// 向列表中增加状态
    /// </summary>
    /// <param name="fsmState"></param>
    public void AddFSMState(FSMState fsmState)
    {
        if (_fsmStates == null)
        {
            Debugger.Log("新加入的状态为空");
        }

        //要加入的状态是不是再列表中存在
        foreach (FSMState state in _fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debugger.Log("状态已经存在");
                return;
            }
        }

        //如果不存在则加入状态
        _fsmStates.Add(fsmState);
    }

    /// <summary>
    /// 启动状态机
    /// </summary>
    /// <param name="fsmState"></param>
    public void StartFSM(FSMState fsmState)
    {
        currentState        = fsmState;
        _currentActionID    = fsmState.ID;
    }


    /// <summary>
    /// 删除列表中的状态
    /// </summary>
    public void DeleteState(int fsmState)
    {
        foreach (FSMState state in _fsmStates)
        {
            if (state.ID == fsmState)
            {
                _fsmStates.Remove(state);
                return;
            }
        }
        Debugger.Log("当前列表中不存在这个状态");
    }


    /// <summary>
    /// 转变状态
    /// </summary>
    /// <param name="trans"></param>
    public void PerformTransition(int trans)
    {
        _canPlayNext    = false;
        //_animationEnd   = false;
        int id   = currentState.GetOutAction(trans);
        _currentActionID = id;
        foreach (FSMState state in _fsmStates) 
        {
            if (state.ID == _currentActionID) 
            {
                currentState        = state;
                break;
            }
        }
    }
}
