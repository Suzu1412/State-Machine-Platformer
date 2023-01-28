using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Roll Input Pressed", fileName = "Decision_InputRollPressed")]
public class InputRollPressedDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.Input.RollPressed;
    }
}
