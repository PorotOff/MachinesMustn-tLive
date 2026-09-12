using System;
using UnityEngine;

public abstract class ValueIndicator : MonoBehaviour
{
    public IIndicateable Indicateable;

    private void OnDestroy()
    {
        Indicateable.Changed -= UpdateIndicator;
    }

    public virtual void Initialize(IIndicateable indicateable)
    {
        if (indicateable == null)
            throw new ArgumentNullException(nameof(indicateable));

        Indicateable = indicateable;

        UpdateIndicator();
        Indicateable.Changed += UpdateIndicator;
    }

    public abstract void Display(float current);

    public abstract void SetActive(bool isActive);

    private void UpdateIndicator()
    {
        Display(Indicateable.Current);
    }
}