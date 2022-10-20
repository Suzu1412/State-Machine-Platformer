using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : MonoBehaviour
{
    private int horizontalMovementDirection;
    private float currentSpeed = 0;
    private Vector2 currentVelocity = Vector2.zero;

    public int HorizontalMovementDirection => horizontalMovementDirection;
    public float CurrentSpeed => currentSpeed;
    public Vector2 CurrentVelocity => currentVelocity;

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
}
