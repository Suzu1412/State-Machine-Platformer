using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    internal State PreviousState => previousState;
    internal State CurrentState => currentState;
    internal StateType PreviousStateType => previousState.StateType;
    internal StateType CurrentStateType => currentState.StateType;

    private void Awake()
    {
        agent = GetComponentInParent<Agent>();
        stateFactory = GetComponent<StateFactory>();
        states = GetComponents<State>();

        if (agent == null) Debug.LogError("Agent missing in: " + this.name);
        if (stateFactory == null) Debug.LogError("State Factory missing in: " + this.name);
        if (states == null) Debug.LogError("States missing in: " + this.name);

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
        if (currentState != null) currentState.TransitionToState(this);
    }

    private void FixedUpdate()
    {
        if (currentState != null) currentState.PhysicsUpdate();
    }

    public void TransitionToState(StateType newState)
    {
        State state = stateFactory.GetState(newState);

        if (state == null)
        {
            Debug.LogError(gameObject.name + " is missing the state: " + newState.ToString());
            return;
        }

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
