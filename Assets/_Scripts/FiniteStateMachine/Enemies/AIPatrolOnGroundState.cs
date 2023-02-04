using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolOnGroundState : MoveState
{
    private Vector2 direction = Vector2.zero;
    private int pathBlockedAmount;
    private bool isTouchingWall;
    private bool isGroundAhead;
    private float timeBeforeCanChangeDirection;

    internal override void EnterState()
    {
        base.EnterState();

        fsm.Agent.MovementData.SetMoveDuration(fsm.Agent.Data.MoveDuration);
        pathBlockedAmount = 3;
        timeBeforeCanChangeDirection = 0.3f;

        if (direction == Vector2.zero)
        {
            direction.Set(-1f, 0f);
        }
        else
        {
            direction.x *= -1f;
        }

        if (LookAtClosestTarget() != Vector2.zero)
        {
            direction = LookAtClosestTarget();
        }

        fsm.Agent.Input.CallOnMovementVector(direction);

        isTouchingWall = false;
        isGroundAhead = true;
    }

    internal override void LogicUpdate()
    {
        base.LogicUpdate();
        fsm.Agent.MovementData.ReduceMoveTimeDurationBySeconds(Time.deltaTime);
        timeBeforeCanChangeDirection -= Time.deltaTime;

        if (fsm.Agent.CollissionSenses.IsTouchingWall && timeBeforeCanChangeDirection <= 0f)
        {
            direction.x *= -1f;
            pathBlockedAmount--;
            isTouchingWall = true;
            timeBeforeCanChangeDirection = 0.3f;
        }

        if (!fsm.Agent.CollissionSenses.IsThereGroundAhead && !isTouchingWall && timeBeforeCanChangeDirection <= 0f) 
        {
            direction.x *= -1f;
            pathBlockedAmount--;
            isGroundAhead = false;
            timeBeforeCanChangeDirection = 0.3f;
        }

        if (!fsm.Agent.CollissionSenses.IsTouchingWall)
        {
            isTouchingWall = false;
        }
        if (fsm.Agent.CollissionSenses.IsThereGroundAhead)
        {
            isGroundAhead = true;
        }

        if (pathBlockedAmount <= 0)
        {
            fsm.Agent.MovementData.SetMoveDuration(0f);
            direction.x *= -1f;
        }

        fsm.Agent.Input.CallOnMovementVector(direction);

        fsm.Agent.AgentWeapon.CheckIfTargetInRange(fsm.Agent.Data.HittableLayerMask);

        if (fsm.Agent.AgentWeapon.GetCurrentWeapon() != null && fsm.Agent.AgentWeapon.IsTargetInRange)
        {
            fsm.Agent.Input.CallOnAttackPressed();
        }

        HandleAttackTransition();
    }

    internal override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.Input.CallOnMovementVector(Vector2.zero);
        fsm.Agent.MovementData.SetCurrentSpeed(0f);
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }
}
