using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Trajectory : MonoBehaviour {
    public delegate void CallBack(object arg);

    private class CallBackPackage {
        public CallBack fun = null;
        public object arg = null;
    }
	// Use this for initialization
    public virtual void Start() {
        _costTime = 0;
        _isRunning = true;
		if (startObj != null) {
			startPosition = startObj.transform.position;
		}
		if (targetObj != null) {
			targetPosition = targetObj.transform.position;
		}
        _direction = targetPosition - startPosition;

		transform.position = startPosition;
		_lastPosition = startPosition;

        //先绕局部坐标z转倾斜，再转到世界坐标
        Quaternion localzRotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        _lookMatrix.SetTRS(startPosition, Quaternion.LookRotation(targetPosition - startPosition), Vector3.one);
        _lookMatrix = _lookMatrix * Matrix4x4.TRS(Vector3.zero, localzRotation, Vector3.one);
    }
	
	// Update is called once per frame
    public void Update() {
        if (_isRunning) {
            _costTime += FightTime.deltaTime;
            Vector3 localPos = CalcPosition();
            transform.position = _lookMatrix.MultiplyPoint(localPos);
            if (transform.position != _lastPosition) {
                transform.forward = transform.position - _lastPosition;
            }
            _lastPosition = transform.position;
            CheckArrived();
        }
    }

    protected abstract Vector3 CalcPosition();

    public void Stop() {
        _isRunning = false;
    }

    public void CheckArrived() {
        Vector3 currentDirection = targetPosition - transform.position;
        //当前帧已经越过了终点
        if (Vector3.Dot(_direction, currentDirection) < 0 ||
            currentDirection.magnitude <= stoppingDistance) {
            Stop();
            OnFinish();
        }
    }

    public void SetCallBack(CallBack cb, object arg) {
        _callback = cb;
        _arg = arg;
    }

    protected void OnFinish() {
        if (_callback != null) {
            _callback(_arg);
        }
        if (isFinishDestroy) {
            Object.Destroy(gameObject);
        }
    }

    public float costTime {
        get { return _costTime; }
        set { _costTime = value; }
    }

    public bool isRunning {
        get { return _isRunning; }
    }

    public Vector3 startPosition = Vector3.zero;
	public Vector3 targetPosition = Vector3.zero;
	public GameObject startObj;
	public GameObject targetObj;
    public float stoppingDistance = 0;
    public float horizontalSpeed = 0;
    public float angle = 0;
    public bool isFinishDestroy = false;

    private CallBack _callback = null;
    private object _arg = null;
    private float _costTime = 0;
    private bool _isRunning = false;

    private Vector3 _direction = Vector3.zero;
    private Matrix4x4 _lookMatrix = new Matrix4x4();
    private Vector3 _lastPosition = Vector3.zero;
}
