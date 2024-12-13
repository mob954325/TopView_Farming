using UnityEngine;
using UnityEngine.InputSystem;

public class DetectedMouse : MonoBehaviour
{
    private IInteractable target;
    private Outline outline;

    private void OnEnable()
    {
        outline = GetComponentInChildren<Outline>();
        target = GetComponent<IInteractable>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if(Mouse.current.rightButton.isPressed)
        {
            target.OnInteract();
        }

        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
