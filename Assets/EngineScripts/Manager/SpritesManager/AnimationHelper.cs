/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   AnimationHelper.cs
 * 
 * 简    介:    辅助Lua播放序列帧动画 
 * 
 * 创建标识：   Pancake 2017/11/18 18:03:02
 * 
 * 修改描述：   经测试，功能已经实现(暂时不用，原因是每次使用后内存不能被回收，原因待查)
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationHelper : MonoBehaviour
{
    /// <summary>
    /// 当前是，未考虑动画需要切换情况，后续如有功能需要，在拓展
    /// </summary>
    public class M_Animation
    {
        public M_Animation(Image image, List<Sprite> List, int speed, int flag)
        {
            m_image = image;
            m_spritList = List;
            m_speed = speed;

            m_index = 0;
            m_time = 1.0f / m_speed;

            m_loop = flag == 1 ? true : false;
        }

        /// <summary>
        /// 播放动画的组件
        /// </summary>
        public Image m_image;
        /// <summary>
        /// 序列帧
        /// </summary>
        public List<Sprite> m_spritList;
        /// <summary>
        /// 每秒播放帧数
        /// </summary>
        public int m_speed;
        /// <summary>
        /// 当前播第几帧
        /// </summary>
        public int m_index;
        /// <summary>
        /// 播放间隔时间
        /// </summary>
        public float m_time;
        /// <summary>
        /// 从上帧播放完毕到当前时间
        /// </summary>
        public float m_curTime;

        /// <summary>
        /// 循环播放
        /// </summary>
        private bool m_loop;
        /// <summary>
        /// 改动画是否需要播放
        /// </summary>
        private bool m_needPlay;
        public void PlayAnimation()
        {
            if (!m_image.gameObject.activeInHierarchy)
            {
                m_index = 0;
                m_needPlay = false;
            }
            else
                m_needPlay = true;

            if (!m_needPlay)
                return;

            m_curTime += Time.deltaTime;
            if (m_curTime < m_time)
                return;

            ++m_index;
            m_index %= m_spritList.Count;
            if (m_index == 0)
            {
                if (!m_loop)
                {
                    ioo.animationHelper.RemoveAnimation(m_image);
                    return;
                }
            }
            m_image.sprite = m_spritList[m_index];
            m_curTime = 0;
            return;
        }
    }

    private List<M_Animation> _animList;

    void Awake()
    {
        ioo.gameManager.RegisterUpdate(UpdatePreFrame);
    }

    void Deatroy()
    {
        ioo.gameManager.UnregisterUpdate(UpdatePreFrame);
    }

    private void UpdatePreFrame()
    {
        if (_animList == null)
            return;

        for (int i = 0; i < _animList.Count; ++i)
        {
            M_Animation anim = _animList[i];
            anim.PlayAnimation();
        }
    }

    #region Public Function
    /// <summary>
    /// 在指定的图片上播放序列帧
    /// </summary>
    /// <param name="image"></param>
    /// <param name="list"></param>
    public void AddAnimation(Image image, List<Sprite> list, int speed = 3, int flag = 1)
    {
        if (_animList == null)
            _animList = new List<M_Animation>();

        if (list == null)
        {
            Debugger.LogError("List<Sprite> is null, please check!");
            return;
        }

        M_Animation anim = new M_Animation(image, list, speed, flag);
        for (int i = 0; i < _animList.Count; ++i)
        {
            if (_animList[i].m_image == image)
            {
                if (_animList[i].m_spritList == list)
                    return;
                _animList[i] = anim;
                return;
            }
        }

        image.sprite = list[0];

        _animList.Add(anim);
    }

    /// <summary>
    /// /移除制定的动画信息
    /// </summary>
    /// <param name="image"></param>
    public void RemoveAnimation(Image image)
    {
        if (_animList == null)
            return;

        M_Animation anim = null;
        for (int i = 0; i < _animList.Count; ++i)
        {
            if (_animList[i].m_image != image)
                continue;
            anim = _animList[i];
            break;
        }

        if (anim == null)
            return;

        _animList.Remove(anim);

    }
    #endregion
}
