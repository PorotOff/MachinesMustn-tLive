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
            pillar.TilesStack.TileAdded += ChargeWarrior;
        }
    }

    private void Unsubscribe()
    {
        foreach (var pillar in _pillars)
        {
            pillar.TilesStack.TileAdded -= ChargeWarrior;
        }
    }

    private void ChargeWarrior()
    {
        List<Pillar> fullPillars = _pillars.Where(pillar => pillar.TilesStack.Count >= Constants.MaxThresholdTilesAtPillar).ToList();

        foreach (var fullPillar in fullPillars)
        {
            WarriorCombatUnit warrior = _warriors.FirstOrDefault(warrior => warrior.Config.ID == fullPillar.TilesStack.TopTile.Config.ID);

            if (warrior == null)
                throw new ArgumentNullException(nameof(warrior));

            // warrior.atta
        }

        Unsubscribe();
        // Берём количество плиток в столбе, ищем нужного война по конфигу плитки, заряжаем война на это число, удаляем столб

        // todo Добавить конфиг плитки или что-то, что поможет найти нужного война для зарядки

        Subscribe();
    }
}