using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "PlayerHelpers/Player Input")]
public class PlayerInputSO : ScriptableObject, PlayerInputConfig.IGameplayActions, 
    PlayerInputConfig.IPauseMenuActions, IAgentInput
{
    PlayerInputConfig input;

    public event Action OnJumpPressed;
    public event Action<Vector2> OnMovement;
    public event Action OnJumpReleased;
    public event Action OnWeaponChange;
    public event Action OnRollPressed;
    public event Action OnRollReleased;

    public Vector2 MovementVector { get; private set; }

    public event Action OnMenu;
    public event Action OnAttackPressed;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new PlayerInputConfig();
            input.Gameplay.SetCallbacks(this);
            input.PauseMenu.SetCallbacks(this);

            input.Gameplay.Enable();
        }
    }

    private void OnDisable()
    {
        input = null;
        ResetEvents();
    }

    public void ResetEvents()
    {
        OnMenu = null;
        OnAttackPressed = null;
        OnJumpPressed = null;
        OnJumpReleased = null;
        OnMovement = null;
        OnWeaponChange = null;
        OnRollPressed = null;
    }

    public void OnMoveAgent(InputAction.CallbackContext context)
    {
        MovementVector = context.ReadValue<Vector2>();
        OnMovement?.Invoke(MovementVector);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpPressed?.Invoke();
        }
        else if (context.canceled)
        {
            OnJumpReleased?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) OnAttackPressed?.Invoke();
    }

    public void OnWeaponSwap(InputAction.CallbackContext context)
    {
        if (context.started) OnWeaponChange?.Invoke();
    }

    public void OnEnterMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnMenu?.Invoke();
            input.Gameplay.Disable();
            input.PauseMenu.Enable();
        }
    }

    public void OnExitMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnMenu?.Invoke();
            input.PauseMenu.Disable();
            input.Gameplay.Enable();
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRollPressed?.Invoke();
        }
        else if (context.canceled)
        {
            OnRollReleased?.Invoke();
        }
    }
}
