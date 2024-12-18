using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    LocalManager manager;

    private float timer = 0f;
    private float maxTimer = 3f;

    private void OnEnable()
    {
        manager = FindAnyObjectByType<LocalManager>();
        Init();
    }

    private void Init()
    {
        if(manager != null)
        {
            int rand = UnityEngine.Random.Range(0 ,manager.SpawnPoints.Length);
            this.gameObject.transform.position = manager.SpawnPoints[rand].position;
        }
        else
        {
            Vector3 randVec = UnityEngine.Random.insideUnitSphere * 15f;
            this.gameObject.transform.position = new Vector3(randVec.x, 0f, randVec.z);
        }
    }

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
