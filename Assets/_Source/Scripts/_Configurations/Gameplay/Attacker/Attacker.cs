using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : ScriptableObject
{
    public virtual void Attack(CombatUnit attacker,List<CombatUnit> opponents)
    {
        if (attacker == null)
            throw new ArgumentNullException(nameof(attacker));
            
        opponents.CastExeption();

        foreach (var opponent in opponents)
        {
            if (opponent.IsDead)
                throw new InvalidOperationException($"{nameof(opponent)} {nameof(opponent.IsDead)} = {opponent.IsDead}.");
        }
    }
}