using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionsSO : ScriptableObject
{
    protected abstract void DoActions(Agent agent);
}
