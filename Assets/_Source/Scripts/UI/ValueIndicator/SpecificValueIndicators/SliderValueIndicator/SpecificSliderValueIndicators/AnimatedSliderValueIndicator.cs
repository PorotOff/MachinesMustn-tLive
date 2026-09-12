using System.Collections;
using UnityEngine;

public class AnimatedSliderValueIndicator : SliderValueIndicator
{
    [SerializeField] private float _interpolationStep = 0.1f;

    private Coroutine _coroutine;

    public override void Display(float current)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(UpdateIndicator(current));
    }

    private IEnumerator UpdateIndicator(float current)
    {
        while (Slider.value != current)
        {
            Slider.value = Mathf.Lerp(Slider.value, current, _interpolationStep);
            yield return null;
        }
    }
}