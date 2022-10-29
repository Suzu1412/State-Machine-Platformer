using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MoveState
{
    [SerializeField] private State moveState;
    private bool isInCoyoteTime;
    private float coyoteTimeDuration;

    protected override void EnterState()
    {
        if (CanEnterCoyoteTime)
        {
            isInCoyoteTime = true;
            coyoteTimeDuration = agent.Data.CoyoteDuration;
        }

        agent.AnimationManager.PlayAnimation(AnimationType.fall);
        movementData.SetCurrentVelocity(agent.Rb2d.velocity);
        agent.Rb2d.velocity = movementData.CurrentVelocity;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (isInCoyoteTime)
        {
            coyoteTimeDuration -= Time.deltaTime;

            if (coyoteTimeDuration <= 0f)
            {
                isInCoyoteTime = false;
                ConsumeJump();
            }
        }
    }


    public override void StateFixedUpdate()
    {
        agent.Senses.CheckIsGrounded();
        SetPlayerVelocity(movementData);

        if (agent.Senses.IsGrounded)
        {
            Debug.Log("is grounded");
            if (Mathf.Abs(agent.Rb2d.velocity.x) > 0f)
            {
                agent.TransitionToState(moveState);
            }
            else
            {
                agent.TransitionToState(idleState);
            }
        }
    }

    protected override void ExitState()
    {
        ResetJump();
    }
}
