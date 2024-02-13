using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerInput.IGameplayActions
{
    private PlayerInput _playerInput;

    public event Action<Vector2> MovementEvent = delegate {};
    public event Action<Vector2> LookEvent = delegate {};
    public event Action HandbrakeEvent = delegate {};

    private Vector2 _currentMovementInput;
    private Vector2 _lookDirectionInput;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _playerInput ??= new PlayerInput();
        _playerInput.Gameplay.SetCallbacks(this);
        EnableInput();
    }
    
    public void EnableInput()
    {
        _playerInput.Gameplay.Enable();    
    }

    public void DisableInput()
    {
        _playerInput.Gameplay.Disable();
    }
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDirectionInput = context.ReadValue<Vector2>();
    }

    public void OnHandbrake(InputAction.CallbackContext context)
    {
        if (context.performed)
            HandbrakeEvent();
    }

    private void FixedUpdate()
    {
        MovementEvent(_currentMovementInput);
    }

    private void LateUpdate()
    {
        LookEvent(_lookDirectionInput);
    }
}
