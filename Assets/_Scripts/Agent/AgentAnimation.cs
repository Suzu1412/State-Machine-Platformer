using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[DefaultExecutionOrder(-900)] // To Prevent any kind of errors it allows the awake to be called earlier
public class AgentAnimation : MonoBehaviour
{
    private Animator animator;

    public UnityEvent OnAnimationAction;
    public UnityEvent OnAnimationEnd;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError(this.name + ": Has no Animator Attached");
        }
    }

    public void PlayAnimation(AnimationType type)  
    {
        switch (type)
        {
            case AnimationType.die:
                Play("Die");
                break;
            case AnimationType.hit:
                Play("Hit");
                break;
            case AnimationType.idle:
                Play("Idle");
                break;
            case AnimationType.attack:
                Play("Attack");
                break;
            case AnimationType.run:
                Play("Run");
                break;
            case AnimationType.jump:
                Play("Jump");
                break;
            case AnimationType.fall:
                Play("Fall");
                break;
            case AnimationType.climb:
                Play("Climb");
                break;
            case AnimationType.land:
                Play("Land");
                break;
            case AnimationType.roll:
                Play("Roll");
                break;
            default:
                Play("Idle");
                break;
        }
    }

    private void Play(string animation)
    {
        animator.Play(animation);
    }

    public void Pause()
    {
        animator.speed = 0f;
    }

    public void Resume()
    {
        animator.speed = 1f;
    }

    public void CheckCurrentAnimation()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
    }

    public void ResetEvents()
    {
        OnAnimationAction.RemoveAllListeners();
        OnAnimationEnd.RemoveAllListeners();
    }

    public void InvokeAnimationAction()
    {
        OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}
