using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CombatUnitView : MonoBehaviour
{
    [field: SerializeField] public CombatUnitAnimator Animator { get; private set; }
    [field: SerializeField] public CombatUnitAnimationEvents AnimationEvents { get; private set; }

    private List<CombatUnitStatsDisplayerAtBar> _displayersAtBar;

    public void Initialize(List<CombatUnitStatsDisplayerAtBar> displayersAtBar)
    {
        if (displayersAtBar == null)
            throw new ArgumentNullException(nameof(_displayersAtBar));

        _displayersAtBar = displayersAtBar;
    }
}