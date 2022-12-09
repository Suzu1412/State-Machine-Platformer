using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class IdleState : State
{
    protected override void EnterState()
    {
        base.EnterState();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!fsm.Agent.GroundDetector.IsGrounded)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Fall));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (Mathf.Abs(fsm.Agent.Input.MovementVector.x) > 0f)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Move));
        }
    }

    protected override void HandleMovement(Vector2 input)
    {
        if (input.y > 0.33f)
        {
            if (fsm.Agent.ClimbingDetector.CanClimb && fsm.Agent.TopLadderDetector.TopLadder == null)
            {
                fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Climb));
            }
        }

        if (input.y < -0.33f)
        {
            if (fsm.Agent.ClimbingDetector.CanClimb && fsm.Agent.TopLadderDetector.TopLadder != null)
            {
                fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Climb));
            }
        }
    }
}