using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUnitBuilder : MonoBehaviour
{
    [SerializeField] private WarriorsChargeService _warriorsChargeService; // temp временно только для проверки нахуй! Только геи так оставляют

    [SerializeField] private CombatUnitRoot _combatUnitRootPrefab;
    [SerializeField] private CombatUnitConfig _combatUnitConfig;

    private void Start()
    {
        List<WarriorCombatUnit> warriors = new List<WarriorCombatUnit>();
        warriors.Add((WarriorCombatUnit)Build<WoodcutterWarriorCombatUnit>(_combatUnitConfig)); // temp каст тоже временный. и вся эта конструкция в старте тоже временная нахуй

        _warriorsChargeService.Initialize(warriors);
    }

    public CombatUnit Build<T>(CombatUnitConfig config) where T : CombatUnit
    {
        var combatUnitRoot = Instantiate(_combatUnitRootPrefab);
        var combatUnit = combatUnitRoot.AddComponent<T>();
        var combatUnitView = Instantiate(config.View, combatUnitRoot.ViewContainer);

        combatUnit.Initialize(config);

        foreach (var bar in combatUnitRoot.StatsDisplayersAtBar)
        {
            bar.Initialize(combatUnit);
        }

        // combatUnitView.Initialize();        

        return combatUnit;
    }
}