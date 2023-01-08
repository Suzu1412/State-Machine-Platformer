using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State 
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

        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
        fsm.Agent.CollissionSenses.DetectLadder();
    }

    protected override void ExitState()
    {
        showGizmos = false;
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(false);
    }

    protected override void HandleJumpPressed()
    {
    }

    protected override void HandleAttackPressed()
    {
    }

    protected override void HandleRollPressed()
    {
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AgentWeapon.PerformAttack(fsm.Agent.Data.HittableLayerMask);
    }

    private void OnAttackEnd()
    {
        if (Mathf.Abs(fsm.Agent.Input.MovementVector.x) > 0f &&
            !fsm.Agent.CollissionSenses.IsTouchingWall)
        {
            fsm.TransitionToState(StateType.Move);
        }
        else
        {
            fsm.TransitionToState(StateType.Idle);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (Application.isPlaying == false) return;

    //    if (!showGizmos) return;

    //    fsm.Agent.AgentWeapon.GetCurrentWeapon().DrawWeaponGizmos(fsm.Agent.AgentWeapon.transform.position, direction);
    //}
}
