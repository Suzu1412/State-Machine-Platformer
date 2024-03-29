using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool IsOnBottomOfTopLadder => topLadderDetector.IsOnBottom;
    public bool IsThereGroundAhead => groundAheadDetector.IsThereGroundAhead;

    public Collider2D Ladder => climbingDetector.Ladder;

    public Collider2D TopLadder => topLadderDetector.TopLadder;

    private void Awake()
    {
        if (agentColliderStanding == null) Debug.LogError("Agent Collider Standing is Empty in: " + this.name);
        if (agentColliderCrouching == null) Debug.LogError("Agent Collider Crouching is Empty in: " + this.name);
        if (collissionData == null) Debug.LogError("Agent Collission Data is Empty in: " + this.name);

        groundDetector = GetComponent<GroundDetector>();
        wallDetector = GetComponent<WallDetector>();
        climbingDetector = GetComponent<ClimbingDetector>();
        topLadderDetector = GetComponent<TopLadderDetector>();
        groundAheadDetector = GetComponent<GroundAheadDetector>();
        targetDetector = GetComponent<TargetDetector>();

        if (groundDetector == null) Debug.LogError("Ground Detector is Missing in: " + this.name);
        if (wallDetector == null) Debug.LogError("Wall Detector is Missing in: " + this.name);
        if (climbingDetector == null) Debug.LogError("Climbing Detector is Missing in: " + this.name);
        if (topLadderDetector == null) Debug.LogError("Top Ladder Detector is Missing in: " + this.name);

        SetAgentCollider(false);
        SetCollissionData();
    }

    public void SetAgentCollider(bool isCrouching)
    {
        if (!isCrouching)
        {
            agentCollider = agentColliderStanding;

            agentColliderStanding.enabled = true;
            agentColliderCrouching.enabled = false;
        }
        else
        {
            agentCollider = agentColliderCrouching;

            agentColliderStanding.enabled = false;
            agentColliderCrouching.enabled = true;
        }

        groundDetector.SetCollider(agentCollider);
        wallDetector.SetCollider(agentCollider);
        climbingDetector.SetCollider(agentCollider);
        TopLadderDetector.SetCollider(agentCollider);
    }

    private void SetCollissionData()
    {
        groundDetector.SetCollissionData(collissionData);
        wallDetector.SetCollissionData(collissionData);
        climbingDetector.SetCollissionData(collissionData);
        TopLadderDetector.SetCollissionData(collissionData);
        if (groundAheadDetector != null) groundAheadDetector.SetCollissionData(collissionData);
        if (targetDetector != null) targetDetector.SetCollissionData(collissionData); 
    }

    public void DetectGround()
    {
        groundDetector.CheckIsGrounded();
    }

    public void DetectGroundWhileClimbing()
    {
        groundDetector.CheckIsGroundedWhileClimbing();
    }

    public void DetectWall()
    {
        wallDetector.CheckIsTouchingWall();
    }

    public void DetectLadder()
    {
        climbingDetector.CheckIfCanClimb();
    }

    public void DetectIfOnTopOfLadder()
    {
        topLadderDetector.CheckIfOnTop();
    }

    public void DetectIfOnBottomOfLadder()
    {
        topLadderDetector.CheckIfOnBottom();
    }

    public void DetectGroundAhead()
    {
        groundAheadDetector.CheckIfThereIsPathAhead();
    }
}
