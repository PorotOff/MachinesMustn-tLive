using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatUnitView : MonoBehaviour
{
    [field: SerializeField] public CombatUnitAnimator Animator { get; private set; }
    [field: SerializeField] public CombatUnitAnimationEvents AnimationEvents { get; private set; }

    private List<CombatUnitStatIndicator> _displayersAtBar;

    public void Initialize(List<CombatUnitStatIndicator> displayersAtBar)
    {
        if (displayersAtBar == null)
            throw new ArgumentNullException(nameof(_displayersAtBar));

        _displayersAtBar = displayersAtBar;
    }
}