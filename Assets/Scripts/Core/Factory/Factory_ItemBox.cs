using System.Collections.Generic;
using UnityEngine;

public class Factory_ItemBox : Factory<ItemBox>
{
    public void SpawnBox(List<ItemDataSO> datas, Vector3 pos, Quaternion rot)
    {
        ItemBox box = SpawnProduct(pos, rot);
        box.Init();
        box.AddItems(datas);
    }
}