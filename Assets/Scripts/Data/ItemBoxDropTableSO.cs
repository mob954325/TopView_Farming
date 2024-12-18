using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBoxDropTable", menuName = "Data/ItemBox", order = 0)]
public class ItemBoxDropTableSO : ScriptableObject
{
    public List<DataTable> dataTable;
}
