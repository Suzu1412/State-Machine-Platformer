using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Is Ground Ahead", fileName = "Decision_IsGroundAhead")]

public class GroundAheadDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.IsThereGroundAhead;
    }
}
