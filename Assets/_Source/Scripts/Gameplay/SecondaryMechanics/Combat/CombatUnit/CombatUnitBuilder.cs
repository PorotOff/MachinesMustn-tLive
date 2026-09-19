using Unity.VisualScripting;
using UnityEngine;

public class CombatUnitBuilder : MonoBehaviour // todo Монобех тут не нужен (но Instantiate не даст его просто так убрать). И вообще это спавнер, а не билдер
{
    public T Build<T>(CombatUnitRoot combatUnitRootPrefab, Transform instancesContainer, CombatUnitConfig config) where T : CombatUnit
    {
        CombatUnitRoot combatUnitRoot = Instantiate(combatUnitRootPrefab, instancesContainer);
        CombatUnit combatUnit = combatUnitRoot.AddComponent<T>();
        CombatUnitView combatUnitView = Instantiate(config.View, combatUnitRoot.ViewContainer);

        combatUnitView.Initialize();
        combatUnit.Initialize(config, combatUnitView);        
        combatUnitRoot.Indicators.ForEach(indicator => indicator.Initialize(combatUnit));

        return combatUnit as T;
    }
}