using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalManager : MonoBehaviour
{
    private Player player;    
    /// <summary>
    /// 플레이어 접근용 프로퍼티
    /// </summary>
    public Player Player { get => player; }

    private ItemDataManager itemManager;
    /// <summary>
    /// 아이템 데이터 매니저 접근용 프로퍼티
    /// </summary>
    public ItemDataManager ItemDataManager { get => itemManager; }

    private Factory_Enemy factory_Enemy;
    private Factory_ItemBox factory_ItemBox;

    private Transform[] spawnpoints;
    public Transform[] SpawnPoints { get => spawnpoints; }
    public int eachEnemyCount = 1;

    public Action onGameEnd;

    [Space(10f)]
    public ItemBoxDropTableSO boxData;

#if UNITY_EDITOR
    public bool isTest = false;
#endif

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        itemManager = FindAnyObjectByType<ItemDataManager>();
        factory_Enemy = FindAnyObjectByType<Factory_Enemy>();
        factory_ItemBox = FindAnyObjectByType<Factory_ItemBox>();

        spawnpoints = transform.GetChild(1).GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        SpawnEnemies();
        SpawnItemBox();
    }

    private void SpawnEnemies()
    {
#if UNITY_EDITOR
        if (isTest) return;
#endif

        for(int i = 1; i < spawnpoints.Length; i++)
        {
            for (int count = 0; count < eachEnemyCount; count++)
            {
                Vector3 randVec = UnityEngine.Random.insideUnitSphere;
                Vector3 spawnVec = new Vector3(randVec.x + spawnpoints[i].position.x, 0.1f, randVec.z + spawnpoints[i].position.z);
                EnemyBase enemy = factory_Enemy.SpawnEnemy(spawnVec, Quaternion.identity);
                onGameEnd += () => { enemy.gameObject.SetActive(false); };
            }
        }
    }

    public void SpawnItemBox()
    {
        for(int i = 1; i < spawnpoints.Length; i++)
        {
            Vector3 randVec = UnityEngine.Random.insideUnitSphere * 3f;
            Vector3 spawnVec = new Vector3(randVec.x + spawnpoints[i].position.x, 0.1f, randVec.z + spawnpoints[i].position.z);
            factory_ItemBox.SpawnBox(SetItemBoxDrop(boxData), spawnVec, Quaternion.identity);
        }
    }

    private List<ItemDataSO> SetItemBoxDrop(ItemBoxDropTableSO data)
    {
        List<ItemDataSO> result = new List<ItemDataSO>(data.dataTable.Count);

        for(int i = 0; i < data.dataTable.Count; i++)
        {
            float rand = UnityEngine.Random.value;
            if(rand <= data.dataTable[i].dropRate)
            {
                result.Add(data.dataTable[i].itemData);
            }
        }

        return result;
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }
}
