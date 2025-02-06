using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmoothView : HealthBarView<Slider>
{
    [SerializeField] private float _smoothSpeed;

    private void Awake()
    {
        Bar.maxValue = Health.MaxValue;
        Bar.value = Health.Value;
    }

    private void Update()
    {
        if (Bar != null)
        {
            Bar.value = Mathf.MoveTowards(Bar.value, Health.Value, _smoothSpeed * Time.deltaTime);
        }
    }
}