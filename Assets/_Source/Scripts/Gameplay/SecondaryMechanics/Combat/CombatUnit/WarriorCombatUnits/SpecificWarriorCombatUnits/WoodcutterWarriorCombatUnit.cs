using System;
using System.Collections.Generic;

public class WoodcutterWarriorCombatUnit : WarriorCombatUnit
{
    public override void Attack(List<CombatUnit> opponents)
    {
        if (Config is not WoodcutterCombatUnitConfig config)
            throw new InvalidOperationException();

        int chance = UnityEngine.Random.Range(0, Constants.MaxChance);
        
        if (chance <= config.DoubleAttackChance)
        {
            for (int i = 0; i < config.AttackableCount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, opponents.Count + 1);

                CombatUnit opponent = opponents[randomIndex];
                opponent.TakeDamage(config.Damage);
                
                opponents.Remove(opponent);
            }
        }
        else
        {
            base.Attack(opponents);
        }
    }
}