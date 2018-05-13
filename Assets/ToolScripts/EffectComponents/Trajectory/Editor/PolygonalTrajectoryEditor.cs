using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PolygonalTrajectory))]
public class PolygonalTrajectoryEditor : Editor {
	
	private SerializedObject obj;
	private SerializedProperty startPos;
	private SerializedProperty endPos;
	private SerializedProperty stoppingDistance;
	private SerializedProperty horizontalSpeed;
	private SerializedProperty angle;
	
	private SerializedProperty count;
	private SerializedProperty maxAngle;
    private SerializedProperty minAngle;

    private SerializedProperty isFinishDestroy;
	
	void OnEnable()
	{
		obj = new SerializedObject(target);
		startPos = obj.FindProperty("startObj");
		endPos = obj.FindProperty("targetObj");
		stoppingDistance = obj.FindProperty("stoppingDistance");
		horizontalSpeed = obj.FindProperty("horizontalSpeed");
		angle = obj.FindProperty("angle");
		
		count = obj.FindProperty("count");
		minAngle = obj.FindProperty("minAngle");
        maxAngle = obj.FindProperty("maxAngle");

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
		
		EditorGUILayout.PropertyField(count);
		EditorGUILayout.PropertyField(minAngle);
        EditorGUILayout.PropertyField(maxAngle);

        EditorGUILayout.PropertyField(isFinishDestroy);

		if(GUILayout.Button("重新播放"))
		{
			((PolygonalTrajectory)target).Start();
		}
		
		// 接受序列化赋值
		obj.ApplyModifiedProperties();
	}
}
