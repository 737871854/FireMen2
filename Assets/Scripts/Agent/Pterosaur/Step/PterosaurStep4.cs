/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PterosaurStep2.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/1/6 9:07:12
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;
using System.Collections.Generic;

public class PterosaurStep4 : Step
{
   private Vector3 fixedPos;
    #region Public Function
   public PterosaurStep4(PterosaurBehaviour pterosaurBehaviour)
    {
        // TODO: Complete member initialization
        this.pterosaurBehaviour = pterosaurBehaviour;
        pterosaurStep = E_PterosaurStep.Step4;
        animator = pterosaurBehaviour.Animator;
        pterosaurBehaviour.AddStep(this);
        fixedPos = new Vector3(148.15f, 12.35f, 53.44f);
    }

    public override void RunStep()
    {
        pState = E_PterosaurState.Throw;
    }

    public override void UpdateStep()
    {
        Vector3 direction = pterosaurBehaviour.LookAtPoint.position - ioo.cameraManager.cTransform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        ioo.cameraManager.cTransform.rotation = Quaternion.Lerp(ioo.cameraManager.cTransform.rotation, toRotation, Time.deltaTime * 5);

        switch (pState)
        {
            case E_PterosaurState.Throw:
                Throw();
                break;
            case E_PterosaurState.Idle:
                Idle();
                break;
            case E_PterosaurState.Run:
                Run();
                break;
            case E_PterosaurState.UpRush0:
                UpRush();
                break;
        }
    }

    #endregion

    #region Private Function
    private void ToIdle()
    {
        pState = E_PterosaurState.Idle;
    }

    private int pathIndex;
    private List<Vector3> path = new List<Vector3>();
    private void ToUpRush(E_PterosaurState state)
    {
        pathIndex = 0;
        animator.speed = 1;
        pState = state;
        pterosaurBehaviour.EnterInvincible(false);
        pterosaurBehaviour.ClearHitPoint();
     
        path.Clear();
        Vector3 pos = pterosaurBehaviour.transform.position + pterosaurBehaviour.transform.forward * Random.Range(2.0f, 3.0f) + pterosaurBehaviour.transform.up * Random.Range(3.0f, 5.0f);
        path.Add(pos);
        pos += -5 * pterosaurBehaviour.transform.right - 6 * pterosaurBehaviour.transform.forward;
        path.Add(pos);
    }

    private void Idle()
    {
        Vector3 direction = ioo.cameraManager.position - pterosaurBehaviour.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        pterosaurBehaviour.transform.rotation = Quaternion.Lerp(pterosaurBehaviour.transform.rotation, toRotation, pterosaurBehaviour.RotationSpeed * Time.deltaTime);

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("run"))
        {
            animator.SetInteger("State", 1);
        }
    }

    private void Throw()
    {
        Vector3 direction = fixedPos - pterosaurBehaviour.transform.position;
        if (direction.magnitude < 1.0f)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("attack_throw"))
            {
                animator.SetInteger("State", 13);
            }
            else
            {
                if (info.normalizedTime >= 0.6f && info.normalizedTime < 0.8f)
                {
                    animator.speed = 0.05f;
                    pterosaurBehaviour.ActiveCan(3);
                }else if (info.normalizedTime >= 0.8f)
                {
                    EventDispatcher.TriggerEvent(EventDefine.Summoned_Explosed);
                    ToUpRush(E_PterosaurState.UpRush0);
                }
            }
        }
        else
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("run"))
            {
                animator.SetInteger("State", 1);
            }

            pterosaurBehaviour.transform.position += direction.normalized * Time.deltaTime * pterosaurBehaviour.MoveSpeed * 5;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            pterosaurBehaviour.transform.rotation = Quaternion.Lerp(pterosaurBehaviour.transform.rotation, toRotation, pterosaurBehaviour.RotationSpeed * Time.deltaTime);
        }
    }

    private void Run()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("run"))
        {
            animator.SetInteger("State", 1);
        }
    }

    private void UpRush()
    {
        Vector3 pos = path[pathIndex];
        Vector3 direction = pos - pterosaurBehaviour.transform.position;
        if (pathIndex == 0)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName(a_info.name))
            {
                animator.SetInteger("State", a_info.id);
            }
        }
        else
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("run"))
            {
                animator.SetInteger("State", 1);
            }

        }
        if (direction.magnitude < 0.2f)
        {
            ++pathIndex;
            if (pathIndex >= path.Count)
                pterosaurBehaviour.NextStep();
        }else
        {
            pterosaurBehaviour.transform.position += direction.normalized * Time.deltaTime * pterosaurBehaviour.MoveSpeed * 5;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            pterosaurBehaviour.transform.rotation = Quaternion.Lerp(pterosaurBehaviour.transform.rotation, toRotation, pterosaurBehaviour.RotationSpeed * Time.deltaTime);
        }
    }

    private void ClawBreak()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("attack_claw_break"))
        {
            animator.SetInteger("State", 4);
        }
        else
        {
            if (info.normalizedTime >= 0.9f)
            {
                ToUpRush(E_PterosaurState.UpRush0);
            }
        }
    }
    #endregion
}