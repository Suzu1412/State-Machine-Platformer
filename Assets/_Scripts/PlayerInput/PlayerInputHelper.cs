using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHelper : MonoBehaviour, IAgentInput
{
    [SerializeField] private PlayerInputSO input;

    public Vector2 MovementVector => input.MovementVector;

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
        input.OnAttackPressed += () => OnAttackPressed?.Invoke();
        input.OnJumpPressed += () => OnJumpPressed?.Invoke();
        input.OnJumpReleased += () => OnJumpReleased?.Invoke();
        input.OnRollPressed += () => OnRollPressed?.Invoke();
        input.OnRollReleased += () => OnRollReleased?.Invoke();
        input.OnMovement += (vector) => OnMovement?.Invoke(vector);
        input.OnMenu += () => OnMenuPressed?.Invoke(); 
    }
}
