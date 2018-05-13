using UnityEngine;
using System.Collections;

public class PlayTimeControl : MonoBehaviour
{
    public GameObject[] _objList ;
    // 例如_time的值是1， 2， 3，那么，意思就是，经过1s后显示第1物体，再过2s后就显示第2个物体，再过3s后就显示第3个物体，
    // 那么就是过了6秒才把3个物体显示完
    public float[] _time;
    private float _beginTime = 0;
    private float[] _totalTimes;
    private bool _isFinish = false;
	// Use this for initialization
	void Start () {
        _totalTimes = new float[_time.Length];
        for (int i = 0; i < _time.Length; i++)
        {
            if (i > 0 ){
                _totalTimes[i] = _time[i] + _totalTimes[i - 1];
            }
            else{
                _totalTimes[i] = _time[i];
            }
        }

        for (int i = 0; i < _objList.Length; i++)
        {
            _objList[i].SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (_isFinish || _isDestroy) return;
        _beginTime += FightTime.deltaTime;
        for (int i = 0; i < _totalTimes.Length; i++)
        {
            if (_beginTime >= _totalTimes[i] && !_objList[i].activeSelf)
            {
                _objList[i].SetActive(true);
            }
            if (_objList[_totalTimes.Length - 1].activeSelf)
            {
                _isFinish = true;
            }
        }
	}

    public void Destroy() {
        if (!_isDestroy) {
            for (int i = 0; i < _objList.Length; ++i) {
                if (_objList[i] != null) {
                    GameObject myObject = Instantiate((GameObject)_objList[i]);
                    GameObject.Destroy(myObject);
                    _objList[i] = null;
                } 
            }
            _isDestroy = true;
        }
    }

    private bool _isDestroy = false;
}
