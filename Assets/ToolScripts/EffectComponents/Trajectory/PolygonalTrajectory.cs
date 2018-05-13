using UnityEngine;
using System.Collections;

public class PolygonalTrajectory : Trajectory {
	// Use this for initialization
	public override void Start () {
		base.Start();

		//check arg
		_distance = Vector3.Distance(targetPosition, startPosition);
		_blockDistance = _distance / count;

		_angles = new float[count];
		for (int i = 0; i < count; i++) {
			_angles[i] = Mathf.PI / 180 * Random.Range(minAngle, maxAngle) / 2;
		}

		_curBlock = 0;
	}
	
	// Update is called once per frame
	protected override Vector3 CalcPosition() {
		Vector3 pos = new Vector3();
		pos.x = 0;
		pos.z = horizontalSpeed * costTime;
		//befor the height point
		if (count < 1) {
			pos.y = 0;
		} else {
			float curBlockStart = _blockDistance * _curBlock;
			float curBlockEnd = curBlockStart + _blockDistance;

			if (pos.z > curBlockStart + _blockDistance / 2) {
				pos.y = (curBlockEnd - pos.z) / Mathf.Tan(_angles[_curBlock]) * Mathf.Pow(-1, _curBlock);
			} else {
				pos.y = (pos.z - curBlockStart) / Mathf.Tan(_angles[_curBlock]) * Mathf.Pow(-1, _curBlock);
			}

			if (pos.z > curBlockEnd) {
				_curBlock += 1;
			}
		}

		return pos;
	}
	
	private float _distance = 0;
	private float _blockDistance = 0;
	// angles save 记录角度
	private float[] _angles;
	// cur move block
	private int _curBlock;

	// count 段数
	public int count = 0;
	// max Angle degree 最大不超过180
	public float maxAngle = 179;
	// min Angle degree 最小对于0
	public float minAngle = 1;
}
