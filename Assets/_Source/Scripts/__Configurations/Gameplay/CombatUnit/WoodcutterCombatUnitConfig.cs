using UnityEngine;

[CreateAssetMenu(fileName = "WoodcutterCombatUnit", menuName = "Configurations/Gameplay/CombatUnits/WoodcutterCombatUnit", order = 0)]
public class WoodcutterCombatUnitConfig : WarriorCombatUnitConfig
{
    [field: Header("Attack")]
    [field: SerializeField, Range(Constants.MinChance, Constants.MaxChance)] public int DoubleAttackChance { get; private set; }

    public int AttackableCount { get; } = 2;
}