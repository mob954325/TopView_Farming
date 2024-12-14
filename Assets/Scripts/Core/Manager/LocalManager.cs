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

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        itemManager = FindAnyObjectByType<ItemDataManager>();
    }
}
