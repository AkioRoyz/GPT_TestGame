using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }

    // События для других систем
    public event Action OnAttackPressed;
    public event Action OnBlockPressed;
    public event Action OnDashPressed;
    public event Action OnPausePressed;
    public event Action OnInteractPressed;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();

        inputActions.Gameplay.Move.performed += OnMovePerformed;
        inputActions.Gameplay.Move.canceled += OnMoveCanceled;

        inputActions.Gameplay.Attack.performed += ctx => OnAttackPressed?.Invoke();
        inputActions.Gameplay.Block.performed += ctx => OnBlockPressed?.Invoke();
        inputActions.Gameplay.Dash.performed += ctx => OnDashPressed?.Invoke();
        inputActions.Gameplay.Pause.performed += ctx => OnPausePressed?.Invoke();
        inputActions.Gameplay.Interact.performed += ctx => OnInteractPressed?.Invoke();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }
}
