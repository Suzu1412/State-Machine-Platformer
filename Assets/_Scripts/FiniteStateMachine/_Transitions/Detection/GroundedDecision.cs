using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Is Grounded", fileName = "Decision_IsGrounded")]
public class GroundedDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.IsGrounded;
    }
}
