using UnityEngine;

public class Function_Button : ItemFunctionBase
{
    public GameObject ExtractionPointPrefab;
    private float mapRange = 80f;

    protected override void OnItemUse()
    {
        Vector3 rand = UnityEngine.Random.onUnitSphere;
        Debug.Log(rand * 12f);

        GameObject obj = Instantiate(ExtractionPointPrefab);
        obj.transform.position = new Vector3(rand.x * 12f, 1f, rand.z * 12f);
    }
}