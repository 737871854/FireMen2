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

public class PterosaurStep6 : Step
{
   private Vector3 fixedPos;
    #region Public Function
   public PterosaurStep6(PterosaurBehaviour pterosaurBehaviour)
    {
        // TODO: Complete member initialization
        this.pterosaurBehaviour = pterosaurBehaviour;
        pterosaurStep = E_PterosaurStep.Step6;
        animator = pterosaurBehaviour.Animator;
        pterosaurBehaviour.AddStep(this);
        fixedPos = pterosaurBehaviour.transform.position + pterosaurBehaviour.transform.forward * 10 - pterosaurBehaviour.transform.right * 10 + pterosaurBehaviour.transform.up * 3;
    }

    public override void RunStep()
    {
        ioo.cameraManager.PlayCPA();
        pState = E_PterosaurState.Wander;
        pathIndex = 0;
        path.Clear();
        Vector3 pos = pterosaurBehaviour.transform.position - pterosaurBehaviour.transform.right * 6 + pterosaurBehaviour.transform.up * 9 + pterosaurBehaviour.transform.forward * 6;
        path.Add(pos);
        path.Add(fixedPos);
    }

    public override void UpdateStep()
    {
        Vector3 direction = pterosaurBehaviour.LookAtPoint.position - ioo.cameraManager.cTransform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        ioo.cameraManager.cTransform.rotation = Quaternion.Lerp(ioo.cameraManager.cTransform.rotation, toRotation, Time.deltaTime * 5);

        switch (pState)
        {
            case E_PterosaurState.Wander:
                Wander();
                break;
            case E_PterosaurState.Idle:
                Idle();
                break;
            case E_PterosaurState.Run:
                Run();
                break;
            case E_PterosaurState.Pat:
                Pat();
                break;
            case E_PterosaurState.PatBreak:
                PatBreak();
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

    private void ToBeat()
    {
        pState = E_PterosaurState.Beat;
    }

    private int pathIndex;
    private List<Vector3> path = new List<Vector3>();
    private void ToUpRush(E_PterosaurState state)
    {
        ioo.cameraManager.PlayCPA();

        pathIndex = 0;
        animator.speed = 1;
        pState = state;
        pterosaurBehaviour.EnterInvincible(false);
        pterosaurBehaviour.ClearHitPoint();
        a_info.name = "attack_beat1";
        a_info.id = 20;

        path.Clear();
        Vector3 pos = pterosaurBehaviour.transform.position + pterosaurBehaviour.transform.forward * Random.Range(2.0f, 3.0f) + pterosaurBehaviour.transform.up * Random.Range(3.0f, 5.0f);
        path.Add(pos);
        pos += -5 * pterosaurBehaviour.transform.right - 6 * pterosaurBehaviour.transform.forward;
        path.Add(pos);
        path.Add(new Vector3(139.696f, 17.0503f, 60.164f));
    }

    private void ToBeatBreak()
    {
        animator.speed = 1;
        pState = E_PterosaurState.BeatBreak;
        pterosaurBehaviour.EnterInvincible(false);
        pterosaurBehaviour.ClearHitPoint();
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

        if(toRotation == pterosaurBehaviour.transform.rotation)
        {
            ToBeat();
        }
    }

    private void Wander()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("run"))
        {
            animator.SetInteger("State", 1);
        }

        if (pathIndex < path.Count)
        {
            Vector3 direction = path[pathIndex] - pterosaurBehaviour.transform.position;
            if (direction.magnitude < 0.2f)
            {
                ++pathIndex;
            }
            else
            {
                pterosaurBehaviour.transform.position += direction.normalized * Time.deltaTime * pterosaurBehaviour.MoveSpeed * 5;
                Quaternion toRotation = Quaternion.LookRotation(direction);
                pterosaurBehaviour.transform.rotation = Quaternion.Lerp(pterosaurBehaviour.transform.rotation, toRotation, pterosaurBehaviour.RotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            ToIdle();
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

    private void Pat()
    {
        Vector3 direction = ioo.cameraManager.position + Vector3.up * 4 - pterosaurBehaviour.transform.position;
        if (direction.magnitude <= 6.0f)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("attack_pat"))
            {
                animator.SetInteger("State", 7);
            }
            else
            {
                if (info.normalizedTime > 0.3f && info.normalizedTime < 0.5f)
                {
                    animator.speed = 0.1f;
                    pterosaurBehaviour.ActiveBeat();
                }
                else if (info.normalizedTime >= 0.5f && info.normalizedTime < 0.9f)
                {
                    animator.speed = 1.0f;
                }
                else if (info.normalizedTime >= 0.9f)
                {
                    ToUpRush(E_PterosaurState.UpRush0);
                }
            }
        }
        else
        {
            pterosaurBehaviour.transform.position += direction.normalized * Time.deltaTime * 10;
        }

        if (pterosaurBehaviour.HPIsActive)
        {
            bool isBreak = true;
            for (int i = 0; i < pterosaurBehaviour.HPList.Count; ++i)
            {
                HittingPart hp = pterosaurBehaviour.HPList[i];
                if (hp.curHp > 0)
                    isBreak = false;
            }

            if (isBreak)
                ToBeatBreak();
        }
    }

    private void PatBreak()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("attack_beat"))
        {
            animator.SetInteger("State", 8);
        }
        else
        {
            if (info.normalizedTime >= 0.9f)
            {
                ToUpRush(E_PterosaurState.UpRush1);
            }
        }
    }

    #endregion
}