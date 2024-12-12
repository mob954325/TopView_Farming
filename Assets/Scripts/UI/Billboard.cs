using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Renderer parentRenderer;
    private Vector3 lookTarget;

    private void Awake()
    {
        parentRenderer = GetComponentInParent<Renderer>();
        lookTarget = Camera.main.transform.position;
        float angle = Vector3.Angle(transform.forward, Camera.main.transform.forward);

        transform.Rotate(Vector3.right * angle);
    }
}