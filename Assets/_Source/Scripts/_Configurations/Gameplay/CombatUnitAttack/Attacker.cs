using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : ScriptableObject
{
    protected CombatUnit CombatUnit { get; private set; }

    public void Initialize(CombatUnit unit)
    {
        CombatUnit = unit;
    }

    public virtual void Attack(List<CombatUnit> opponents)
    {
        opponents.CastExeption();

        foreach (var opponent in opponents)
        {
            if (opponent.IsDead)
                throw new InvalidOperationException($"{nameof(opponent)} {nameof(opponent.IsDead)} = {opponent.IsDead}.");
        }
    }
}