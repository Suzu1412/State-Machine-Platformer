using UnityEngine;
using UnityEngine.Events;

public class FallState : MoveState
{
    public UnityEvent OnGrounded;

    private void OnValidate()
    {
        stateType = StateType.Fall;
    }

    internal override void EnterState()
    {
        if (!fsm.Agent.AgentWeapon.IsAttacking)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.fall);
        }


        if (fsm.Agent.MovementData.CanEnterCoyoteTime)
        {
            fsm.Agent.MovementData.IsInCoyoteTime = true;
            fsm.Agent.MovementData.CoyoteTimeDuration = fsm.Agent.Data.CoyoteDuration;
        }

        fsm.Agent.AnimationManager.OnAnimationAttackPerformed.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
    }

    internal override void LogicUpdate()
    {
        HandleAttackTransition();

        CalculateVelocity();

        if (fsm.Agent.MovementData.IsInCoyoteTime)
        {
            fsm.Agent.MovementData.ReduceCoyoteTimeDurationBySeconds(Time.deltaTime);

            if (fsm.Agent.MovementData.CoyoteTimeDuration <= 0f)
            {
                fsm.Agent.MovementData.IsInCoyoteTime = false;
                fsm.Agent.MovementData.ConsumeJump();
            }
        }
    }

    internal override void PhysicsUpdate()
    {
        SetPlayerVelocity();

        ClimbLadder();
    }

    internal override void ExitState()
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
        //if (fsm.Agent.AgentWeapon.CanIUseWeapon())
        //{
        //    fsm.TransitionToState(StateType.AirFallAttack);
        //}
    }

    protected override void OnAttackEnd()
    {
        base.OnAttackEnd();

        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.fall);
    }
}
