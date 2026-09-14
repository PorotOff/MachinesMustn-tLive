using Unity.VisualScripting;
using UnityEngine;

public class CombatUnitBuilder : MonoBehaviour // todo Монобех тут не нужен (но Instantiate не даст его просто так убрать)
{
    public CombatUnit Build<T>(CombatUnitRoot combatUnitRootPrefab, CombatUnitConfig config) where T : CombatUnit
    {
        var combatUnitRoot = Instantiate(combatUnitRootPrefab);
        var combatUnit = combatUnitRoot.AddComponent<T>();

        combatUnit.Initialize(config);
        combatUnitRoot.Indicators.ForEach(indicator => indicator.Initialize(combatUnit));

        return combatUnit;
    }
}