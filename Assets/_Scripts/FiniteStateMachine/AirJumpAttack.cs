using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirJumpAttack : JumpState
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
        base.LogicUpdate();

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

    protected override void HandleAttackPressed()
    {
    }

    protected override void HandleJumpReleased()
    {
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, 0f));

        fsm.TransitionToState(StateType.AirFallAttack);
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
