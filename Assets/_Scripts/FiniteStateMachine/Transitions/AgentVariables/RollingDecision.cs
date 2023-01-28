using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Is Rolling", fileName = "Decision_IsRolling")]
public class RollingDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.MovementData.IsRolling;
    }
}
