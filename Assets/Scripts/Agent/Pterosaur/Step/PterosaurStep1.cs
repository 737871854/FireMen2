using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PterosaurStep1 : Step
{
    private Vector3 fixedPos;
    #region Public Function
    public PterosaurStep1(PterosaurBehaviour pterosaurBehaviour)
    {
        // TODO: Complete member initialization
        this.pterosaurBehaviour = pterosaurBehaviour;
        pterosaurStep = E_PterosaurStep.Step1;
        animator = pterosaurBehaviour.Animator;
        pterosaurBehaviour.AddStep(this);
        fixedPos = pterosaurBehaviour.transform.position + Vector3.up * 4;
        canUpdateSkill = false;
    }

    public override void RunStep()
    {
        pState = E_PterosaurState.Lift;
        // 技能,冷却时间，。。。。，标志位
        // 标志位0：必须顺序播放完才能进入下个阶段；1：循环播放所有技能，事件触发进入下个阶段；
        List<int> list = new List<int> { 2, 0, 3, 3, 4, 4, 0 };

        for (int i = 0; i < list.Count - 2; )
        {
            PterosaurSkill skill = new PterosaurSkill((E_PterosaurState)list[i++], list[i++]);
            pskList.Add(skill);
        }
        skillFlag = list[list.Count - 1];
        curSkillIndex = 0;
    }

    public override void UpdateStep()
    {
        Vector3 direction = pterosaurBehaviour.LookAtPoint.position - ioo.cameraManager.cTransform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        ioo.cameraManager.cTransform.rotation = Quaternion.Lerp(ioo.cameraManager.cTransform.rotation, toRotation, Time.deltaTime * 5);

        switch (pState)
        {
            case E_PterosaurState.Idle:
                Idle();
                break;
            case E_PterosaurState.Run:
                Run();
                break;
            case E_PterosaurState.Lift:
                Lift();
                break;
            case E_PterosaurState.Rain:
                Rain();
                break;
            case E_PterosaurState.Claw:
                Claw();
                break;
            case E_PterosaurState.Beat:
                Beat();
                break;
            case E_PterosaurState.ClawBreak:
                ClawBreak();
                break;
            case E_PterosaurState.BeatBreak:
                BeatBreak();
                break;
            case E_PterosaurState.UpRush0:
            case E_PterosaurState.UpRush1:
                UpRush();
                break;
        }

        if (!canUpdateSkill)
            return;

        if (pskList.Count == 0)
        {
            pterosaurBehaviour.NextStep();
            return;
        }

        PterosaurSkill skill = pskList[curSkillIndex];
        float time = skill.time;
        if (time > 0)
        {
            time -= Time.deltaTime;
            skill.time = time;
        }
        else
        {
            UseSkill(skill.skill);
            pskList.Remove(skill);
        }
    }

    #endregion

    #region Private Function
    private void ToIdle()
    {
        canUpdateSkill = true;
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
        if (pState == E_PterosaurState.UpRush0)
        {
            a_info.name = "attack_claw1";
            a_info.id = 21;

            path.Clear();
            Vector3 pos = pterosaurBehaviour.transform.position + pterosaurBehaviour.transform.forward * Random.Range(2.0f, 3.0f) + pterosaurBehaviour.transform.up * Random.Range(3.0f, 5.0f);
            path.Add(pos);
            pos += -4 * pterosaurBehaviour.transform.right - 4 * pterosaurBehaviour.transform.forward;
            path.Add(pos);
            path.Add(fixedPos);
        }
        else if (pState == E_PterosaurState.UpRush1)
        {
            a_info.name = "attack_beat1";
            a_info.id = 20;

            path.Clear();
            Vector3 pos = pterosaurBehaviour.transform.position + pterosaurBehaviour.transform.forward * Random.Range(4.0f, 5.0f) + pterosaurBehaviour.transform.up * Random.Range(3.0f, 5.0f);
            path.Add(pos);
            pos += -3 * pterosaurBehaviour.transform.right - 4 * pterosaurBehaviour.transform.forward;
            path.Add(pos);
            path.Add(fixedPos);
        }
    }

    private void ToRainEnd()
    {
        animator.speed = 1;
        canUpdateSkill = true;
        pState = E_PterosaurState.Run;
        pterosaurBehaviour.EnterInvincible(false);
        pterosaurBehaviour.ClearHitPoint();
    }

    private void ToClawBreak()
    {
        animator.speed = 1;
        pState = E_PterosaurState.ClawBreak;
        pterosaurBehaviour.EnterInvincible(false);
        pterosaurBehaviour.ClearHitPoint();
    }

    private void ToBeatBreak()
    {
        animator.speed = 1;
        pState = E_PterosaurState.BeatBreak;
        pterosaurBehaviour.EnterInvincible(false);
        pterosaurBehaviour.ClearHitPoint();
    }

    private void Lift()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("run"))
        {
            animator.SetInteger("State", 1);
        }

        Vector3 direction = fixedPos - pterosaurBehaviour.transform.position;
        if (direction.magnitude < 0.2f)
        {
            canUpdateSkill = true;
        }
        else
        {
            pterosaurBehaviour.transform.position += pterosaurBehaviour.MoveSpeed * Time.deltaTime * Vector3.up * 5;
        }
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

    private void Run()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("run"))
        {
            animator.SetInteger("State", 1);
        }
    }

    private void Claw()
    {
        Vector3 direction = ioo.cameraManager.position - pterosaurBehaviour.transform.position;
        if (direction.magnitude <= 6.0f)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("attack_claw"))
            {
                animator.SetInteger("State", 3);
            }
            else
            {
                if (info.normalizedTime > 0.5f && info.normalizedTime < 0.9f)
                {
                    animator.speed = 0.05f;
                    pterosaurBehaviour.ActiveClaw();
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
                ToClawBreak();
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
                ToIdle();
        }else
        {
            pterosaurBehaviour.transform.position += direction.normalized * Time.deltaTime * pterosaurBehaviour.MoveSpeed * 5;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            pterosaurBehaviour.transform.rotation = Quaternion.Lerp(pterosaurBehaviour.transform.rotation, toRotation, pterosaurBehaviour.RotationSpeed * Time.deltaTime);
        }
    }

    private void Beat()
    {
        Vector3 direction = ioo.cameraManager.position + Vector3.up * 4 - pterosaurBehaviour.transform.position;
        if (direction.magnitude <= 6.0f)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("attack_beat"))
            {
                animator.SetInteger("State", 5);
            }
            else
            {
                if (info.normalizedTime > 0.3f && info.normalizedTime < 0.5f)
                {
                    animator.speed = 0.1f;
                    pterosaurBehaviour.ActiveBeat();
                }else if (info.normalizedTime >= 0.5f && info.normalizedTime < 0.9f)
                {
                    animator.speed = 1.0f;
                }
                else if (info.normalizedTime >= 0.9f)
                {
                    ToUpRush(E_PterosaurState.UpRush1);
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

    private void BeatBreak()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("attack_beat"))
        {
            animator.SetInteger("State", 6);
        }
        else
        {
            if (info.normalizedTime >= 0.9f)
            {               
                ToUpRush(E_PterosaurState.UpRush1);
            }
        }
    }

    private void Rain()
    {
        Vector3 direction = ioo.cameraManager.position - pterosaurBehaviour.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        pterosaurBehaviour.transform.rotation = Quaternion.Lerp(pterosaurBehaviour.transform.rotation, toRotation, Time.deltaTime * pterosaurBehaviour.RotationSpeed);

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("attack_rain"))
        {
            animator.SetInteger("State", 2);
        }else
        {
            if (info.normalizedTime >= 0.68f && info.normalizedTime < 0.95f)
            {
                animator.speed = 0.1f;
                pterosaurBehaviour.ActiveRain();
            }
            else if (info.normalizedTime >= 0.95f)
            {
                EventDispatcher.TriggerEvent(EventDefine.Summoned_Explosed);
                ToRainEnd();
            }
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
                ToRainEnd();
        }
    }

    private void UseSkill(E_PterosaurState skill)
    {
        canUpdateSkill = false;
        pterosaurBehaviour.EnterInvincible();
        switch (skill)
        {
            case E_PterosaurState.Rain:
                pState = E_PterosaurState.Rain;
                break;
            case E_PterosaurState.Claw:
                pState = E_PterosaurState.Claw;
                break;
            case E_PterosaurState.Beat:
                pState = E_PterosaurState.Beat;
                break;
        }
    }
    #endregion
}
