using System.Collections.Generic;

public class CombatUnitRepository
{
    public CombatUnitRepository(List<CombatUnit> warriors, List<CombatUnit> enemies)
    {
        Warriors = warriors;
        Enemies = enemies;
    }

    public IReadOnlyList<CombatUnit> Warriors { get; }
    public IReadOnlyList<CombatUnit> Enemies { get; }
}