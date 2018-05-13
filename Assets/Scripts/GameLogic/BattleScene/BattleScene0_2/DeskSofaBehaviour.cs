using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeskSofaBehaviour : MonoBehaviour
{
    public E_Fitment mFitment;

    private Vector3 targetPos;
    private bool activeSelf;
    private float speed;

    void Start()
    {
        GameObject go = GameObject.Find("DeskSofaAttackPoint").gameObject;
        targetPos = go.transform.position;

        switch (mFitment)
        {
            case E_Fitment.Desk:
                EventDispatcher.AddEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Desk, OnBullSill);
                break;
            case E_Fitment.Sofa0:
                EventDispatcher.AddEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Left_Sofa, OnBullSill);
                break;
            case E_Fitment.Sofa1:
                EventDispatcher.AddEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Right_Sofa, OnBullSill);
                break;
        }

    }
    
    void Destroy()
    {
        switch (mFitment)
        {
            case E_Fitment.Desk:
                EventDispatcher.RemoveEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Desk, OnBullSill);
                break;
            case E_Fitment.Sofa0:
                EventDispatcher.RemoveEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Left_Sofa, OnBullSill);
                break;
            case E_Fitment.Sofa1:
                EventDispatcher.RemoveEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Right_Sofa, OnBullSill);
                break;
        }
    }

    private void OnBullSill()
    {
        activeSelf = true;
        float distance = Vector3.Distance(transform.position, targetPos);
        speed = distance / 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeSelf)
            return;

        if(ioo.gameMode.IsUsingHittingPartAllBreaked())
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = targetPos - transform.position;
        if (direction.magnitude < 0.1f)
        {
            activeSelf = false;
            ioo.gameMode.ClearHitPoint();
            Destroy(gameObject);
            // 相机震动
            ioo.cameraManager.NormalShake();
            int[] args = new int[] { -1, -1, 2 };
            ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);

            gameObject.AddScreenCrash();
        }
        else
        {
            transform.position += direction.normalized * Time.deltaTime * speed;
            transform.localEulerAngles += Vector3.right * Time.deltaTime * 15;
        }
    }
}
