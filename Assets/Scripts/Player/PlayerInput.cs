using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;

    public Vector2 MoveVec { get; private set; }
    public Vector2 LookVec { get; private set; }

    /// <summary>
    /// 인벤토리 키(I)를 누를 때 실행되는 델리게이트
    /// </summary>
    public Action OnInvenOpen;

    /// <summary>
    /// 공격 키를 누를 때 실행되는 델리게이트
    /// </summary>
    public Action OnAttack;

    /// <summary>
    /// 아무버튼 누르면 실행되는 델리게이트 (AnyButton)
    /// </summary>
    public Action OnCancel;

    private void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Enable();
        actions.Player.Move.performed += OnMoveInput;
        actions.Player.Move.canceled += OnMoveInput;
        actions.Player.Look.performed += OnLookInput;
        actions.Player.Inventory.performed += OnInvenInput;
        actions.Player.Attack.performed += OnAttackInput;
        actions.Player.Cancel.performed += OnAnyKeyPress;
    }

    private void OnDisable()
    {
        actions.Player.Cancel.performed -= OnAnyKeyPress;
        actions.Player.Attack.performed -= OnAttackInput;
        actions.Player.Inventory.performed -= OnInvenInput;
        actions.Player.Look.performed -= OnLookInput;
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Disable();
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private void OnInvenInput(InputAction.CallbackContext context)
    {
        OnInvenOpen?.Invoke();
    }

    private void OnLookInput(InputAction.CallbackContext context)
    {
        Vector2 lookVec = context.ReadValue<Vector2>();
        LookVec = lookVec;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveVec = context.ReadValue<Vector2>();
        MoveVec = moveVec;
    }

    private void OnAnyKeyPress(InputAction.CallbackContext context)
    {
        OnCancel?.Invoke();
    }
}