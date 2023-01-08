using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyranossaurusIdle : State
{
    private float idleDuration = 1f;
    private float idleDurationTimeLeft;
    [SerializeField] private BossPhaseType phaseType;

    [SerializeField] private bool debugPhase;


    protected override void EnterState()
    {
        idleDurationTimeLeft = idleDuration;

        if (debugPhase) return;

        if (fsm.Agent.HealthSystem.GetHealthPercent() > 0.7f)
        {
            phaseType = BossPhaseType.Phase1;
        }
        else if (fsm.Agent.HealthSystem.GetHealthPercent() >= 0.33f &&
            (fsm.Agent.HealthSystem.GetHealthPercent() <= 0.7f))
        {
            phaseType = BossPhaseType.Phase2;
        }
        else if (fsm.Agent.HealthSystem.GetHealthPercent() < 0.33f)
        {
            phaseType = BossPhaseType.Phase3;
        }
    }

    public override void LogicUpdate()
    {
        idleDurationTimeLeft -= Time.deltaTime;

        if (idleDurationTimeLeft <= 0f)
        {
            PhaseManager();
        }
    }

    private void PhaseManager()
    {
        switch (phaseType)
        {
            case BossPhaseType.Phase1:
                //if (player is close)
                fsm.TransitionToState(StateType.AIAttacking);



                break;
            case BossPhaseType.Phase2:
                break;
            case BossPhaseType.Phase3:
                break;
            case BossPhaseType.DesperationAttack:
                break;
        }
    }
}
