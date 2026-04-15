using System.Collections.Generic;
using UnityEngine;

public class WoodcutterWarriorCombatUnit : WarriorCombatUnit
{
    public override void Attack(List<CombatUnit> opponents)
    {
        int randomEnemyIndex = Random.Range(0, opponents.Count);
        CombatUnit opponent = opponents[randomEnemyIndex];
        opponent.TakingDamageComplete += InvokeAttackComplete;

        opponent.TakeDamage(Config.Damage);
    }
}