using System;
using System.Collections.Generic;
using System.Linq;

public class CombatUnitChargingService
{
    public void Charge(List<CombatUnit> warriors, List<Pillar> pillars)
    {
        warriors.CastExeption();
        pillars.CastExeption();

        foreach (var pillar in pillars)
        {
            CombatUnit combatUnit = warriors.FirstOrDefault(unit => unit.Config.ID == pillar.TileStack.TopTile.Config.ID);
            combatUnit.AttackEnergy.Increase(pillar.TileStack.Count);
        }
    }
}