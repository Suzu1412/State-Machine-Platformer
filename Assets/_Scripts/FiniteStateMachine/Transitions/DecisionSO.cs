using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionSO : ScriptableObject
{
    internal abstract bool Decide(Agent agent);
}