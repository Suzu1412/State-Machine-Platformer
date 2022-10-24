using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    private Animator animator;

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
            default:
                break;
        }
    }

    private void Play(string animation)
    {
        animator.Play(animation, -1, 0f);
    }
}

public enum AnimationType
{
    die, 
    hit,
    idle,
    attack,
    run,
    jump,
    fall,
    climb,
    land
}
