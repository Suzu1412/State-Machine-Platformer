using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour, IAgentInput
{
    [field: SerializeField]
    public Vector2 MovementVector { get; private set; }

    public event Action OnAttackPressed, OnJumpPressed, OnJumpReleased, OnWeaponChange;

    public event Action<Vector2> OnMovement;

    public KeyCode jumpKey, attackKey, menuKey, weaponSwapKey;

    public UnityEvent OnMenuKeyPressed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) GetMenuInput();

        GetMovementInput();
        GetJumpInput();
        GetAttackInput();
        GetWeaponSwapInput();

        GetMenuInput();
    }

    void GetMovementInput()
    {
        MovementVector = GetMovementVector();
        OnMovement?.Invoke(MovementVector);
    }

    Vector2 GetMovementVector()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void GetWeaponSwapInput()
    {
       if (Input.GetKeyDown(weaponSwapKey))
        {
            OnWeaponChange?.Invoke();
        }
    }

    void GetMenuInput()
    {
        if (Input.GetKeyDown(menuKey))
        {
            OnMenuKeyPressed?.Invoke();
        }
    }

    void GetAttackInput()
    {
        if (Input.GetKeyDown(attackKey))
        {
            OnAttackPressed?.Invoke();
        }
    }

    void GetJumpInput()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpPressed?.Invoke();
        }

        if (Input.GetKeyUp(jumpKey))
        {
            OnJumpReleased?.Invoke();
        }
    }

    


}
