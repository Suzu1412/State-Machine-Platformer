using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Amount Of Jumps Left", fileName = "Decision_AmountOfJumpsLeft")]
public class AmountOfJumpsDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.MovementData.AmountOfJumps > 0;
    }
}
