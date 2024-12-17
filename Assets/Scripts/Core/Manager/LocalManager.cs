using UnityEngine;

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

    private Transform[] enemySpawnpoints;
    public int eachEnemyCount = 1;

#if UNITY_EDITOR
    public bool isTest = false;
#endif

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        itemManager = FindAnyObjectByType<ItemDataManager>();
        factory_Enemy = FindAnyObjectByType<Factory_Enemy>();

        enemySpawnpoints = transform.GetChild(1).GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
#if UNITY_EDITOR
        if (isTest) return;
#endif

        for(int i = 1; i < enemySpawnpoints.Length; i++)
        {
            for (int count = 0; count < eachEnemyCount; count++)
            {
                Vector3 randVec = UnityEngine.Random.insideUnitSphere;
                Vector3 spawnVec = new Vector3(randVec.x + enemySpawnpoints[i].position.x, 0.1f, randVec.z + enemySpawnpoints[i].position.z);
                factory_Enemy.SpawnEnemy(spawnVec, Quaternion.identity);
            }
        }
    }
}
