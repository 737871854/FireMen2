/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearStep.cs
 * 
 * 简    介:    熊Boss阶段管理
 * 
 * 创建标识：   Pancake 2017/12/29 9:52:23
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class Step
{
    public class BearSkill
    {
        public BearSkill(E_BearSkill _skill, float _time = -1)
        {
            skill = _skill;
            time = _time;
            coolTime = _time;
        }
        /// <summary>
        /// 技能对应转换条件
        /// </summary>
        public E_BearSkill skill;
        /// <summary>
        /// 技能冷却时间
        /// </summary>
        public float time;

        private float coolTime;
        public void Reset()
        {
            time = coolTime;
        }
    }

    public class PterosaurSkill
    {
        public PterosaurSkill(E_PterosaurState _skill, float _time = -1)
        {
            skill = _skill;
            time = _time;
            coolTime = _time;
        }
        /// <summary>
        /// 技能对应转换条件
        /// </summary>
        public E_PterosaurState skill;
        /// <summary>
        /// 技能冷却时间
        /// </summary>
        public float time;

        private float coolTime;
        public void Reset()
        {
            time = coolTime;
        }
    }

    public class A_Info
    {
        public string name;
        public int id;
    }

    protected A_Info a_info = new A_Info();

    protected Animator animator;

    protected int curSkillIndex;
    protected List<BearSkill> bskList = new List<BearSkill>();
    protected List<PterosaurSkill> pskList = new List<PterosaurSkill>();
    protected int skillFlag;

    protected bool canUpdateSkill;

    protected PterosaurBehaviour pterosaurBehaviour;
    protected E_BearState bState = E_BearState.UnKnow;
    protected E_PterosaurState pState = E_PterosaurState.UnKnow;
    protected E_BearStep bearStep;
    protected E_PterosaurStep pterosaurStep;
    public E_BearStep BearStep { get { return bearStep; } }
    public E_PterosaurStep PterosaurStep { get { return pterosaurStep; } }

    public virtual void RunStep() { }
    public virtual void UpdateStep() { }
}