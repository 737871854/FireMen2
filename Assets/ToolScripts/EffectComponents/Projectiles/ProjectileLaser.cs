using UnityEngine;
using System.Collections;

public class ProjectileLaser : ProjectileBase
{

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
/*        if (_skillFinished) {
            return;
        }
        effectBeat -= FightTime.deltaTime;
        if (effectBeat < 0f && !_isFinished) {
            Destroy();
            _skillFinished = true;
        }*/
	}

	// 全流程是否结束
	public override bool IsFinished()
	{
        return _skillFinished;
	}

	// 设置开始位置
	public override void SetStartPosition(Vector3 start_pos)
	{
		_startPos = start_pos;
	}

	// 设置结束位置并启动流程
	public override void SetEndPosition(Vector3 end_pos)
	{
        if (_skillFinished) {
            return;
        }

		_endPos = end_pos;

		if (_isFinished)
			Destroy();

		if ((_laserHeadPrefab != null) && (_laserBodyPrefab != null))
		{
			Vector3 vec_laser = end_pos - _startPos;
			float laser_length = vec_laser.magnitude;
			Vector3 vec_dir = vec_laser.normalized;
			

			// 建立激光束
            if (_laserBodyObject == null) {
                _laserHeadObject = GameObject.Instantiate(_laserHeadPrefab, _startPos, Quaternion.LookRotation(vec_dir)) as GameObject;

				excuteShader(_laserHeadObject);
				_laserBodyObject = GameObject.Instantiate(_laserBodyPrefab, (_startPos + _endPos) * 0.5f, Quaternion.identity) as GameObject;
				excuteShader(_laserBodyObject);
				_laserBodyObject.transform.localScale = new Vector3(1.0f, 1.0f, laser_length);

				// 面朝摄像机
				Vector3 camDir = Camera.main.transform.position - _laserBodyObject.transform.position;
				_laserBodyObject.transform.LookAt(_startPos, camDir);

                // 建立击中效果
				Vector3 cam_pos = Camera.main.transform.position;
				Vector3 camera_offset = (cam_pos - end_pos) * 0.1f;
                _laserHitObject = GameObject.Instantiate(_laserHitPrefab, end_pos + camera_offset, Quaternion.identity) as GameObject;
                excuteShader(_laserHitObject);
                for (int i = 0; i < _laserHitObject.transform.childCount; ++i) {
                    GameObject sub_particle_obj = _laserHitObject.transform.GetChild(i).gameObject;
                    GameObject prefab_sub_particle_obj = _laserHitPrefab.transform.GetChild(i).gameObject;

                    ParticleSystem sub_particle_sys = sub_particle_obj.GetComponent<ParticleSystem>();
                    ParticleSystem prefab_sub_particle_sys = prefab_sub_particle_obj.GetComponent<ParticleSystem>();

                    if ((sub_particle_sys != null) && (prefab_sub_particle_sys != null))
                        sub_particle_sys.startSize = prefab_sub_particle_sys.startSize * _laserHitScale;
                }
            }
			else 
			{
                _laserHeadObject.transform.position = _startPos;
                _laserHeadObject.transform.rotation = Quaternion.identity;

                _laserBodyObject.transform.position = (_startPos + _endPos) * 0.5f;
				
				// 面朝摄像机
				Vector3 camDir = Camera.main.transform.position - _laserBodyObject.transform.position;
				_laserBodyObject.transform.LookAt(_startPos, camDir);
				_laserBodyObject.transform.localScale = new Vector3(1.0f, 1.0f, laser_length);

				Vector3 cam_pos = Camera.main.transform.position;
				Vector3 camera_offset = (cam_pos - end_pos) * 0.1f;
				_laserHitObject.transform.position = end_pos + camera_offset;
            }

			// 设置纹理动画参数
			LaserBodyAnimation laserAni = _laserBodyObject.GetComponent<LaserBodyAnimation>();
			if (laserAni != null)
				laserAni.SetLaserLength(laser_length);

			_isFinished = false;
		}
	}

	public void SetHitScale(float scale)
	{
		_laserHitScale = scale;
		for (int i = 0; i < _laserHitObject.transform.childCount; ++i)
		{
			GameObject sub_particle_obj = _laserHitObject.transform.GetChild(i).gameObject;
			GameObject prefab_sub_particle_obj = _laserHitPrefab.transform.GetChild(i).gameObject;

			ParticleSystem sub_particle_sys = sub_particle_obj.GetComponent<ParticleSystem>();
			ParticleSystem prefab_sub_particle_sys = prefab_sub_particle_obj.GetComponent<ParticleSystem>();

			if ((sub_particle_sys != null) && (prefab_sub_particle_sys != null))
				sub_particle_sys.startSize = prefab_sub_particle_sys.startSize * _laserHitScale;
		}
	}

	// 设置速度
	public override void SetSpeed(float speed)
	{
	}

	// 销毁投射物
	public override void Destroy()
	{
		if (_laserHeadObject != null)
			GameObject.Destroy(_laserHeadObject);

		if (_laserBodyObject != null)
			GameObject.Destroy(_laserBodyObject);

		if (_laserHitObject != null)
			GameObject.Destroy(_laserHitObject);

		_isFinished = true;
        isHitPoint = true;
	}

	// 起始坐标
	protected Vector3 _startPos = new Vector3(0, 0, 0);
	
	// 终止坐标
	protected Vector3 _endPos = new Vector3(0, 0, 0);

	// 击中粒子缩放值
	protected float _laserHitScale = 2.0f;

	// 激光显示效果
	public GameObject _laserHeadPrefab = null;
	public GameObject _laserBodyPrefab = null;

	// 激光显示对象
	protected GameObject _laserHeadObject = null;
	protected GameObject _laserBodyObject = null;

	// 激光击中效果
	public GameObject _laserHitPrefab = null;

	// 激光击中对象
	protected GameObject _laserHitObject = null;

	// 显示是否结束
	protected bool _isFinished = true;

    protected bool _skillFinished = false;
}
