using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarriorsChargeService : MonoBehaviour
{
    [SerializeField] private CellsField _cellsField;

    private List<WarriorCombatUnit> _warriors = new List<WarriorCombatUnit>();

    private void OnEnable()
    {
        _cellsField.PillarsShuffled += ChargeWarriors;
    }

    private void OnDisable()
    {
        _cellsField.PillarsShuffled -= ChargeWarriors;
    }

    public void Initialize(List<WarriorCombatUnit> warriors)
    {
        _warriors = warriors;
    }

    private void ChargeWarriors()
    {
        List<Pillar> pillars = _cellsField.GetPillars().ToList();
        List<Pillar> fullPillars = pillars.Where(pillar => pillar.TilesStack.Count >= Constants.MaxThresholdTilesAtPillar).ToList();

        // Debug.Log($"Зарядка воинов. Полных столбов: {fullPillars.Count}");

        if (fullPillars.Count == 0)
            return;

        foreach (var fullPillar in fullPillars)
        {
            WarriorCombatUnit warriorCombatUnit = _warriors.FirstOrDefault(warrior => warrior.Config.ID == fullPillar.TilesStack.TopTile.Config.ID);

            // Debug.Log($"Warrior: {warrior.name}");

            if (warriorCombatUnit == null)
                throw new ArgumentNullException(nameof(warriorCombatUnit));

            warriorCombatUnit.AttackEnergy.Add(fullPillar.TilesStack.Count);
            fullPillar.Release();
        }
    }
}

// todo Протестировать перезарядку воинов. ПЕРЕД ЭТИМ ПОДТЯНУТЬ ЛОГИКУ ПОДПИСОК НА НУЖНЫЕ СОБЫТИЯ
// todo Начать делать битву врагов и воинов (пока с ручной инициализацией через SF)