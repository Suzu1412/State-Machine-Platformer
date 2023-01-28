using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Touching Wall", fileName = "Decision_TouchingWall")]
public class TouchingWallDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.IsTouchingWall;
    }
}
