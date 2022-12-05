using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class FallState : MoveState
{
    [SerializeField] private bool isInCoyoteTime;
    [SerializeField] private float coyoteTimeDuration;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.fall);

        if (fsm.Agent.MovementData.CanEnterCoyoteTime)
        {
            isInCoyoteTime = true;
            coyoteTimeDuration = fsm.Agent.Data.CoyoteDuration;
        }
    }

    public override void LogicUpdate()
    {
        CalculateVelocity();

        if (isInCoyoteTime)
        {
            coyoteTimeDuration -= Time.deltaTime;

            if (coyoteTimeDuration <= 0f)
            {
                isInCoyoteTime = false;
                fsm.Agent.MovementData.ConsumeJump();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.GroundDetector.CheckIsGrounded();
        fsm.Agent.WallDetector.CheckIsTouchingWall();
        fsm.Agent.ClimbingDetector.CheckIfCanClimb();

        SetPlayerVelocity();

        if (fsm.Agent.GroundDetector.IsGrounded)
        {
            if (fsm.Agent.Rb2d.velocity.y < 0) return;
            if (Mathf.Abs(fsm.Agent.Rb2d.velocity.x) > 0f)
            {
                fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Move));
            }
            else
            {
                fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Idle));
            }
        }
    }

    protected override void ExitState()
    {
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
    }
}
