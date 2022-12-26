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
    private CollissionSenses collissionSenses;
    private MovementData movementData;
    private Respawn respawn;
    private AgentWeaponManager agentWeapon;
    [SerializeField] private AgentDataSO data;
    [SerializeField] private LayerMask hittableLayerMask;

    public Rigidbody2D Rb2d => rb2d;
    public IAgentInput Input => input;
    public AgentRenderer RendererManager => rendererManager;
    public AgentAnimation AnimationManager => animationManager;
    public CollissionSenses CollissionSenses => collissionSenses;
    public MovementData MovementData => movementData;
    public Respawn Respawn => respawn;
    public AgentWeaponManager AgentWeapon => agentWeapon;
    public AgentDataSO Data => data;
    public LayerMask HittableLayerMask => hittableLayerMask;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponentInParent<IAgentInput>();
        rendererManager = GetComponentInChildren<AgentRenderer>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        collissionSenses = GetComponentInChildren<CollissionSenses>();
        movementData = GetComponent<MovementData>();
        respawn = GetComponent<Respawn>();
        agentWeapon = GetComponentInChildren<AgentWeaponManager>();

        if (data == null) Debug.LogError("Agent Data is Empty in: " + this.name);
    }


}
