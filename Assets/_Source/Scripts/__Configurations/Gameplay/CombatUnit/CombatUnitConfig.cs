using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitConfig", menuName = "Configurations/Gameplay/CombatUnits/CombatUnitConfig", order = 0)]
public class CombatUnitConfig : ScriptableObject
{
    [field: Header("Data")]
    [field: SerializeField] public int ID { get; private set; }

    [field: Header("Health")]
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }

    [field: Header("Attack energy")]
    [field: SerializeField] public int AttackEnergy { get; private set; }
    [field: SerializeField] public int EnergyStripeCapacity { get; private set; }
    [field: SerializeField] public int EnergyStripesCount { get; private set; }

    [field: Header("Damage")]
    [field: SerializeField] public int Damage { get; private set; }

    [field: Header("Attack")]
    [field: SerializeField] public int AttackSpeed { get; private set; }

    [field: Header("View")]
    [field: SerializeField] public CombatUnitView View { get; private set; }
}