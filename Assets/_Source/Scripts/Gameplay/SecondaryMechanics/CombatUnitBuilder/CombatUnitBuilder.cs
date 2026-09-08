using Unity.VisualScripting;
using UnityEngine;

public class CombatUnitBuilder : MonoBehaviour
{
    [SerializeField] private CombatUnitRoot _combatUnitRootPrefab;
    [SerializeField] private CombatUnitConfig _combatUnitConfig;

    // todo Реализовать сбор юнита через композицию
    // Вьюшка юнита не должна содержать в себе сразу всё. Можно тоже через композицию добавлять какие-то показатели и для каждого показателя
    // должна быть вьюшка.

    private void Start()
    {
        Build<WoodcutterWarriorCombatUnit>(_combatUnitConfig);
    }

    public CombatUnit Build<T>(CombatUnitConfig config) where T : CombatUnit
    {
        var combatUnitRoot = Instantiate(_combatUnitRootPrefab);
        var combatUnit = combatUnitRoot.AddComponent<T>();
        var combatUnitView = Instantiate(config.View, combatUnitRoot.ViewContainer);

        combatUnitView.Initialize(combatUnitRoot.DisplayersAtBar);        
        combatUnit.Initialize(config, combatUnitView);

        return combatUnit;
    }
}