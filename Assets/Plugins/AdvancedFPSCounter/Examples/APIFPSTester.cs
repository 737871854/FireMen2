using UnityEngine;
using System.Collections;
using CodeStage.AdvanecedFPSCounter;

public class APIFPSTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        AFPSCounter.Instance.enabled = true;
        AFPSCounter.Instance.fpsCounter.Enabled = true;
        AFPSCounter.Instance.memoryCounter.Enabled = true;
        AFPSCounter.Instance.deviceInfoCounter.Enabled = true;
        AFPSCounter.Instance.memoryCounter.Anchor = CodeStage.AdvanecedFPSCounter.Label.LabelAnchor.UpperLeft;
        AFPSCounter.Instance.fpsCounter.Anchor = CodeStage.AdvanecedFPSCounter.Label.LabelAnchor.UpperLeft;
        AFPSCounter.Instance.deviceInfoCounter.Anchor = CodeStage.AdvanecedFPSCounter.Label.LabelAnchor.UpperRight;

        AFPSCounter.Instance.deviceInfoCounter.Color = Color.white;
        AFPSCounter.Instance.memoryCounter.Color = Color.white;
        AFPSCounter.Instance.fpsCounter.ColorNormal = Color.white;

        AFPSCounter.Instance.ForceFrameRate = true;
        AFPSCounter.Instance.ForcedFrameRate = 60;
    }
}
