using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitConfig", menuName = "Configurations/Gameplay/CombatUnits/CombatUnitConfig", order = 0)]
public class CombatUnitConfig : ScriptableObject
{
    [field: Header("Data")]
    [field: SerializeField, Min(0)] public int ID { get; private set; }

    [field: Header("Health")]
    [field: SerializeField, Min(0)] public int Health { get; private set; }
    [field: SerializeField, Min(0)] public int MinHealth { get; private set; }
    [field: SerializeField, Min(0)] public int MaxHealth { get; private set; }

    [field: Header("Attack energy")]
    [field: SerializeField, Min(0)] public int AttackEnergy { get; private set; }
    [field: SerializeField, Min(0)] public int MinAttackEnergy { get; private set; }
    [field: SerializeField, Min(0)] public int MaxAttackEnergy { get; private set; }
    [field: SerializeField, Min(0)] public int EnergyStripeCapacity { get; private set; }
    [field: SerializeField, Min(0)] public int EnergyStripesCount { get; private set; }

    [field: Header("Damage")]
    [field: SerializeField, Min(0)] public int Damage { get; private set; }

    [field: Header("Attack")]
    [field: SerializeField, Min(0)] public int AttackSpeed { get; private set; }
    [field: SerializeField] public Attacker Attacker { get; private set; }

    [field: Header("View")]
    [field: SerializeField] public CombatUnitView View { get; private set; }

    private void OnValidate()
    {
        MaxAttackEnergy = EnergyStripeCapacity * EnergyStripesCount;
    }
}