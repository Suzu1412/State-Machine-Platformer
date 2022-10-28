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
        base.StateFixedUpdate();

        if (!agent.Senses.IsGrounded)
        {
            agent.TransitionToState(fallState);
        }
    }

    protected override void HandleMovement(Vector2 input)
    {
        if (Mathf.Abs(input.x) > 0f)
        {
            agent.TransitionToState(moveState);
        }

        movement.Set(0f, agent.Rb2d.velocity.y);

        agent.Rb2d.velocity = movement;
    }
}
