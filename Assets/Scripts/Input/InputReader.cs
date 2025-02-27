using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerMovementInput;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Imput/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> FireEvent;
    public event Action<Vector2> MoveEvent; 
    private PlayerMovementInput _playerMovementInput;

    private void OnEnable()
    {
        if (_playerMovementInput == null)
        {
            _playerMovementInput = new PlayerMovementInput();
            _playerMovementInput.Player.SetCallbacks(this);
        }

        _playerMovementInput.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log($"OnLook");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FireEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            FireEvent?.Invoke(false);
        }
    }
}