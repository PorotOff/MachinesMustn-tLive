using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarriorsChargeService : MonoBehaviour
{
    private CellsField _cellsField;

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
        Unsubscribe();

        // Берём количество плиток в столбе, ищем нужного война по конфигу плитки, заряжаем война на это число, удаляем столб

        // todo Добавить конфиг плитки или что-то, что поможет найти нужного война для зарядки

        Subscribe();
    }
}