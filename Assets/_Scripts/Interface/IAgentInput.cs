using System;
using UnityEngine;

public interface IAgentInput
{
    Vector2 MovementVector { get; }
    bool JumpPressed { get; }
    bool JumpHold { get; }
    bool JumpReleased { get; }
    bool RollPressed { get; }
    bool RollReleased { get; }
    bool AttackPressed { get; }

    event Action OnAttackPressed;
    event Action OnJumpPressed;
    event Action OnJumpReleased;
    event Action<Vector2> OnMovement;
    event Action OnWeaponChange;
    event Action OnRollPressed;
    event Action OnRollReleased;

    void CallOnMovementVector(Vector2 input);

    void CallOnAttackPressed();

    void CallOnJumpPressed();

    void CallOnJumpReleased();

    void CallOnWeaponChange();

    void CallOnRollPressed();

    void CallOnRollReleased();
}
