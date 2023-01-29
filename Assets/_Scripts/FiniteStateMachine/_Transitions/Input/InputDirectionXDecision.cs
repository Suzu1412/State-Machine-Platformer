using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Input X", fileName = "Decision_InputX_")]
public class InputDirectionXDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(agent.Input.MovementVector.x);
    }
}
