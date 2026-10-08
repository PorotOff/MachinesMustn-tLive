using System;
using UnityEngine;

public abstract class DamageTaker : ScriptableObject
{
    public virtual void TakeDamage(CombatUnit damageTaker, int damage)
    {
        if (damageTaker == null)
            throw new ArgumentNullException(nameof(damageTaker));

        if (damage < 0)
            throw new InvalidOperationException(nameof(damage));
    }
}