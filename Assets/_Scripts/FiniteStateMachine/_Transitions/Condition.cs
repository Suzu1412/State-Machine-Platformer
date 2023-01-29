using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] private string name;
    [SerializeField] private DecisionSO decision;
    [SerializeField] private ResultType expectedResult;

    internal bool IsMet(Agent agent)
    {
        if (decision.Decide(agent) && expectedResult == ResultType.False)
        {
            return false;
        }
        else if (!decision.Decide(agent) && expectedResult == ResultType.True)
        {
            return false;
        }

        return true;
    }
}
