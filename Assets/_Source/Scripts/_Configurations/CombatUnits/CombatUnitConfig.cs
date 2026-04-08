using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Configurations/UnitConfig", order = 0)]
public class CombatUnitConfig : ScriptableObject
{
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int Armor { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int CriticalDamage { get; private set; }
    [field: SerializeField] public int CriticalDamageChance { get; private set; }
    [field: SerializeField] public int AttackSpeed { get; private set; }
}