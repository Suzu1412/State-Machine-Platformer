using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Roll Duration", fileName = "Decision_RollDuration_")]
public class RollDurationDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(agent.MovementData.RollDuration);
    }
}
