using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CollissionSenses : MonoBehaviour
{
    private Collider2D agentCollider;
    [SerializeField] private Collider2D agentColliderStanding;
    [SerializeField] private Collider2D agentColliderCrouching;
    [SerializeField] private CollissionSensesDataSO collissionData;

    #region Detectors
    private GroundDetector groundDetector;
    private WallDetector wallDetector;
    private ClimbingDetector climbingDetector;
    private TopLadderDetector topLadderDetector;
    private GroundAheadDetector groundAheadDetector;
    private TargetDetector targetDetector;

    #endregion
    public Collider2D AgentCollider => agentCollider;
    public GroundDetector GroundDetector => groundDetector;
    public WallDetector WallDetector => wallDetector;
    public ClimbingDetector ClimbingDetector => climbingDetector;
    public TopLadderDetector TopLadderDetector => topLadderDetector;
    public GroundAheadDetector GroundAheadDetector => groundAheadDetector;
    public TargetDetector TargetDetector => targetDetector;

    public bool IsGrounded => groundDetector.IsGrounded;
    public bool IsTouchingWall => wallDetector.IsTouchingWall;
    public bool IsTouchingLadder => climbingDetector.CanClimb;
    public bool IsBelowOfTopLadder => topLadderDetector.IsBelowLadder;
    public bool IsAboveOfTopLadder => topLadderDetector.IsAboveLadder;
    public bool IsThereGroundAhead => groundAheadDetector.IsThereGroundAhead;
    public bool IsTargetDetected => targetDetector.TargetDetected;

    public Collider2D Ladder => climbingDetector.Ladder;

    public Collider2D TopLadder => topLadderDetector.TopLadder;

    private void Awake()
    {
        if (collissionData == null) Debug.LogError("Agent Collission Data is Empty in: " + this.name);

        groundDetector = GetComponent<GroundDetector>();
        wallDetector = GetComponent<WallDetector>();
        climbingDetector = GetComponent<ClimbingDetector>();
        topLadderDetector = GetComponent<TopLadderDetector>();
        groundAheadDetector = GetComponent<GroundAheadDetector>();
        targetDetector = GetComponent<TargetDetector>();

        SetAgentCollider(false);
        SetCollissionData();
    }

    public void SetAgentCollider(bool isCrouching)
    {
        if (!isCrouching)
        {
            if (agentColliderStanding == null)
            {
                Debug.LogError("Agent Collider Standing is Empty in: " + this.name);
                return;
            }

            agentCollider = agentColliderStanding;
            agentColliderStanding.enabled = true;

            if (agentColliderCrouching != null) agentColliderCrouching.enabled = false;
        }
        else
        {
            if (agentColliderCrouching == null)
            {
                Debug.LogError("Agent Collider Crouching is Empty in: " + this.name);
            }

            agentCollider = agentColliderCrouching;

            agentColliderStanding.enabled = false;
            agentColliderCrouching.enabled = true;
        }

        if (groundDetector != null) groundDetector.SetCollider(agentCollider);
        if (wallDetector != null) wallDetector.SetCollider(agentCollider);
        if (climbingDetector != null) climbingDetector.SetCollider(agentCollider);
        if (TopLadderDetector != null) TopLadderDetector.SetCollider(agentCollider);
    }

    private void SetCollissionData()
    {
        if (groundDetector != null) groundDetector.SetCollissionData(collissionData);
        if (wallDetector != null) wallDetector.SetCollissionData(collissionData);
        if (climbingDetector != null) climbingDetector.SetCollissionData(collissionData);
        if (TopLadderDetector != null) TopLadderDetector.SetCollissionData(collissionData);
        if (groundAheadDetector != null) groundAheadDetector.SetCollissionData(collissionData);
        if (targetDetector != null) targetDetector.SetCollissionData(collissionData); 
    }
}
