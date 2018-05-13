using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CurveTrajectory))]
public class CurveTrajectoryEditor : Editor {
	
	private SerializedObject obj;
	private SerializedProperty startPos;
	private SerializedProperty endPos;
	private SerializedProperty stoppingDistance;
	private SerializedProperty horizontalSpeed;
	private SerializedProperty angle;
	
	private SerializedProperty wave;
	private SerializedProperty swing;

    private SerializedProperty isFinishDestroy;
	
	void OnEnable()
	{
		obj = new SerializedObject(target);
		startPos = obj.FindProperty("startObj");
		endPos = obj.FindProperty("targetObj");
		stoppingDistance = obj.FindProperty("stoppingDistance");
		horizontalSpeed = obj.FindProperty("horizontalSpeed");
		angle = obj.FindProperty("angle");
		
		wave = obj.FindProperty("wave");
		swing = obj.FindProperty("swing");

        isFinishDestroy = obj.FindProperty("isFinishDestroy");
	}
	
	public override void OnInspectorGUI ()
	{
		// 更新编辑器显示的序列化属性
		obj.Update();
		
		// 开始物体位置
		EditorGUILayout.PropertyField(startPos);
		// 结束物体位置
		EditorGUILayout.PropertyField(endPos);
		
		EditorGUILayout.PropertyField(stoppingDistance);
		EditorGUILayout.PropertyField(horizontalSpeed);
		EditorGUILayout.PropertyField(angle);
		
		EditorGUILayout.PropertyField(wave);
        EditorGUILayout.PropertyField(swing);

        EditorGUILayout.PropertyField(isFinishDestroy);

		if(GUILayout.Button("重新播放"))
		{
			((CurveTrajectory)target).Start();
		}
		
		// 接受序列化赋值
		obj.ApplyModifiedProperties();
		
	}
}
