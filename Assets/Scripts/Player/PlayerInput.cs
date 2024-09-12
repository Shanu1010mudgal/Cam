using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 aimPos;
    [HideInInspector] public bool runInput;
    [HideInInspector] public bool jumpInput;
    [HideInInspector] public bool sneakInput;
    [HideInInspector] public bool aimInput;

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveInput.x = Mathf.Round(moveInput.x);
    }

    public void Run(InputAction.CallbackContext context)
    {
        runInput = context.performed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
    }

    public void Sneak(InputAction.CallbackContext context)
    {
        if (context.performed)
            sneakInput = !sneakInput;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        aimInput = context.performed;
    }

    public void AimPos(InputAction.CallbackContext context)
    {
        if (aimInput)
            aimPos = Camera.main.ScreenToViewportPoint(context.ReadValue<Vector2>());
    }

}