using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Jump Input Pressed", fileName = "Decision_InputJumpPressed")]
public class InputJumpPressedDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.Input.JumpPressed;
    }
}
