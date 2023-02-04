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
            fsm.Agent.MovementData.SetIsInCoyoteTime(true);
            fsm.Agent.MovementData.SetCoyoteTimeDuration(fsm.Agent.Data.CoyoteDuration);
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
                fsm.Agent.MovementData.SetIsInCoyoteTime(false);
                fsm.Agent.MovementData.ConsumeJump();
            }
        }
    }

    internal override void PhysicsUpdate()
    {
        SetPlayerVelocity();
    }

    internal override void ExitState()
    {
        if (!fsm.Agent.CollissionSenses.IsGrounded) return;

        OnGrounded?.Invoke();
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
    }

    protected override void OnAttackEnd()
    {
        base.OnAttackEnd();

        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.fall);
    }
}
