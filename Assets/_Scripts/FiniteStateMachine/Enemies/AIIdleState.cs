using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : State
{
    private Vector2 direction = Vector2.zero;

    private void OnValidate()
    {
        stateType = StateType.Idle;
    }

    internal override void EnterState()
    {
        if (direction == Vector2.zero)
        {
            direction.Set(-1f, 0f);
            fsm.Agent.Input.CallOnMovementVector(direction);
        }
        else
        {
            direction = transform.parent.parent.transform.right * (transform.parent.parent.transform.localScale.x > 0 ? 1 : -1);
        }

        fsm.Agent.MovementData.SetIdleDuration(fsm.Agent.Data.IdleTime);

        if (!fsm.Agent.AgentWeapon.IsAttacking)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        }

        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.OnAnimationAttackPerformed.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
    }

    internal override void LogicUpdate()
    {
        fsm.Agent.MovementData.ReduceIdleTimeDurationBySeconds(Time.deltaTime);

        fsm.Agent.AgentWeapon.CheckIfTargetInRange(fsm.Agent.Data.HittableLayerMask);

        HandleAttackTransition();
    }

    internal override void ExitState()
    {
        fsm.Agent.MovementData.SetIdleDuration(0f);

        fsm.Agent.AnimationManager.OnAnimationAttackPerformed?.RemoveListener(PerformAttack);
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
        fsm.Agent.AnimationManager.ResetEvents();
    }

    protected override void OnAttackEnd()
    {
        base.OnAttackEnd();

        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }
}
