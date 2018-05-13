/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UISpriteFlash.cs
 * 简    述：Unity ui 闪闪闪 改变透明度
 * 创建标识：Fanki  2015/11/03
 * 
 * 修改标识：
*/

using UnityEngine;
using UnityEngine.UI;

public class UISpriteFlash : MonoBehaviour
{
	#region 公共属性
	public GameObject m_flashObj;
	public float m_speed = 0.05f;
	public int m_times = 7;
	public bool m_isPlay = false;

	public float m_maxOpacity = 1f;
	public float m_minOpacity = 0.1f;
	#endregion //公共属性

	#region 公共属性
	private Image _image;
	private int _times;
	private float _apha;
	private bool _isFinish;
	#endregion //公共属性

	void Update()
	{
		if (m_isPlay)
		{
			m_isPlay = false;
			Play();
		}

		if (_image == null || _isFinish) return;

		_apha -= m_speed;
		if (_apha > m_maxOpacity)
		{
			m_speed = Mathf.Abs(m_speed);
			_times -= 1;
		}
		if (_apha < m_minOpacity)
			m_speed = -Mathf.Abs(m_speed);

		if (_times <= 0)
			OnFinish();
		else
			SetColorApha(_apha);
	}

	void OnFinish()
	{
		SetColorApha(1);
		_isFinish = true;
	}

	void SetColorApha(float apha)
	{
		Color upColor = _image.color;
		upColor.a = apha;
		_image.color = upColor;
	}

	public void Play()
	{
		if (m_flashObj != null)
			_image = m_flashObj.GetComponent<Image>();

		_apha = 1;
		_times = m_times;
		_isFinish = false;
	}
}
