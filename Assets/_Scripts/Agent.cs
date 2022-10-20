using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private IAgentInput input;
    [SerializeField] private AgentDataSO data;

    public Rigidbody2D Rb2d => rb2d;
    public IAgentInput Input => input;
    public AgentDataSO Data => data;

    [SerializeField] private State currentState, initialState;
    private State previousState;
    private State[] states;

    [Header("State Debugging:")]
    private string stateName = "";

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponentInParent<IAgentInput>();

        states = GetComponentsInChildren<State>();

        foreach (var state in states)
        {
            state.InitializeState(this);
        }
    }

    private void OnEnable()
    {
        TransitionToState(initialState);
    }

    public void TransitionToState(State newState)
    {
        if (newState == null) return;

        if (currentState != null) currentState.Exit();

        previousState = currentState;
        currentState = newState;
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

    private void Update()
    {
        if (currentState != null) currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null) currentState.StateFixedUpdate();
    }
}
