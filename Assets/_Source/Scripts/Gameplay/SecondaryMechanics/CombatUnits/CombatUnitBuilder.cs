using Unity.VisualScripting;
using UnityEngine;

public class CombatUnitBuilder : Spawner<CombatUnitRoot>
{
    public void Build<T>(CombatUnitConfig config) where T : CombatUnit
    {
        CombatUnitRoot combatUnitRoot = Spawn();
        CombatUnit combatUnit = combatUnitRoot.AddComponent<T>();

        // combatUnit.
    }
}