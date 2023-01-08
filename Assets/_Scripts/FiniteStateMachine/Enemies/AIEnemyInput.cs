using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyInput : MonoBehaviour, IAgentInput
{
    public Vector2 MovementVector { get; private set; }

    public event Action OnAttackPressed;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action<Vector2> OnMovement;
    public event Action OnWeaponChange;
    public event Action OnRollPressed;
    public event Action OnRollReleased;

    public void CallOnMovementVector(Vector2 input)
    {
        MovementVector = input;
        OnMovement?.Invoke(input);
    }

    public void CallOnAttackPressed()
    {
        OnAttackPressed?.Invoke();
    }

    public void CallOnJumpPressed()
    {
        OnJumpPressed?.Invoke();
    }

    public void CallOnJumpReleased()
    {
        OnJumpReleased?.Invoke();
    }

    public void CallOnWeaponChange()
    {
        OnWeaponChange?.Invoke();
    }

    public void CallOnRollPressed()
    {
        OnRollPressed?.Invoke();
    }

    public void CallOnRollReleased()
    {
        OnRollReleased?.Invoke();
    }
}
