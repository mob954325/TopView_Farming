using UnityEngine;

[CreateAssetMenu(fileName = "Item_0_A", menuName = "Data/Item/Useable", order = 3)]
public class ItemDataSO_Useable : ItemDataSO
{
    /// <summary>
    /// 기능 스크립트가 있는 오브젝트(IUseable 사용)
    /// </summary>
    public ItemFunctionBase function;
}