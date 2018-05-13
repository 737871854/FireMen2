using UnityEngine;
using System.Collections;

public class CurveTrajectory : Trajectory {
	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override Vector3 CalcPosition() {
		Vector3 pos = new Vector3();
		pos.x = 0;
		pos.z = horizontalSpeed * costTime;
		//befor the height point
		pos.y = swing * Mathf.Sin(2 * Mathf.PI / wave * pos.z);

		return pos;
	}

	// move wave 波长 不等于0
	public float wave = 1;
	// move swing 振幅
	public float swing = 0;
}
