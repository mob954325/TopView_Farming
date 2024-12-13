using UnityEngine;

public class InventoryContentUI : MonoBehaviour
{
    private RectTransform rectTransform;

    /// <summary>
    /// rectTransform의 Height 값을 설정하는 함수 ( 슬롯개수 * 100(슬롯높이값) )
    /// </summary>
    /// <param name="slotCount">인벤토리 슬롯 개수</param>
    public void SetHeight(int slotCount)
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.rect.Set(rectTransform.rect.x, rectTransform.rect.y, rectTransform.rect.width, slotCount * 100f);
    }
}
