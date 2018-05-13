/********************************************************************
	Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,广州擎天柱网络科技有限公司

	All rights reserved.

	文件名称：EffectInfo.cs
	简	述：特效的一些信息，目前有挂点信息，挂在那个对象上，生命时长。
	创建标识：廖凯 2015/08/26
*********************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class EffectInfo : MonoBehaviour {
	public enum Joints {
		None,
		gua_fire,
		gua_Spine,
	}

	public enum Positions {
		None,
		Person,
		Position,
        AtAttackPosition,
        AtTargetPosition,
	}

	public enum EffectType
	{
		None,
		Hero,
		Soilder,
	}

    public enum TraceType
    {
        None,
        TraceTarget, // 追踪目标
        XZEqualTarget, // xz坐标总是等于target的xz坐标
    }

	static public readonly float InfLeftTime = 1;
	public Joints _joint = Joints.None;
	public Positions _pointion = Positions.None;
	public EffectType _type = EffectType.None;
    public TraceType _trace = TraceType.None; // 追踪方式
	public float _leftTime = InfLeftTime; // 生存时间
    public float _startHitTime = 0; // hit开始的时间
    public float _endHitTime = 0; // 受击结束后特效持续时间
    public float _bombRadius = 1; // 半径
    public Vector3 _offsetPosition = Vector3.zero; // 特效的偏移位置
}