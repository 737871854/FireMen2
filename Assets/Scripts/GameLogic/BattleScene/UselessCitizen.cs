// 场景特许要求，不再做统一管理

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UselessCitizen 
{
    public enum E_UselessType
    {
        Null = -1,
        Escaple = 0,
        ForHelp = 1,
    }

    private GameObject mGameobject;
    private List<Transform> mPath;
    private E_UselessType mType;
    private int mCurrentIndex;
    private int mNextIndex;
    private bool mCanDestroy;
    private float mSpeed;
    private Animator mAnim;
    private float mTimer;

    public bool canDestroy { get { return mCanDestroy; } }

    public UselessCitizen(GameObject gameObject, List<Transform> path, E_UselessType type)
    {
        mGameobject = gameObject;
        mPath = path;
        mType = type;
        mCanDestroy = false;
        mCurrentIndex = 0;
        mGameobject.transform.position = mPath[mCurrentIndex].position;
        mNextIndex = 1;
        mSpeed = 0.7f;
        mAnim = mGameobject.transform.Find("Root").GetComponent<Animator>();
        mTimer = 30;
        gameObject.transform.localScale = Vector3.one * 0.5f;
    }

    // Update is called once per frame
    public void Update()
    {
        if (mCanDestroy) return;

        if(mType == E_UselessType.ForHelp)
        {
            mTimer -= Time.deltaTime;
            if (mTimer <= 0)
                mCanDestroy = true;
        }

        if (mNextIndex >= mPath.Count) return;

        Vector3 direction = mPath[mNextIndex].position - mGameobject.transform.position;
        if(direction.magnitude < 0.3f)
        {
            mCurrentIndex = mNextIndex;
            ++mNextIndex;
            CheckIsEnd();
        }
        else
        {
            Vector3 step = direction.normalized * Time.deltaTime * mSpeed;
            mGameobject.transform.position += step;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            mGameobject.transform.rotation = Quaternion.Lerp(mGameobject.transform.rotation, toRotation, Time.deltaTime * 20);
        }
    }

    public void Release()
    {
        PoolManager.Instance.DeSpawn(mGameobject);
    }

    private void CheckIsEnd()
    {
        if (mNextIndex < mPath.Count) return;

        if (mType == E_UselessType.Escaple)
            mCanDestroy = true;
        else if (mType == E_UselessType.ForHelp)
            PlayAnim("State", 2);
    }

    private void PlayAnim(string name, int id)
    {
        mAnim.SetInteger(name, id);
    }
}
