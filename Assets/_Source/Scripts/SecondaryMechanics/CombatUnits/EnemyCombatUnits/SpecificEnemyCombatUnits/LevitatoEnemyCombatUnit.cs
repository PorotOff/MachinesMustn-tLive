using System.Collections.Generic;
using UnityEngine;

public class LevitatoEnemyCombatUnit : EnemyCombatUnit
{
    public override void Attack(List<IDamageable> damageables)
    {
        int randomEnemyIndex = Random.Range(0, damageables.Count);
        IDamageable damageable = damageables[randomEnemyIndex];

        damageable.TakeDamage(Config.Damage);
    }
}