using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAttackingState : State
{
    public UnityEvent<AudioClip> OnWeaponSound;
    private Vector2 direction;
    private bool showGizmos;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());

        // fsm.Agent.AgentWeapon.ToggleWeaponVisibility(true);

        direction = fsm.Agent.transform.right * (fsm.Agent.transform.localScale.x > 0 ? 1 : -1);
        showGizmos = true;

        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
    }

    protected override void ExitState()
    {
        showGizmos = false;
        fsm.Agent.AnimationManager.ResetEvents();
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AgentWeapon.PerformAttack(fsm.Agent.Data.HittableLayerMask);
    }

    private void OnAttackEnd()
    {
        if (fsm.PreviousState != StateType.Hit)
        {
            fsm.TransitionToState(fsm.PreviousState);
        }
        else
        {
            fsm.TransitionToState(StateType.Idle);
        }
        
    }
}
