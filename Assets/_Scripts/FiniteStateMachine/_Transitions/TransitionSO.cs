using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Transition", fileName = "Transition_")]
public class TransitionSO : ScriptableObject
{
    [SerializeField] private Condition[] conditions;
    [SerializeField] private StateType newStateType;
    [SerializeField] private bool debug;
    private bool conditionsMet;

    internal void TransitionToNewState(FiniteStateMachine fsm)
    {
        conditionsMet = false;

        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].IsMet(fsm.Agent))
            {
                conditionsMet = true;
            }
            else
            {
                conditionsMet = false;
                break;
            }
        }

        if (!conditionsMet) return;

        if (fsm.CurrentState != null && fsm.CurrentStateType == newStateType)
        {
            Debug.LogWarning(fsm.transform.root.name + " Trying to Transition to Same State. Fix Transition From: " + this.name);
            return;
        }

        if (debug)
        {
            Debug.Log("Transition From: " + fsm.CurrentStateType + " To: " + newStateType + " Using: " + this.name);
        }

        fsm.TransitionToState(newStateType);
    }
}
