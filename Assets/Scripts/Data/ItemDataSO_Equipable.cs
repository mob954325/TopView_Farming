using UnityEngine;

[CreateAssetMenu(fileName = "Item_0_A", menuName = "Data/Item/Weapon", order = 1)]
public class ItemDataSO_Equipable : ItemDataSO
{
    [Header("Weapon Data")]
    public GameObject weaponPrefab;
    public float damage = 1;
}