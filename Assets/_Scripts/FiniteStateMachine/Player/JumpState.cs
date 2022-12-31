using UnityEngine;


public class JumpState : MoveState
{
    protected override void EnterState()
    {
        fsm.Agent.MovementData.ActivateJump();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.jump);
        fsm.Agent.MovementData.JumpDuration = fsm.Agent.Data.JumpDuration;
    }

    public override void LogicUpdate()
    {
        fsm.Agent.MovementData.ReduceJumpDurationByTime(Time.deltaTime);
        if (fsm.Agent.MovementData.JumpDuration <= 0f) HandleJumpReleased();
        CalculateVelocity();
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
        fsm.Agent.CollissionSenses.DetectLadder();

        SetPlayerVelocity();

        ClimbLadder();
    }

    protected override void CalculateVelocity()
    {
        base.CalculateVelocity();

        if (fsm.Agent.MovementData.JumpDuration <= 0f) return;

        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, fsm.Agent.Data.JumpSpeed));
    }

    protected override void HandleJumpReleased()
    {
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, 0f));

        fsm.TransitionToState(StateType.Fall);
    }

    protected override void HandleAttackPressed()
    {
        if (fsm.Agent.AgentWeapon.CanIUseWeapon())
        {
            fsm.TransitionToState(StateType.AirJumpAttack);
        }
    }

    protected override void ExitState()
    {
    }

    protected override void HandleRollPressed()
    {
        
    }
}
