/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UIShake.cs
 * 简    述：Unity ui 抖动 模板1
 * 创建标识：Fanki  2015/11/03
 * 
 * 修改标识：
*/

using UnityEngine;

public class UIShake : MonoBehaviour
{
	#region 公共属性
	public GameObject m_shakeObj;
	public float m_speed = 300;
	public float m_angle = 50;
	public float m_standardAngle = 0;
	public int m_times = 3;

	public float m_delayTimes = 0.3f;
	public int m_totalTimes = 2;

	public float m_foreverDelayTime = 5;

	public bool m_isFirstDelay = false;
	public bool m_isForever = false;

	public bool m_isPlay = false;
	#endregion //公共属性

	#region 私有属性
	private int _times = 0;
	private float _currentAngle = 0;
	private bool _isFinish = false;
	private bool _isTurn = false;

	private float _delayTimes = 0;
	private int _totalTimes = 0;

	private float _foreverDelayTime = 0;

	private bool _isFirstDelay = false;
	private bool _isForever = false;
	private bool _isForeverFinish = false;
	#endregion //私有属性

	/// <summary>
	/// 抖动刷新
	/// </summary>
	void Update()
	{
		if (m_isPlay)
		{
			PlayTolal();
			m_isPlay = false;
		}

		if (_isFirstDelay && _delayTimes > 0)
		{
			_delayTimes -= Time.deltaTime;
			return;
		}

		if (_isForeverFinish && _foreverDelayTime > 0)
		{
			_foreverDelayTime -= Time.deltaTime;
			return;
		}

		if (_isFinish || m_shakeObj == null) return;

		_currentAngle += m_speed * Time.deltaTime;


		if (_currentAngle > m_standardAngle + m_angle / 2)
		{
			m_speed = -Mathf.Abs(m_speed);
			if (_isTurn && _currentAngle > m_standardAngle)
			{
				_times -= 1;
				_isTurn = false;
			}
		}
		if (_currentAngle < m_standardAngle - m_angle / 2)
		{
			m_speed = Mathf.Abs(m_speed);
			_isTurn = true;
		}

		//m_shakeObj.transform.Rotate(0, 0, m_speed * Time.deltaTime, Space.Self);
		m_shakeObj.transform.rotation = Quaternion.Euler(0, 0, _currentAngle);

		if (_times <= 0) OnFinish();
	}

	/// <summary>
	/// 完成检测
	/// </summary>
	void OnFinish()
	{
		m_shakeObj.transform.rotation = Quaternion.Euler(0, 0, 0);
		_isFinish = true;
		_isFirstDelay = true;
		_delayTimes = m_delayTimes;

		if (_isForever || _totalTimes > 1)
		{
			_totalTimes -= 1;
			if (_isForever && _totalTimes <= 0)
			{
				_totalTimes = m_totalTimes;
				_foreverDelayTime = m_foreverDelayTime;
				_isForeverFinish = true;
			}
			Play();
		}
	}

	#region 测试代码
	/// <summary>
	/// 测试代码
	/// </summary>
	void OnGUI()
	{
		if (GUILayout.Button("Play"))
		{
			PlayTolal();
		}
	}
	#endregion //测试代码

	/// <summary>
	/// 播放单词
	/// </summary>
	public void Play ()
	{
		_times = m_times;
		_isTurn = false;
		_isFinish = false;
	}

	/// <summary>
	/// 播放多次
	/// </summary>
	public void PlayTolal()
	{
		_isFirstDelay = m_isFirstDelay;
		_delayTimes = m_delayTimes;
		_totalTimes = m_totalTimes;
		_foreverDelayTime = m_foreverDelayTime;
		_isForever = m_isForever;
		_isForeverFinish = false;

		Play();
	}
}
