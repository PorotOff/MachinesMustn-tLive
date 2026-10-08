using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MultipleRandomAttacker", menuName = "Configurations/Gameplay/Attackers/MultipleRandomAttacker", order = 0)]
public class MultipleRandomAttacker : Attacker
{
    [SerializeField] private int attacksCount;

    public override void Attack(CombatUnit attacker, List<CombatUnit> opponents)
    {
        base.Attack(attacker, opponents);

        List<CombatUnit> aliveOpponents = opponents.Where(opponent => opponent.IsDead == false).ToList();

        if (aliveOpponents.Count == 0)
            return;

        System.Random random = new System.Random();

        List<CombatUnit> attackableOpponents = aliveOpponents.OrderBy(opponent => random.Next()).Take(attacksCount).ToList();
        attackableOpponents.ForEach(opponent => opponent.TakeDamage(attacker.Config.Damage));

        attacker.AttackEnergy.Reduce(attacker.AttackEnergy.EnergyStripeCapacity);
    }
}