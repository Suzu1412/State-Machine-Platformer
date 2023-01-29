using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Is Climbing", fileName = "Decision_IsClimbing")]
public class ClimbingDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.GroundDetector.IsClimbing;
    }
}
