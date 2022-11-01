using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class Agent : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private IAgentInput input;
    private AgentRenderer rendererManager;
    private AgentAnimation animationManager;
    private CollissionSenses senses;
    [SerializeField] private AgentDataSO data;

    public Rigidbody2D Rb2d => rb2d;
    public IAgentInput Input => input;
    public AgentRenderer RendererManager => rendererManager;
    public AgentAnimation AnimationManager => animationManager;
    public CollissionSenses Senses => senses;
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
        rendererManager = GetComponentInChildren<AgentRenderer>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        senses = GetComponentInChildren<CollissionSenses>();
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
