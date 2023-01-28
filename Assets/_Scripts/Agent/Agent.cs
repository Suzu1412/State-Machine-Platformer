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
    private HealthSystem healthSystem;
    private HittableKnockback knockbackSystem;
    
    [SerializeField] private AgentDataSO data;

    public Rigidbody2D Rb2d => rb2d;
    public IAgentInput Input => input;
    public AgentRenderer RendererManager => rendererManager;
    public AgentAnimation AnimationManager => animationManager;
    public CollissionSenses CollissionSenses => collissionSenses;
    public MovementData MovementData => movementData;
    public Respawn Respawn => respawn;
    public AgentWeaponManager AgentWeapon => agentWeapon;
    public AgentDataSO Data => data;
    public HealthSystem HealthSystem => healthSystem;
    public HittableKnockback KnockbackSystem => knockbackSystem;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponentInParent<IAgentInput>();
        rendererManager = GetComponentInChildren<AgentRenderer>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        collissionSenses = GetComponentInChildren<CollissionSenses>();
        movementData = GetComponent<MovementData>();
        respawn = GetComponentInParent<Respawn>();
        agentWeapon = GetComponentInChildren<AgentWeaponManager>();
        healthSystem = GetComponent<HealthSystem>();
        knockbackSystem = GetComponent<HittableKnockback>();

        if (rb2d == null) Debug.LogError("Agent RB missing in: " + this.name);
        if (input == null) Debug.LogError("Agent Input missing in: " + this.name);
        if (rendererManager == null) Debug.LogError("Agent Renderer Manager missing in: " + this.name);
        if (animationManager == null) Debug.LogError("Agent Animation Manager missing in: " + this.name);
        if (collissionSenses == null) Debug.LogError("Agent Collission Senses missing in: " + this.name);
        if (movementData == null) Debug.LogError("Agent Movement Data missing in: " + this.name);
        if (agentWeapon == null) Debug.LogError("Agent Weapon missing in: " + this.name);
        if (healthSystem == null) Debug.LogError("Agent Health System missing in: " + this.name);
        if (knockbackSystem == null) Debug.LogError("Agent Knockback System missing in: " + this.gameObject.name);

        if (data == null) Debug.LogError("Agent Data is Empty in: " + this.name);
    }

    private void OnEnable()
    {
        healthSystem.Initialize(data.Health, data.InvulnerabilityDuration, data.HitStunDuration);
    }     
}
