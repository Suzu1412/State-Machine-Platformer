using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Rigidbody Velocity X Abs", fileName = "Decision_RigidbodyVelocityXAbs_")]
public class RigidbodyVelocityXAbsDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(Mathf.Abs(agent.Rb2d.velocity.x));
    }
}
