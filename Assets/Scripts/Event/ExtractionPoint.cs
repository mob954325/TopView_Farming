using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    private float timer = 0f;
    private float maxTimer = 3f;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;

            if(timer > maxTimer)
            {
                Debug.Log("탈출 !");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0f;
    }
}
