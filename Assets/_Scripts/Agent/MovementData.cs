using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : MonoBehaviour
{
    private int horizontalMovementDirection;
    private float currentSpeed = 0;
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField] private int amountOfJumps;
    [SerializeField] private bool isJumping;
    private float jumpDuration;

    [SerializeField] private bool canEnterCoyoteTime;
    private bool isInCoyoteTime;
    private float coyoteTimeDuration;

    public int HorizontalMovementDirection => horizontalMovementDirection;
    public float CurrentSpeed => currentSpeed;
    public Vector2 CurrentVelocity => currentVelocity;

    public int AmountOfJumps => amountOfJumps;
    public bool IsJumping => isJumping;
    public bool CanEnterCoyoteTime { get => canEnterCoyoteTime; set => canEnterCoyoteTime = value; }
    public bool IsInCoyoteTime { get => isInCoyoteTime; set => isInCoyoteTime = value; }
    public float JumpDuration { get => jumpDuration; set => jumpDuration = value; }
    public float CoyoteTimeDuration { get => coyoteTimeDuration; set => coyoteTimeDuration = value; }

    public void SetHorizontalMovementDirection(int horizontalMovementDirection)
    {
        this.horizontalMovementDirection = horizontalMovementDirection;
    }

    public void SetCurrentSpeed(float currentSpeed)
    {
        this.currentSpeed = currentSpeed;
    }

    public void SetCurrentVelocity(Vector2 currentVelocity)
    {
        this.currentVelocity = currentVelocity;
    }

    public void ActivateJump()
    {
        canEnterCoyoteTime = false;
        isJumping = true;
        ConsumeJump();
    }

    public void ResetJump(Agent agent)
    {
        isJumping = false;
        canEnterCoyoteTime = true;
        amountOfJumps = agent.Data.AmountOfJumps;
    }
    public void ConsumeJump()
    {
        if (amountOfJumps > 0)
        {
            amountOfJumps--;
        }
    }

    public void ReduceJumpDurationByTime(float time)
    {
        jumpDuration -= time;
    }

    public void ReduceCoyoteTimeDurationByTime(float time)
    {
        coyoteTimeDuration -= time;
    }
}
