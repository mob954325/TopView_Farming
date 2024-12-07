using UnityEngine;

[CreateAssetMenu(fileName = "Item_0_A", menuName = "Data/Item", order = 1)]
public class ItemDataSO : ScriptableObject
{
    public Sprite Icon;
    public GameObject prefab;

    public string objName = "Unknown_ItemName";
    public int maxCount = 1;
    public int price = 1;
}