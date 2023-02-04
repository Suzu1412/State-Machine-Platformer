using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChooseAttackState : State
{
    private Vector2 direction;

    private void OnValidate()
    {
        stateType = StateType.Attack;
    }

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();

        ChooseCurrentWeapon();

        LookAtClosestTarget();

        direction = transform.parent.parent.transform.right * (transform.parent.parent.transform.localScale.x > 0 ? 1 : -1);

        fsm.Agent.Input.CallOnAttackPressed();
    }

    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
    }

    protected override void OnAttackEnd()
    {
    }

    private void ChooseCurrentWeapon()
    {
        //fsm.Agent.AgentWeapon.SwapWeapon()
    }
}
