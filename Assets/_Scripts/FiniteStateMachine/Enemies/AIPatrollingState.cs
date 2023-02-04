using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPatrollingState : MoveState
{
    private Vector2 direction = Vector2.zero;

    internal override void EnterState()
    {
        base.EnterState();

        if (direction == Vector2.zero)
        {
            direction.Set(-1f, 0f);
        }
        else
        {
            if (fsm.Agent.CollissionSenses.IsTouchingWall || !fsm.Agent.CollissionSenses.IsThereGroundAhead)
            {
                direction.x *= -1f;
            }
        }

        fsm.Agent.Input.CallOnMovementVector(direction);
    }

    internal override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    internal override void PhysicsUpdate()
    {
        SetPlayerVelocity();
    }


    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.Input.CallOnMovementVector(Vector2.zero);
    }
}
