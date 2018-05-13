using UnityEngine;
using System.Collections;

public abstract class ProjectileBase : MonoBehaviour
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	// 是不是结束
	public abstract bool IsFinished();

    public bool isHitPoint {
        get;
        set;
    }

	// 设置开始位置
	public abstract void SetStartPosition(Vector3 start_pos);

	// 设置结束位置并启动流程
	public abstract void SetEndPosition(Vector3 end_pos);

	// 设置速度
	public abstract void SetSpeed(float speed);

	// 销毁投射物
	public abstract void Destroy();

    public float effectBeat {
        get;
        set;
    }

    public virtual GameObject GetMainObject() {
        return null;
    }

    static public void excuteShader(GameObject _gameobject)
    {
        Renderer[] renders = _gameobject.transform.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer rd in renders)
        {
            if (rd != null && rd.sharedMaterial != null)
            {
                //if (rd.sharedMaterial.shader.isSupported == false)
                {
                    //Debugger.Log("Not Support mat:" + rd.sharedMaterial.name + ",shader:" + rd.sharedMaterial.shader.name);
                    rd.sharedMaterial.shader = Shader.Find(rd.sharedMaterial.shader.name);
                }
                //Debug.Log("@@@@@@@Out put shareMaterial Name:" + rd.sharedMaterial.name);
            }
        }
    }
}
