using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirFallAttack : FallState
{
    public UnityEvent OnAttack;
    private Vector2 direction;
    private bool debugGizmos;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => OnAttack?.Invoke());
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => OnAttackTrigger());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());

        direction = fsm.Agent.transform.right * (fsm.Agent.transform.localScale.x > 0 ? 1 : -1);
        debugGizmos = true;
    }

    public override void LogicUpdate()
    {
        CalculateVelocity();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void ExitState()
    {
        base.ExitState();
        fsm.Agent.AnimationManager.ResetEvents();
        debugGizmos = false;
    }


    private void OnAttackEnd()
    {
        if (fsm.Agent.MovementData.JumpDuration > 0f)
        {
            fsm.TransitionToState(StateType.Jump);
        }
        else
        {
            fsm.TransitionToState(StateType.Fall);
        }
    }


    private void OnAttackTrigger()
    {
        //fsm.Agent.AgentWeapon.GetCurrentWeapon().PerformAttack();
    }

    private void OnDrawGizmos()
    {
        if (!debugGizmos) return;

        fsm.Agent.AgentWeapon.GetCurrentWeapon().DrawWeaponGizmos(this.transform.position, direction);
    }
}
