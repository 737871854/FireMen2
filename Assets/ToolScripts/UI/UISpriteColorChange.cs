/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UISpriteColorChange.cs
 * 简    述：Unity ui 更改颜色
 * 创建标识：Fanki  2015/11/03
 * 
 * 修改标识：
*/

using UnityEngine;
using UnityEngine.UI;

public class UISpriteColorChange : MonoBehaviour
{
	#region 公共成员
	public Color NormalColor = Color.white;
	public Color HighLightedColor = Color.white;
	public Color DisableColor = Color.white;
	#endregion // 公共成员

	#region 私有成员
	private Image _image;
	#endregion // 私有成员

	/// <summary>
	/// 设置颜色
	/// </summary>
	/// <param name="state"></param>
	public void SetStatus(UISpriteState state)
	{
		if (_image == null)
		{
			_image = gameObject.GetComponent<Image>();
			if (_image == null) { Debugger.LogError("UISpriteSwap need Image Component"); }
		}

		switch (state)
		{
			case UISpriteState.Normal:
				if (DisableColor != null) _image.color = NormalColor;
				break;
			case UISpriteState.HighLighted:
				if (DisableColor != null) _image.color = HighLightedColor;
				break;
			case UISpriteState.Disable:
				if (DisableColor != null) _image.color = DisableColor;
				break;
		}
	}
}
