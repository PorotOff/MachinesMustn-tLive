using UnityEngine;

[CreateAssetMenu(fileName = "CasualDamageTaker", menuName = "Configurations/Gameplay/DamageTakers/CasualDamageTaker", order = 0)]
public class CasualDamageTaker : DamageTaker
{
    public override void TakeDamage(CombatUnit damageTaker, int damage)
    {
        base.TakeDamage(damageTaker, damage);
        damageTaker.Health.Reduce(damage);
    }
}