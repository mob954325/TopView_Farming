using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public IHealth targetHealth;
    private Slider hpSlider;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();
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
}