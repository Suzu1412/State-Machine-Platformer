using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Windows;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class JumpState : MoveState
{
    [SerializeField] private float jumpDuration;

    protected override void EnterState()
    {
        fsm.Agent.MovementData.ActivateJump();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.jump);
        jumpDuration = fsm.Agent.Data.JumpDuration;
    }

    public override void LogicUpdate()
    {
        jumpDuration -= Time.deltaTime;
        if (jumpDuration <= 0f) HandleJumpReleased();
        CalculateVelocity();
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.GroundDetector.CheckIsGrounded();
        fsm.Agent.WallDetector.CheckIsTouchingWall();
        fsm.Agent.ClimbingDetector.CheckIfCanClimb();

        SetPlayerVelocity();

        if (fsm.Agent.Rb2d.velocity.y <= 0f)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Fall));
        }

        ClimbLadder();
    }

    protected override void CalculateVelocity()
    {
        base.CalculateVelocity();

        if (jumpDuration <= 0f) return;

        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, fsm.Agent.Data.JumpSpeed));

    }

    protected override void HandleJumpReleased()
    {
        jumpDuration = 0f;
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, 0f));
    }

    protected override void ExitState()
    {
        jumpDuration = 0f;
    }
}
