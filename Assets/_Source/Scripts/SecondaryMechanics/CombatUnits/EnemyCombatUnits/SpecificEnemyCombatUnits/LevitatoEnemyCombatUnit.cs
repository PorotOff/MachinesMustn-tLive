using System.Collections.Generic;
using UnityEngine;

public class LevitatoEnemyCombatUnit : EnemyCombatUnit
{
    public override void Attack(List<CombatUnit> oppnonents)
    {
        int randomEnemyIndex = Random.Range(0, oppnonents.Count);
        IDamageable damageable = oppnonents[randomEnemyIndex];

        damageable.TakeDamage(Config.Damage);
    }
}