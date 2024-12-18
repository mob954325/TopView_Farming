using UnityEngine;

public class Function_Button : ItemFunctionBase
{
    public GameObject ExtractionPointPrefab;

    protected override void OnItemUse()
    {
        Vector3 rand = UnityEngine.Random.onUnitSphere;

        GameObject obj = Instantiate(ExtractionPointPrefab);
    }
}