using UnityEngine;
using UnityEngine.Events;

public class FallState : MoveState
{
    public UnityEvent OnGrounded;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.fall);

        if (fsm.Agent.MovementData.CanEnterCoyoteTime)
        {
            fsm.Agent.MovementData.IsInCoyoteTime = true;
            fsm.Agent.MovementData.CoyoteTimeDuration = fsm.Agent.Data.CoyoteDuration;
        }
    }

    public override void LogicUpdate()
    {
        CalculateVelocity();

        if (fsm.Agent.MovementData.IsInCoyoteTime)
        {
            fsm.Agent.MovementData.ReduceCoyoteTimeDurationByTime(Time.deltaTime);

            if (fsm.Agent.MovementData.CoyoteTimeDuration <= 0f)
            {
                fsm.Agent.MovementData.IsInCoyoteTime = false;
                fsm.Agent.MovementData.ConsumeJump();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
        fsm.Agent.CollissionSenses.DetectLadder();

        SetPlayerVelocity();

        HandleTransitionToStates();

        ClimbLadder();
    }

    protected override void ExitState()
    {
        if (!fsm.Agent.CollissionSenses.IsGrounded) return;

        OnGrounded?.Invoke();
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
    }

    protected override void HandleRollPressed()
    {
        // We avoid the player being able to roll while in this state
    }

    protected override void HandleAttackPressed()
    {
        if (fsm.Agent.AgentWeapon.CanIUseWeapon())
        {
            fsm.TransitionToState(StateType.AirFallAttack);
        }
    }

    protected virtual void HandleTransitionToStates()
    {
        if (fsm.Agent.CollissionSenses.IsGrounded && fsm.Agent.Rb2d.velocity.y == 0f)
        {
            if (Mathf.Abs(fsm.Agent.Rb2d.velocity.x) > 0f)
            {
                fsm.TransitionToState(StateType.Move);
            }
            else
            {
                fsm.TransitionToState(StateType.Idle);
            }
        }
    }
}
