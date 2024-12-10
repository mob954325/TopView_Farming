using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;

    public Vector2 MoveVec { get; private set; }
    public Vector2 LookVec { get; private set; }

    /// <summary>
    /// 상호작용 키를 누를 때 실행되는 델리게이트
    /// </summary>
    public Action OnInteract;

    /// <summary>
    /// 공격 키를 누를 때 실행되는 델리게이트
    /// </summary>
    public Action OnAttack;

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
        actions.Player.Interact.performed += OnInteractInput;
        actions.Player.Attack.performed += OnAttackInput;
    }

    private void OnDisable()
    {
        actions.Player.Attack.performed -= OnAttackInput;
        actions.Player.Interact.performed -= OnInteractInput;
        actions.Player.Look.performed -= OnLookInput;
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Enable();
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
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
}