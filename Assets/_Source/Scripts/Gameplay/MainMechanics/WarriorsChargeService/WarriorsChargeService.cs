using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarriorsChargeService : MonoBehaviour
{
    [SerializeField] private List<WarriorCombatUnit> _warriors; // TEMP: заменить SF на инициализацию
    [SerializeField] private CellsField _cellsField;

    private List<Pillar> _pillars;

    private void Awake()
    {
        _pillars = _cellsField.GetPillars().ToList();
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (var pillar in _pillars)
        {
            pillar.TilesStack.TileAdded += ChargeWarriors;
        }
    }

    private void Unsubscribe()
    {
        foreach (var pillar in _pillars)
        {
            pillar.TilesStack.TileAdded -= ChargeWarriors;
        }
    }

    private void ChargeWarriors()
    {
        List<Pillar> fullPillars = _pillars.Where(pillar => pillar.TilesStack.Count >= Constants.MaxThresholdTilesAtPillar).ToList();

        if (fullPillars.Count == 0)
            return;

        Unsubscribe();

        foreach (var fullPillar in fullPillars)
        {
            WarriorCombatUnit warrior = _warriors.FirstOrDefault(warrior => warrior.Config.ID == fullPillar.TilesStack.TopTile.Config.ID);

            if (warrior == null)
                throw new ArgumentNullException(nameof(warrior));

            warrior.AttackEnergy.Add(fullPillar.TilesStack.Count);
            fullPillar.Release();
        }

        Subscribe();
    }
}

// todo Протестировать перезарядку воинов
// todo Начать делать битву врагов и воинов (пока с ручной инициализацией через SF)