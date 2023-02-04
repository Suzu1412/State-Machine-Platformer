using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHelper : MonoBehaviour, IAgentInput
{
    [SerializeField] private PlayerInputSO input;
    private Coroutine resetJumpCoroutine;
    private Coroutine resetRollCoroutine;
    private Coroutine resetAttackCoroutine;
    private WaitForSeconds waitForSeconds = new(0.05f);


    public Vector2 MovementVector { get; private set; }

    public event Action OnAttackPressed;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action<Vector2> OnMovement;
    public event Action OnWeaponChange;
    public event Action OnRollPressed;
    public event Action OnRollReleased;

    public UnityEvent OnMenuPressed;

    private bool canCleanInput;

    public bool JumpPressed { get; private set; }
    public bool JumpHold { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool RollPressed { get; private set; }
    public bool RollReleased { get; private set; }

    public bool AttackPressed { get; private set; }

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
        resetAttackCoroutine = StartCoroutine(ResetAttackCoroutine());
        OnAttackPressed?.Invoke();
    }

    public void CallOnJumpPressed()
    {
        resetJumpCoroutine = StartCoroutine(ResetJumpCoroutine());
        JumpReleased = false;
        OnJumpPressed?.Invoke();
    }

    public void CallOnJumpReleased()
    {
        StopCoroutine(ResetJumpCoroutine());
        JumpPressed = false;
        JumpHold = false;
        JumpReleased = true;
        OnJumpReleased?.Invoke();
    }

    public void CallOnWeaponChange()
    {
        OnWeaponChange?.Invoke();
    }

    public void CallOnRollPressed()
    {
        resetRollCoroutine = StartCoroutine(ResetRollCoroutine());
        RollReleased = false;
        OnRollPressed?.Invoke();
    }

    public void CallOnRollReleased()
    {
        StopCoroutine(ResetRollCoroutine());
        RollPressed = false;
        RollReleased = true;
        OnRollReleased?.Invoke();
    }

    #region Coroutines
    private IEnumerator ResetJumpCoroutine()
    {
        JumpPressed = true;
        yield return waitForSeconds;
        JumpPressed = false;
    }
    
    private IEnumerator ResetRollCoroutine()
    {
        RollPressed = true;
        yield return waitForSeconds;
        RollPressed = false;
    }

    private IEnumerator ResetAttackCoroutine()
    {
        AttackPressed = true;
        yield return waitForSeconds;
        AttackPressed = false;
    }

    #endregion
}
