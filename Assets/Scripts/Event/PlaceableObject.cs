using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceableObject : MonoBehaviour
{
    private bool isPlaced = false;
    public bool IsPlaced { get => isPlaced; }

    private void OnEnable()
    {
        isPlaced = false;
    }

    private void Update()
    {
        if (!isPlaced)
        {
            FollowPointer();
        }
    }

    private void FollowPointer()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            isPlaced = true;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
        bool isHit = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground"));

        if (isHit)
        {
            Vector3 hitPointVec = hitInfo.point;
            this.gameObject.transform.position = hitPointVec;
        }
    }
}