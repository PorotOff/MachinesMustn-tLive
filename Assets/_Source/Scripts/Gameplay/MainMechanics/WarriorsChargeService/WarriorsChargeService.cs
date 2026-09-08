using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarriorsChargeService : MonoBehaviour
{
    [SerializeField] private List<WarriorCombatUnit> _warriors; // TEMP: заменить SF на инициализацию
    [SerializeField] private CellsField _cellsField;

    private List<Pillar> _pillars = new List<Pillar>();

    private void OnEnable()
    {
        _cellsField.CellOccupied += UpdatePillars;
    }

    private void OnDisable()
    {
        _cellsField.CellOccupied -= UpdatePillars;
    }

    private void SubscribeToPillars()
    {
        foreach (var pillar in _pillars)
        {
            pillar.TilesStack.TileAdded += ChargeWarriors;
        }
    }

    private void UnsubscribeFromPillars()
    {
        foreach (var pillar in _pillars)
        {
            pillar.TilesStack.TileAdded -= ChargeWarriors;
        }
    }

    private void UpdatePillars()
    {
        UnsubscribeFromPillars();
        _pillars = _cellsField.GetPillars().ToList();
        SubscribeToPillars();
    }

    private void ChargeWarriors()
    {
        List<Pillar> fullPillars = _pillars.Where(pillar => pillar.TilesStack.Count >= Constants.MaxThresholdTilesAtPillar).ToList();

        // Debug.Log($"Зарядка воинов. Полных столбов: {fullPillars.Count}");

        if (fullPillars.Count == 0)
            return;

        UnsubscribeFromPillars();

        foreach (var fullPillar in fullPillars)
        {
            WarriorCombatUnit warrior = _warriors.FirstOrDefault(warrior => warrior.Config.ID == fullPillar.TilesStack.TopTile.Config.ID);

            // Debug.Log($"Warrior: {warrior.name}");

            if (warrior == null)
                throw new ArgumentNullException(nameof(warrior));

            warrior.AttackEnergy.Add(fullPillar.TilesStack.Count);
            fullPillar.Release();
        }

        SubscribeToPillars();
    }
}

// todo Протестировать перезарядку воинов. ПЕРЕД ЭТИМ ПОДТЯНУТЬ ЛОГИКУ ПОДПИСОК НА НУЖНЫЕ СОБЫТИЯ
// todo Начать делать битву врагов и воинов (пока с ручной инициализацией через SF)