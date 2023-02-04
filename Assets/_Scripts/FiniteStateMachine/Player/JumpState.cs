using UnityEngine;


public class JumpState : MoveState
{
    private void OnValidate()
    {
        stateType = StateType.Jump;
    }

    internal override void EnterState()
    {
        fsm.Agent.MovementData.ActivateJump();

        if (!fsm.Agent.AgentWeapon.IsAttacking)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.jump);
            fsm.Agent.MovementData.SetCurrentSpeed(0f);
        }
        
        fsm.Agent.MovementData.SetJumpDuration(fsm.Agent.Data.JumpDuration);

        fsm.Agent.AnimationManager.OnAnimationAttackPerformed.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
    }

    internal override void LogicUpdate()
    {
        HandleAttackTransition();

        fsm.Agent.MovementData.ReduceJumpDurationBySeconds(Time.deltaTime);
        if (!fsm.Agent.MovementData.IsJumping) HandleJumpReleased();
        CalculateVelocity();
    }

    internal override void PhysicsUpdate()
    {
        SetPlayerVelocity();
    }

    internal override void ExitState()
    {
        fsm.Agent.MovementData.SetJumpDuration(0f);
    }

    protected override void CalculateVelocity()
    {
        base.CalculateVelocity();

        if (!fsm.Agent.MovementData.IsJumping) return;

        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, fsm.Agent.Data.JumpSpeed));
    }

    protected override void OnAttackEnd()
    {
        base.OnAttackEnd();

        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.jump);
    }
}
