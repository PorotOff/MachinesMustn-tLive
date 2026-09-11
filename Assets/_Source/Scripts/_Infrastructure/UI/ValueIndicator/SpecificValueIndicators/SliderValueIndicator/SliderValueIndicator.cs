using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueIndicator : ValueIndicator
{
    [SerializeField] protected Slider Slider;

    public override void Initialize(IIndicateable indicateable)
    {
        if (indicateable == null)
            throw new ArgumentNullException(nameof(indicateable));

        base.Initialize(indicateable);

        Slider.minValue = Indicateable.Min;
        Slider.maxValue = Indicateable.Max;
    }

    public override void Display(float current)
    {
        Slider.value = current;
    }

    public override void SetActive(bool isActive)
    {
        Slider.gameObject.SetActive(isActive);
    }
}