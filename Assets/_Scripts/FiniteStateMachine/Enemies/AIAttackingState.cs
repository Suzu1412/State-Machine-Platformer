using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAttackingState : State
{
    private Vector2 direction;

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        fsm.Agent.AnimationManager.OnAnimationAttackPerformed.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());

        direction = fsm.Agent.transform.right * (fsm.Agent.transform.localScale.x > 0 ? 1 : -1);

        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    internal override void PhysicsUpdate()
    {
    }

    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
    }

    protected override void OnAttackEnd()
    {
        if (fsm.PreviousStateType != StateType.Hit)
        {
            fsm.TransitionToState(fsm.PreviousStateType);
        }
        else
        {
            fsm.TransitionToState(StateType.Idle);
        }
    }
}
