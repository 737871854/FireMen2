using UnityEngine;
using System.Collections;

public class ParabolaTrajectory : Trajectory {

	// Use this for initialization
	public override void Start () {
        base.Start();
        //check arg
        float distance = Vector3.Distance(targetPosition, startPosition);
        float percent = heightPointHorizontalPercent / 100.0f;
        //time of arrive the height point
        _upTime = distance * percent / horizontalSpeed;
        float downTime = distance * (1.0f - percent) / horizontalSpeed;
        //s = 1/2at^2
        _upAcceleration = maxHeight * 2 / Mathf.Pow(_upTime, 2);
        _upStartSpeed = _upAcceleration * _upTime;
        _downAcceleration = maxHeight * 2 / Mathf.Pow(downTime, 2);
	}
	
	// Update is called once per frame
    protected override Vector3 CalcPosition() {
        Vector3 pos = new Vector3();
        pos.x = 0;
        pos.z = horizontalSpeed * costTime;
        //befor the height point
        if (costTime < _upTime) {
            pos.y = _upStartSpeed * costTime - _upAcceleration * Mathf.Pow(costTime, 2) / 2;
        } else {
            pos.y = maxHeight - _downAcceleration * Mathf.Pow(costTime - _upTime, 2) / 2;
        }
        return pos;
    }

    public float maxHeight = 0;
    public int heightPointHorizontalPercent = 0;

    private float _upStartSpeed = 0;
    private float _upAcceleration = 0;
    private float _downAcceleration = 0;
    private float _upTime = 0;
}
