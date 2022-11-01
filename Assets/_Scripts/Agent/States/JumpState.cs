using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MoveState
{
    [SerializeField] private float jumpDuration;

    protected override void EnterState()
    {
        agent.AnimationManager.PlayAnimation(AnimationType.jump);
        jumpDuration = agent.Data.JumpDuration;
    }

    public override void StateUpdate()
    {
        jumpDuration -= Time.deltaTime;
        if (jumpDuration <= 0f) HandleJumpReleased();
        CalculateVelocity();
    }

    public override void StateFixedUpdate()
    {
        SetPlayerVelocity();
        if (agent.Rb2d.velocity.y <= 0f)
        {
            agent.TransitionToState(fallState);
        }
    }

    protected override void CalculateVelocity()
    {
        base.CalculateVelocity();

        if (jumpDuration <= 0f) return;

        movementData.SetCurrentVelocity(new Vector2(movementData.CurrentVelocity.x, agent.Data.JumpSpeed));

    }

    protected override void HandleJumpReleased()
    {
        jumpDuration = 0f;
        movementData.SetCurrentVelocity(new Vector2(movementData.CurrentVelocity.x, 0f));
    }

    protected override void ExitState()
    {
        jumpDuration = 0f;
    }

    /// TODO: Eliminar codigo sin usar, solo usado de referencia de momento


    /*
    protected override void EnterState()
    {
        agent.AnimationManager.PlayAnimation(AnimationType.jump);
        movementData.SetCurrentVelocity(new Vector2(agent.Rb2d.velocity.x, agent.Data.JumpForce));
        agent.Rb2d.velocity = movementData.CurrentVelocity;
        jumpPressed = true;
    }

    public override void StateUpdate()
    {
        ControlJumpHeight();
        CalculateVelocity();

        
    }

    public override void StateFixedUpdate()
    {
        SetPlayerVelocity(movementData);

        if (agent.Rb2d.velocity.y <= 0f)
        {
            agent.TransitionToState(fallState);
        }
    }

    protected override void HandleJumpPressed()
    {
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    private void ControlJumpHeight()
    {
        if (jumpPressed == false)
        {
            movementData.SetCurrentVelocity(new Vector2(agent.Rb2d.velocity.x, (agent.Rb2d.velocity.y + 
                agent.Data.LowJumpMultiplier) * Physics2D.gravity.y * Time.deltaTime));

            agent.Rb2d.velocity = movementData.CurrentVelocity;
        }
    }
    */
}
