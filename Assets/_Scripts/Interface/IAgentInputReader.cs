using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentInputReader
{
    Vector2 MovementVector { get; set; }
    event Action OnAttackPressed;
    event Action OnJumpPressed;
    event Action OnJumpReleased;
    event Action<Vector2> OnMovement;
    event Action OnWeaponChange;
    event Action OnRollPressed;
    event Action OnRollReleased;
}
