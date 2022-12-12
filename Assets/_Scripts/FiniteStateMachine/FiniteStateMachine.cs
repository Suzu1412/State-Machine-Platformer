using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class FiniteStateMachine : MonoBehaviour
{
    private Agent agent;
    private StateFactory stateFactory;
    [SerializeField] private StateType initialState;
    private State currentState;
    private State previousState;
    private State[] states;

    [Header("State Debugging:")]
    [SerializeField] private string stateName = "";

    public Agent Agent => agent;
    //public StateFactory StateFactory => stateFactory;

    private void Awake()
    {
        agent = GetComponentInParent<Agent>();
        stateFactory = GetComponent<StateFactory>();
        states = GetComponents<State>();

        foreach (var state in states)
        {
            state.InitializeState(this);
        }
    }

    private void OnEnable()
    {
        if (agent == null) return;

        TransitionToState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null) currentState.PhysicsUpdate();
    }

    public void TransitionToState(StateType newState)
    {
        State state = stateFactory.GetState(newState);

        if (state == null) return;

        if (currentState != null) currentState.Exit();

        previousState = currentState;
        currentState = state;
        currentState.Enter();

        DisplayState();
    }

    private void DisplayState()
    {
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }

}
