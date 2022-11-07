using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private State moveState;

    protected override void EnterState()
    {
        base.EnterState();
        agent.AnimationManager.PlayAnimation(AnimationType.idle);
    }

    public override void StateFixedUpdate()
    {
        DetectCollissions();

        if (!agent.GroundDetector.IsGrounded)
        {
            agent.TransitionToState(fallState);
        }
    }

    protected override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);

        if (Mathf.Abs(input.x) > 0f)
        {
            agent.TransitionToState(moveState);
        }
    }
}
