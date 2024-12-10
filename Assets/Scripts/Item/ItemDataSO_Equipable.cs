using UnityEngine;

[CreateAssetMenu(fileName = "Item_0_A", menuName = "Data/Item/Weapon", order = 2)]
public class ItemDataSO_Equipable : ItemDataSO
{
    [Header("Weapon Data")]
    public float damage = 1;
}