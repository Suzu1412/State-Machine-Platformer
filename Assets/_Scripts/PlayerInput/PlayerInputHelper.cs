using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHelper : MonoBehaviour, IAgentInput
{
    [SerializeField] private PlayerInputSO input;

    public Vector2 MovementVector { get; private set; }

    public event Action OnAttackPressed;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action<Vector2> OnMovement;
    public event Action OnWeaponChange;
    public event Action OnRollPressed;
    public event Action OnRollReleased;

    public UnityEvent OnMenuPressed;

    private void Awake()
    {
        input.ResetEvents();
        input.OnAttackPressed += CallOnAttackPressed;
        input.OnJumpPressed += CallOnJumpPressed;
        input.OnJumpReleased += CallOnJumpReleased;
        input.OnRollPressed += CallOnRollPressed;
        input.OnRollReleased += CallOnRollReleased;
        input.OnWeaponChange += CallOnWeaponChange;
        input.OnMovement += CallOnMovementVector;
        input.OnMenu += () => OnMenuPressed?.Invoke(); 
    }

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
