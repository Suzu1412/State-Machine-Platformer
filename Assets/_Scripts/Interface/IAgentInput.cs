using System;
using UnityEngine;

public interface IAgentInput
{
    Vector2 MovementVector { get; }
    event Action OnAttackPressed;
    event Action OnJumpPressed;
    event Action OnJumpReleased;
    event Action<Vector2> OnMovement;
    event Action OnWeaponChange;
    event Action OnRollPressed;
    event Action OnRollReleased;
}
