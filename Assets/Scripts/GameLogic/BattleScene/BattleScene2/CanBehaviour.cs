using UnityEngine;
using System.Collections;

public class CanBehaviour : MonoBehaviour
{
    public int Step;
    private bool Actived;
    private float Speed;
    // Use this for initialization
    void Start()
    {
        EventDispatcher.AddEventListener<int>(EventDefine.Event_Active_CanSkill, OnCan);
        EventDispatcher.AddEventListener(EventDefine.Summoned_Explosed, OnExplosed);
    }

    void Destroy()
    {
        EventDispatcher.RemoveEventListener<int>(EventDefine.Event_Active_CanSkill, OnCan);
        EventDispatcher.RemoveEventListener(EventDefine.Summoned_Explosed, OnExplosed);
    }

    void OnCan(int step)
    {
        if (Step != step)
            return;

        //Actived = true;
        //Speed = 3.0f;
        //Transform[] trans = transform.GetComponentsInChildren<Transform>();
        //for (int i = 1; i < trans.Length; ++i )
        //{
        //    HitPoint hp = trans[i].gameObject.GetOrAddComponent<HitPoint>();
        //    hp.SetParentType(HitPoint.E_Parent.Can);
        //}
        EventDispatcher.TriggerEvent(EventDefine.Event_Active_Can, 1.0f);
    }

    void OnExplosed()
    {
        Speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Actived)
            return;

        Vector3 direction = ioo.cameraManager.position - transform.position;
        transform.position += direction.normalized * Time.deltaTime * Speed;
    }
}
