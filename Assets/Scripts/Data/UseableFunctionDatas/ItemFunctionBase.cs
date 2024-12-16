using UnityEngine;

public class ItemFunctionBase : MonoBehaviour, IUseable
{
    /// <summary>
    /// 아이템 사용 시 실행되는 함수
    /// </summary>
    protected virtual void OnItemUse()
    {
        Debug.Log("아이템 사용됨");
    }

    public void OnUse()
    {
        OnItemUse();
    }
}
