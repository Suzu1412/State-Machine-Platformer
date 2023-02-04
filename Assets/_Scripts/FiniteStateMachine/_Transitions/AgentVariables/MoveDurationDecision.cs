using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Move Duration", fileName = "Decision_MoveDuration_")]
public class MoveDurationDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(agent.MovementData.MoveDuration);
    }
}
