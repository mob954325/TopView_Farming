using UnityEngine;

[RequireComponent(typeof(PlaceableObject), typeof(Collider))]
public class Spike : MonoBehaviour
{
    private PlaceableObject placeableObject;
    private Collider coll;

    public float damge = 1f;

    private void Awake()
    {
        placeableObject = GetComponent<PlaceableObject>();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!placeableObject.isActiveAndEnabled)
            return;

        if(other.gameObject.tag == "Enemy")
        {
            IHealth target = other.GetComponent<IHealth>();
            target.Hit(1f);

            Destroy(this.gameObject);
        }
    }
}
