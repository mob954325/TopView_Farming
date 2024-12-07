using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    PlayerController controller;
    PlayerInput input;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        controller.OnMove(input.MoveVec);
    }
}