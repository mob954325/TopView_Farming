using UnityEngine;

public class DetectedMouse : MonoBehaviour
{
    private Outline outline;
    public bool canInspection = false;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        outline.enabled = true;
        canInspection = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
        canInspection = false;
    }
}
