using System.Collections.Generic;
using System.Linq;

public class CombatUnitChargingService
{
    public void Charge(IReadOnlyList<CombatUnit> combatUnits, List<Pillar> pillars)
    {
        combatUnits.CastException();
        pillars.CastException();

        List<Pillar> fullPillars = pillars.Where(pillar => pillar.TileStack.Count >= Constants.TilesThreshohldAtPillar).ToList();

        if (fullPillars.Count == 0)
            return;

        List<CombatUnit> aliveCombatUnits = combatUnits.Where(combatUnit => combatUnit.IsDead == false).ToList();

        foreach (var pillar in fullPillars)
        {
            CombatUnit combatUnit = aliveCombatUnits.FirstOrDefault(combatUnit => combatUnit.Config.ID == pillar.TileStack.TopTile.Config.ID);
            combatUnit.AttackEnergy.Increase(pillar.TileStack.Count);
        }

        fullPillars.ForEach(pillar => pillar.Release());
    }
}