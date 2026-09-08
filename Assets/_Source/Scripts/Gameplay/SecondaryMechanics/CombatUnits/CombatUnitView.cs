using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CombatUnitView : MonoBehaviour
{
    [field: SerializeField] public CombatUnitAnimator Animator { get; private set; }
    [field: SerializeField] public CombatUnitAnimationEvents AnimationEvents { get; private set; }

    private HealthDisplayerAtBar _healthDisplayerAtBar;
    private List<DisplayerAtBar> _displayersAtBar;

    public void Initialize(HealthDisplayerAtBar healthDisplayerAtBar)
    {
        _healthDisplayerAtBar = healthDisplayerAtBar;
    }

    public void Initialize(List<DisplayerAtBar> displayersAtBar)
    {
        if (displayersAtBar == null)
            throw new ArgumentNullException(nameof(_displayersAtBar));

        _displayersAtBar = displayersAtBar;
    }

    public void Initialize(Health health)
    {
        _healthDisplayerAtBar.Initialize(health);
    }

    public void Initialize(List<IDisplayableAtBar> displayablesAtBar)
    {
        if (_displayersAtBar == null)
            throw new ArgumentNullException(nameof(_displayersAtBar));

        if (displayablesAtBar == null)
            throw new ArgumentNullException(nameof(displayablesAtBar));

        List<IDisplayableAtBar> cachedDisplayablesAtBar = new List<IDisplayableAtBar>(displayablesAtBar);

        foreach (var displayerAtBar in _displayersAtBar)
        {
            IDisplayableAtBar displayableAtBar = cachedDisplayablesAtBar.FirstOrDefault(displayable => displayable.GetType() == displayerAtBar.DisplayableType);

            if (displayableAtBar == null)
                throw new ArgumentNullException(nameof(displayableAtBar));

            displayerAtBar.Initialize(displayableAtBar);
            cachedDisplayablesAtBar.Remove(displayableAtBar);
        }
    }

    public virtual void Subscribe()
    {
        // _healthDisplayerAtBar.Subscribe();

        foreach (var displayerAtBar in _displayersAtBar)
        {
            displayerAtBar.Subscribe();
        }
    }

    public virtual void Unsubscribe()
    {
        // _healthDisplayerAtBar.Subscribe();

        foreach (var displayerAtBar in _displayersAtBar)
        {
            displayerAtBar.Unsubscribe();
        }
    }
}