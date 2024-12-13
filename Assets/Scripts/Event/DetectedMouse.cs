using UnityEngine;
using UnityEngine.InputSystem;

public class DetectedMouse : MonoBehaviour
{
    private IInteractable target;
    private Outline outline;

    private ContextMenuUI contextUI;

    public bool canInspection = false;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        target = GetComponent<IInteractable>();

        contextUI = FindAnyObjectByType<ContextMenuUI>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if(Mouse.current.rightButton.isPressed)
        {
            Debug.Log(Mouse.current.position.value);
            contextUI.OnActive(Mouse.current.position.value);
        }

        outline.enabled = true;
        canInspection = true;
    }

    private void OnMouseExit()
    {
        contextUI.OnDeactive();

        outline.enabled = false;
        canInspection = false;
    }
}
