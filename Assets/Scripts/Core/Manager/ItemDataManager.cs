using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    [SerializeField] private List<ItemDataSO> items;
    public List<ItemDataSO> Items { get => items; }

    [SerializeField] private List<ItemDataSO_Equipable> equipable;
    public List<ItemDataSO_Equipable> Equipable { get => equipable; }
}
