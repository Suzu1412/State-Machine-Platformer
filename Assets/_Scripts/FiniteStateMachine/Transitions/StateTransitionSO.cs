using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Transition DataBase", fileName = "StateTransition_")]
public class StateTransitionSO : ScriptableObject
{
    [SerializeField] private TransitionSO[] transitions;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (transitions != null && transitions.Length > 1)
        {
            Array.Sort(transitions, delegate(TransitionSO x, TransitionSO y) { return x.name.CompareTo(y.name); });
        }
    }
    #endif

    internal void HandleTransition(FiniteStateMachine fsm)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].TransitionToNewState(fsm);
        }
    }
}
