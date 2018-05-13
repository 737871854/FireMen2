using UnityEngine;
using System.Collections;

public class HandBehaviour : MonoBehaviour
{
    private Animator handAnimator;
    private Animator stateAnimator;
    private GameObject body;
    // Use this for initialization
    void Start()
    {
        body = transform.Find("Hand").gameObject;
        handAnimator = transform.Find("Hand").gameObject.GetComponent<Animator>();
        stateAnimator = transform.Find("Hand/Root").gameObject.GetComponent<Animator>();

        EventDispatcher.AddEventListener(EventDefine.Event_Monster_Hold_Screen, OnShakeScreen);
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnStruggle);
    }

    void OnDestroy()
    {
        EventDispatcher.RemoveEventListener(EventDefine.Event_Monster_Hold_Screen, OnShakeScreen);
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnStruggle);
    }

    private void OnShakeScreen()
    {
        body.SetActive(true);
        handAnimator.SetInteger("State", 1);
    }

    private void OnStruggle(bool flag)
    {
        if (flag)
        {
            stateAnimator.SetInteger("State", 1);
        }
        else
        {
            stateAnimator.SetInteger("State", 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!body.activeSelf)
            return;

        AnimatorStateInfo info0 = stateAnimator.GetCurrentAnimatorStateInfo(0);
        if (info0.IsName("02") || info0.IsName("03"))
        {
            if (info0.normalizedTime >= 0.99f)
            {
                handAnimator.SetInteger("State", 2);
            }
        }

        AnimatorStateInfo info1 = handAnimator.GetCurrentAnimatorStateInfo(0);
        if (info1.IsName("disappear") && info1.normalizedTime >= 0.9f)
        {
            body.SetActive(false);
        }
    }
}
