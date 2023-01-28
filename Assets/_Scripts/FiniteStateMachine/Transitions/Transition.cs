using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    [SerializeField] private StateTransitionSO stateTransition;

    public void TransitionToState(FiniteStateMachine fsm)
    {
        if (stateTransition == null) return;

        stateTransition.HandleTransition(fsm);
    }

}