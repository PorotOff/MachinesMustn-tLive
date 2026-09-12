using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarriorChargingService : MonoBehaviour
{
    [SerializeField] private CellField _cellField;

    private List<WarriorCombatUnit> _warriors = new List<WarriorCombatUnit>();

    private void OnEnable()
    {
        _cellField.PillarsShuffled += ChargeWarriors;
    }

    private void OnDisable()
    {
        _cellField.PillarsShuffled -= ChargeWarriors;
    }

    public void Initialize(List<WarriorCombatUnit> warriors)
    {
        _warriors = warriors;
    }

    private void ChargeWarriors()
    {
        List<Pillar> pillars = _cellField.GetPillars().ToList();
        List<Pillar> fullPillars = pillars.Where(pillar => pillar.TileStack.Count >= Constants.MaxThresholdTilesAtPillar).ToList();

        if (fullPillars.Count == 0)
            return;

        foreach (var fullPillar in fullPillars)
        {
            WarriorCombatUnit warriorCombatUnit = _warriors.FirstOrDefault(warrior => warrior.Config.ID == fullPillar.TileStack.TopTile.Config.ID);

            if (warriorCombatUnit == null)
                throw new ArgumentNullException(nameof(warriorCombatUnit));

            warriorCombatUnit.AttackEnergy.Increase(fullPillar.TileStack.Count);
            fullPillar.Release();
        }
    }
}