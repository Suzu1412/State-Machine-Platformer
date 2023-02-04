using UnityEngine;
using UnityEngine.Events;

public class IdleState : State
{
    private void OnValidate()
    {
        stateType = StateType.Idle;
    }

    internal override void EnterState()
    {
        base.EnterState();

        if (!fsm.Agent.AgentWeapon.IsAttacking)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        }
        
        fsm.Agent.Rb2d.velocity = Vector2.zero;

        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.OnAnimationAttackPerformed.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
    }

    internal override void LogicUpdate()
    {
        HandleAttackTransition();
    }

    internal override void PhysicsUpdate()
    {
    }

    internal override void ExitState()
    {
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
