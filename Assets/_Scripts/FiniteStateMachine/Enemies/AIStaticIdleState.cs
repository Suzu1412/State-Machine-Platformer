using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStaticIdleState : State
{
    private Vector2 direction;

    private void OnValidate()
    {
        stateType = StateType.Idle;
    }

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    internal override void LogicUpdate()
    {
        if (fsm.Agent.CollissionSenses.TargetDetector.Target == null) return;


        if (fsm.Agent.CollissionSenses.TargetDetector.DirectionToTarget.x > 1f)
        {
            direction.Set(1f, 0f);
            fsm.Agent.Input.CallOnMovementVector(direction);
        }
        else if (fsm.Agent.CollissionSenses.TargetDetector.DirectionToTarget.x < 1f)
        {
            direction.Set(-1f, 0f);
            fsm.Agent.Input.CallOnMovementVector(direction);
        }
    }

    internal override void PhysicsUpdate()
    {
    }
}
