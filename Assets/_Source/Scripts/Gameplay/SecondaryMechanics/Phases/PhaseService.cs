using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhaseService
{
    private CellField _cellField;
    private int _availableSteps;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;

    private List<CombatUnit> _warriors;
    private List<CombatUnit> _enemies;

    private MonoBehaviour _monoBehaviour;

    private IPhase _currentPhase;

    public event Action WarriorsDied;
    public event Action EnemiesDied;

    public PhaseService(CellField cellField, int availableSteps, PillarBar pillarBar, PillarSpawner pillarSpawner, List<CombatUnit> warriors, List<CombatUnit> enemies, MonoBehaviour monoBehaviour)
    {
        _cellField = cellField;
        _availableSteps = availableSteps;
        _pillarBar = pillarBar;
        _pillarSpawner = pillarSpawner;

        _warriors = warriors;
        _enemies = enemies;

        _monoBehaviour = monoBehaviour;

        SetPhase(new PreparePhase(_cellField, _availableSteps, _pillarBar, _pillarSpawner));
    }

    private void Subscribe()
    {
        _currentPhase.Over += OnPhaseOver;
    }

    private void Unsubscribe()
    {
        if (_currentPhase == null)
            return;

        _currentPhase.Over -= OnPhaseOver;
    }

    private void OnPhaseOver()
    {
        if (_currentPhase is BattlePhase)
        {
            if (IsAllCombatUnitsDead(_warriors))
            {
                WarriorsDied?.Invoke();
                return;
            }

            if (IsAllCombatUnitsDead(_enemies))
            {
                EnemiesDied?.Invoke();
                return;
            }

            SetPhase(new PreparePhase(_cellField, _availableSteps, _pillarBar, _pillarSpawner));
        }
        else
        {
            SetPhase(new BattlePhase(_warriors, _enemies, _monoBehaviour));
        }
    }

    private void SetPhase(IPhase phase)
    {
        Unsubscribe();
        _currentPhase?.Exit();

        _currentPhase = phase;
        
        Subscribe();
        _currentPhase.Enter();
    }

    private bool IsAllCombatUnitsDead(List<CombatUnit> combatUnits)
    {
        return combatUnits.All(combatUnit => combatUnit.State is DeadCombatUnitState);
    }
}