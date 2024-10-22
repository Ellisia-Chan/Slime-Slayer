using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    public event EventHandler OnAttackAction;
    public event EventHandler OnAttackActionCancel;
    public event EventHandler OnDashAction;
    public event EventHandler<int> OnInventoryAction;

    public PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputActions.Enable();
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }

    private void Start() {
        playerInputActions.Player.Attack.performed += Attack_performed;
        playerInputActions.Player.Attack.canceled += Attack_canceled;
        playerInputActions.Player.Dash.performed += Dash_performed;
        playerInputActions.Player.Inventory.performed += Inventory_performed1;
    }

    private void Inventory_performed1(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        int inventorySlot = (int)obj.ReadValue<float>();
        OnInventoryAction?.Invoke(this, inventorySlot);
    }

    private void Attack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnAttackActionCancel?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void Dash_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }

    private void OnDestroy() {
        playerInputActions.Player.Attack.performed -= Attack_performed;
        playerInputActions.Player.Attack.canceled -= Attack_canceled;
        playerInputActions.Player.Dash.performed -= Dash_performed;
    }
}
