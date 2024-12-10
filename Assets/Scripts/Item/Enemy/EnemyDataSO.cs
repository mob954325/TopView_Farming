using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DataTable
{
    public ItemDataSO itemData;

    [Range(0f,1f)]
    public float dropRate; // 0-1 값
} 

[CreateAssetMenu(fileName = "Enemy_aaa", menuName = "Data/Enemy", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    public float attackPower = 1f;
    public float maxHealth = 3f;

    [Space(10)]
    public List<DataTable> dataTable;
}