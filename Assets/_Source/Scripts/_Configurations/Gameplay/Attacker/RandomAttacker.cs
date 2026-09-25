using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomAttacker", menuName = "Configurations/Gameplay/Attackers/RandomAttacker", order = 0)]
public class RandomAttacker : Attacker
{
    [SerializeField] private int attacksCount;

    public override void Attack(List<CombatUnit> opponents)
    {
        base.Attack(opponents);

        List<CombatUnit> cachedOpponents = new List<CombatUnit>(opponents);

        while (CombatUnit.AttackEnergy.AvailableAttacks > 0)
        {
            for (int i = 0; i < attacksCount; i++)
            {
                List<CombatUnit> aliveOpponents = cachedOpponents.Where(opponent => opponent.IsDead == false).ToList();

                if (aliveOpponents.Count == 0)
                    return;

                int index = UnityEngine.Random.Range(0, cachedOpponents.Count);
                CombatUnit opponent = cachedOpponents[index];

                opponent.TakeDamage(CombatUnit.Config.Damage);
                cachedOpponents.Remove(opponent);

                CombatUnit.AttackEnergy.Reduce(CombatUnit.AttackEnergy.EnergyStripeCapacity);
            }
        }
    }
}