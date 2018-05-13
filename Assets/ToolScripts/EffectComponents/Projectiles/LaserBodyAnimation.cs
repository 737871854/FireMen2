using UnityEngine;
using System.Collections;

public class LaserBodyAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// 激光纹理动画
		Renderer laser_renderer = this.GetComponentInChildren<Renderer>();
		if (laser_renderer != null)
		{
			laser_renderer.material.SetTextureScale("_MainTex", new Vector2(_laserLength / _laserStep, 1.0f));

			float sliceTime = _laserStep / _laserSpeed;
			Vector2 texOffset = new Vector2((Time.time % sliceTime) / sliceTime, 0.0f);
			laser_renderer.material.SetTextureOffset("_MainTex", texOffset);
		}	
	}
	public void SetLaserLength(float len)
	{
		_laserLength = len;
	}
	private float _laserLength = 1.0f;

	// 一个贴图周期所覆盖的距离
	public float _laserStep = 4.0f;

	// 贴图的行进速度
	public float _laserSpeed = 1.0f;
}
