using Unity.VisualScripting;
using UnityEngine;

public class CombatUnitBuilder : MonoBehaviour
{
    [SerializeField] private WarriorCombatUnitRoot _warriorCombatUnitRoot;

    [SerializeField] private WarriorCombatUnitConfig _warriorCombatUnitConfig;

    private void Awake()
    {
        Build<WoodcutterWarriorCombatUnit>(_warriorCombatUnitConfig);
    }

    public void Build<T>(WarriorCombatUnitConfig config) where T : WarriorCombatUnit
    {
        WarriorCombatUnitRoot warriorCombatUnitRoot = Instantiate(_warriorCombatUnitRoot);
        WarriorCombatUnit warriorCombatUnit = warriorCombatUnitRoot.AddComponent<T>();
        WarriorCombatUnitView warriorCombatUnitView = Instantiate(config.View, warriorCombatUnitRoot.ViewContainer);
        
        warriorCombatUnitView.Initialize(warriorCombatUnitRoot.HealthDisplayerAtBar, warriorCombatUnitRoot.AttackEnergyDisplayerAtBar);
        warriorCombatUnit.Initialize(config, warriorCombatUnitView);
    }
}