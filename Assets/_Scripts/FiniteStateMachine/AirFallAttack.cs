using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirFallAttack : FallState
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

        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(true);
        direction = fsm.Agent.transform.right * (fsm.Agent.transform.localScale.x > 0 ? 1 : -1);
        showGizmos = true;
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
        showGizmos = false;
        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(false);
    }

    protected override void HandleTransitionToStates()
    {
        if (fsm.Agent.CollissionSenses.IsGrounded && fsm.Agent.Rb2d.velocity.y == 0f)
        {
            fsm.TransitionToState(StateType.Attack);
        }
    }


    private void OnAttackEnd()
    {
        fsm.TransitionToState(StateType.Fall);
    }


    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AgentWeapon.GetCurrentWeapon().PerformAttack(fsm.Agent.AgentWeapon.transform, fsm.Agent.HittableLayerMask, direction);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;

        if (!showGizmos) return;

        fsm.Agent.AgentWeapon.GetCurrentWeapon().DrawWeaponGizmos(fsm.Agent.AgentWeapon.transform.position, direction);
    }
}
