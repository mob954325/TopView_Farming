using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Canvas canvas;              // 캔버스
    private RectTransform canvasRect;   // 캔버스의 RectTransform
    private RectTransform UI_Element;   // 해당 UI RectTransform
    private Slider hpSlider;

    public IHealth targetHealth;
    public Vector2 offset;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();

        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        UI_Element = GetComponent<RectTransform>();
    }

    public void Init(IHealth target)
    {
        targetHealth = target;

        targetHealth.OnHit += () =>
        {
            float sliderValue = targetHealth.Health / targetHealth.MaxHealth;
            SetValue(sliderValue);
        };
    }

    /// <summary>
    /// 0 - 1 사이 슬라이더 값을 적용할 함수
    /// </summary>
    public void SetValue(float value)
    {
        hpSlider.value = value;
    }

    private void LateUpdate()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        if (targetHealth == null) return;

        // 월드 오브젝트 위치를 뷰포트 위치로 변환
        Vector2 viewportPositoin = Camera.main.WorldToViewportPoint(targetHealth.worldObject.transform.position);

        // WorldToViewportPoint() 함수는 좌측 하단을 0,0으로 잡고 값을 반환하기 때문에 올바른 좌표값을 구할려면 각 좌표에 스크린 크기값 * 0.5을 빼줘야한다.
        Vector2 worldObjectScreenPosition = new Vector2(
            (viewportPositoin.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
            (viewportPositoin.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        UI_Element.anchoredPosition = worldObjectScreenPosition + offset;
    }
}