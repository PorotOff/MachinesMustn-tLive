using UnityEngine;

[CreateAssetMenu(fileName = "WarriorCombatUnitConfig", menuName = "Configurations/WarriorCombatUnitConfig", order = 0)]
public class WarriorCombatUnitConfig : CombatUnitConfig
{
    [field: Header("View")]
    [field: SerializeField] public WarriorCombatUnitView View { get; private set; }
}