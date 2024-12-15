using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour, IMoveable
{
    private CharacterController characterController;

    public float speed = 5f;
    public float Speed { get => speed; set => speed = value; }

    public float rotatePower = 90f;

    public Action OnMoveAction { get; set; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(Vector3 moveDir)
    {
        characterController.Move(Time.deltaTime * speed * moveDir.y * transform.forward);

        Vector3 rotVec = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + rotatePower * moveDir.x * Vector3.up, Time.deltaTime);
        characterController.gameObject.transform.eulerAngles = rotVec;
        OnMoveAction?.Invoke();
    }
}