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
    private bool canEnterCoyoteTime;

    public int HorizontalMovementDirection => horizontalMovementDirection;
    public float CurrentSpeed => currentSpeed;
    public Vector2 CurrentVelocity => currentVelocity;

    public int AmountOfJumps => amountOfJumps;
    public bool IsJumping => isJumping;
    public bool CanEnterCoyoteTime => canEnterCoyoteTime;

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

    public bool ActivateJump()
    {
        if (amountOfJumps <= 0) return false;

        canEnterCoyoteTime = false;
        isJumping = true;
        ConsumeJump();

        return true;
    }

    public void ResetJump(Agent agent)
    {
        if (!agent.GroundDetector.IsGrounded) return;

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
}
