using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitConfig", menuName = "Configurations/CombatUnits/CombatUnitConfig", order = 0)]
public class CombatUnitConfig : ScriptableObject
{
    [field: Header("Health")]
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }

    [field: Header("Armor")]
    [field: SerializeField] public int Armor { get; private set; }

    [field: Header("Energy")]
    [field: SerializeField] public int EnergyStripeCapacity { get; private set; }
    [field: SerializeField] public int EnergyStripesCount { get; private set; }

    [field: Header("Damage")]
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int CriticalDamage { get; private set; }
    [field: SerializeField] public int CriticalDamageChance { get; private set; }

    [field: Header("Attack")]
    [field: SerializeField] public int AttackSpeed { get; private set; }
}