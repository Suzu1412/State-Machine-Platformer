using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : MonoBehaviour
{
    [Header("Idle Variables")]
    private float idleDuration;

    [Header("Move Variables")]
    private int horizontalMovementDirection;
    private float currentSpeed = 0;
    private Vector2 currentVelocity = Vector2.zero;
    private float moveDuration;

    [Header("Jump Variables")]
    [SerializeField] private int amountOfJumps;
    [SerializeField] private bool isJumping;
    private float jumpDuration;

    [Header("Coyote Variables")]
    [SerializeField] private bool canEnterCoyoteTime;
    private bool isInCoyoteTime;
    private float coyoteTimeDuration;

    [Header("Rolling Variables")]
    [SerializeField] private bool isRolling;
    private float rollDuration;

    public int HorizontalMovementDirection => horizontalMovementDirection;
    public float CurrentSpeed => currentSpeed;
    public Vector2 CurrentVelocity => currentVelocity;
    public float MoveDuration => moveDuration;
    public int AmountOfJumps => amountOfJumps;
    public bool IsJumping => isJumping;
    public bool IsRolling => isRolling;
    public float IdleDuration => idleDuration;
    public bool CanEnterCoyoteTime => canEnterCoyoteTime;
    public bool IsInCoyoteTime => isInCoyoteTime;
    public float JumpDuration => jumpDuration;
    public float CoyoteTimeDuration => coyoteTimeDuration;
    public float RollDuration => rollDuration; 

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

    public void ActivateRoll()
    {
        isRolling = true;
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

    public void SetIdleDuration(float idleDuration)
    {
        this.idleDuration = idleDuration;
    }

    public void SetMoveDuration(float moveDuration)
    {
        this.moveDuration = moveDuration;
    }

    public void SetIsInCoyoteTime(bool isInCoyoteTime)
    {
        this.isInCoyoteTime = isInCoyoteTime;
    }

    public void SetJumpDuration(float jumpDuration)
    {
        this.jumpDuration = jumpDuration;
    }

    public void SetCoyoteTimeDuration(float coyoteTimeDuration)
    {
        this.coyoteTimeDuration = coyoteTimeDuration;
    }

    public void SetRollDuration(float rollDuration)
    {
        this.rollDuration = rollDuration;
    }

    public void ReduceJumpDurationBySeconds(float seconds)
    {
        jumpDuration -= seconds;

        if (jumpDuration <= 0f)
        {
            isJumping = false;
        }
    }

    public void ReduceIdleTimeDurationBySeconds(float seconds)
    {
        idleDuration -= seconds;
    }

    public void ReduceMoveTimeDurationBySeconds(float seconds)
    {
        moveDuration -= seconds;
    }

    public void ReduceCoyoteTimeDurationBySeconds(float seconds)
    {
        coyoteTimeDuration -= seconds;
    }

    public void ReduceRollDurationBySeconds(float seconds)
    {
        rollDuration -= seconds;

        if (rollDuration <= 0f)
        {
            isRolling = false;
        }
    }
}
