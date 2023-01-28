using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : State
{
    [SerializeField] private float idleTime = 3f;
    private float idleTimeDuration;
    private int idleCount;

    private void OnEnable()
    {
        idleCount = 1;
    }

    internal override void EnterState()
    {
        if (idleTimeDuration > 0f)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        }
        else
        {
            fsm.Agent.Rb2d.velocity = Vector2.zero;
        }

        idleCount++;
    }

    internal override void LogicUpdate()
    {
        idleTimeDuration -= Time.deltaTime;

        if (idleTimeDuration <= 0f)
        {
            fsm.TransitionToState(StateType.AIPatrolling);
        }

        if (!fsm.Agent.CollissionSenses.IsGrounded)
        {
            fsm.TransitionToState(StateType.Fall);
        }
    }

    internal override void PhysicsUpdate()
    {
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    internal override void ExitState()
    {
        if (idleCount >= 4)
        {
            idleTimeDuration = idleTime;
            idleCount = 0;
        }
    }
}
