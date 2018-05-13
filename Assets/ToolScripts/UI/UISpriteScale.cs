/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UISpriteScale.cs
 * 简    述：Unity ui 放大缩小
 * 创建标识：Fanki  2015/11/03
 * 
 * 修改标识：
*/

using UnityEngine;

public class UISpriteScale : MonoBehaviour
{
	#region 公共属性
	public GameObject m_Obj;
	public float m_speedX = 0.015f;
	public float m_speedY = 0.015f;
	public float m_maxScale = 1f;
	public float m_minScale = 0.85f;
	public int m_times = 3;
	public bool m_isPlay = false;
	public bool m_isForever = false;
	#endregion //公共属性

	#region 公共属性
	private int _times;
	private float _scaleX;
	private float _scaleY;
	private bool _isFinish;
	#endregion //公共属性

	#region 私有方法
	/// <summary>
	/// 刷新
	/// </summary>
	void Update()
	{
		if (m_isPlay)
		{
			m_isPlay = false;
			Play();
		}

		if (m_Obj == null || _isFinish) return;

		OnUpdateSpeed(ref _scaleX, ref m_speedX);
		OnUpdateSpeed(ref _scaleY, ref m_speedY);
		CheckFinish(_scaleX);

		if (_times <= 0)
			OnFinish();
		else
			SetSpriteScale(_scaleX, _scaleY);
	}

	/// <summary>
	/// 更新速度
	/// </summary>
	/// <param name="scale"></param>
	/// <param name="speed"></param>
	void OnUpdateSpeed(ref float scale, ref float speed)
	{
		scale -= speed;
		if (scale > m_maxScale)
			speed = Mathf.Abs(speed);
		if (scale < m_minScale)
			speed = -Mathf.Abs(speed);
	}

	/// <summary>
	/// 检测完成一次
	/// </summary>
	/// <param name="scale"></param>
	void CheckFinish(float scale)
	{
		if (scale > 1)
			_times -= 1;
	}

	/// <summary>
	/// 完成所有的
	/// </summary>
	void OnFinish()
	{
		SetSpriteScale(1, 1);
		_isFinish = true;

		if (m_isForever)
			Replay();
	}

	/// <summary>
	/// 重播
	/// </summary>
	void Replay()
	{
		Play();
	}

	/// <summary>
	/// 设置大小
	/// </summary>
	/// <param name="scaleX"></param>
	/// <param name="scaleY"></param>
	void SetSpriteScale(float scaleX, float scaleY)
	{
		_scaleX = scaleX;
		_scaleY = scaleY;
		m_Obj.transform.localScale = new Vector3(scaleX, scaleY, 1);
	}
	#endregion //私有方法

	#region 公共方法
	/// <summary>
	/// 播放
	/// </summary>
	public void Play()
	{
		_times = m_times;
		_isFinish = false;
	}

	/// <summary>
	/// 按次数表
	/// </summary>
	/// <param name="times"></param>
	public void Play(int times)
	{
		if (times < 1)
			m_isForever = true;

		m_times = times;
		_times = m_times;
		_isFinish = false;
	}

	/// <summary>
	/// 停止
	/// </summary>
	public void Stop()
	{
		_times = 1;
		m_isForever = false;
	}
	#endregion //公共方法
}
