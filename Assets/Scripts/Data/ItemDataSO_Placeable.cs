using UnityEngine;

[CreateAssetMenu(fileName = "Item_0_A", menuName = "Data/Item/Placeable", order = 2)]

public class ItemDataSO_Placeable : ItemDataSO
{
    [Header("Placeable Info")]
    public GameObject worldObject;
}