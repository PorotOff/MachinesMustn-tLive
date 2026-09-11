using System;
using UnityEngine;

public abstract class CombatUnitStatIndicator : MonoBehaviour
{
    protected ValueIndicator Indicator;
    protected CombatUnit CombatUnit;

    public virtual void Initialize(CombatUnit combatUnit)
    {
        if (TryGetComponent(out ValueIndicator indicator) == false)
            throw new ArgumentNullException(nameof(indicator));

        Indicator = indicator;
        CombatUnit = combatUnit;
    }
}