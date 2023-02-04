using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Idle Duration", fileName = "Decision_IdleDuration_")]
public class IdleDurationDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare((agent.MovementData.IdleDuration / agent.Data.IdleTime) * 100);
    }
}
