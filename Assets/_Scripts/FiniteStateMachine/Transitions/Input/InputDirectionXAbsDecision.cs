using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Input X Abs", fileName = "Decision_InputX_Abs_")]
public class InputDirectionXAbsDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(Mathf.Abs(agent.Input.MovementVector.x));
    }
}
