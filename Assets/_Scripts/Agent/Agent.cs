using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Agent : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private IAgentInput input;
    private AgentRenderer rendererManager;
    private AgentAnimation animationManager;
    private GroundDetector groundDetector;
    private WallDetector wallDetector;
    private ClimbingDetector climbingDetector;
    private TopLadderDetector topLadderDetector;
    private MovementData movementData;
    private Collider2D agentCollider;
    [SerializeField] private AgentDataSO data;
    [SerializeField] private CollissionSensesDataSO collissionData;

    public Rigidbody2D Rb2d => rb2d;
    public IAgentInput Input => input;
    public AgentRenderer RendererManager => rendererManager;
    public AgentAnimation AnimationManager => animationManager;
    public GroundDetector GroundDetector => groundDetector;
    public WallDetector WallDetector => wallDetector;
    public ClimbingDetector ClimbingDetector => climbingDetector;
    public TopLadderDetector TopLadderDetector => topLadderDetector;
    public MovementData MovementData => movementData;
    public Collider2D AgentCollider => agentCollider;
    public AgentDataSO Data => data;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponentInParent<IAgentInput>();
        rendererManager = GetComponentInChildren<AgentRenderer>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        groundDetector = GetComponentInChildren<GroundDetector>();
        wallDetector = GetComponentInChildren<WallDetector>();
        climbingDetector = GetComponentInChildren<ClimbingDetector>();
        topLadderDetector = GetComponentInChildren<TopLadderDetector>();
        agentCollider = GetComponentInChildren<Collider2D>();
        movementData = GetComponent<MovementData>();

        if (data == null) Debug.LogError("Agent Data is Empty in: " + this.name);
        if (collissionData == null) Debug.LogError("Agent Collission Data is Empty in: " + this.name);

        groundDetector.SetCollissionData(collissionData);
        wallDetector.SetCollissionData(collissionData);
        climbingDetector.SetCollissionData(collissionData);
        TopLadderDetector.SetCollissionData(collissionData);
    }
    
}
