using System;
using System.Collections.Generic;
using System.Linq;

public class WarriorChargingService
{
    public void Charge(List<WarriorCombatUnit> warriors, List<Pillar> pillars)
    {
        warriors.CastExeption();
        pillars.CastExeption();

        foreach (var pillar in pillars)
        {
            WarriorCombatUnit warriorCombatUnit = warriors.FirstOrDefault(warrior => warrior.Config.ID == pillar.TileStack.TopTile.Config.ID);
            warriorCombatUnit.AttackEnergy.Increase(pillar.TileStack.Count);
        }
    }
}