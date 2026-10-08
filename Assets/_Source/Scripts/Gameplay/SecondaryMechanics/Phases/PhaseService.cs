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

    public event Action WarriorsDied;
    public event Action EnemiesDied;

    public IPhase Phase { get; private set; }

    public PhaseService(CellField cellField, int availableSteps, PillarBar pillarBar, PillarSpawner pillarSpawner, List<CombatUnit> warriors, List<CombatUnit> enemies, MonoBehaviour monoBehaviour)
    {
        _cellField = cellField;
        _availableSteps = availableSteps;
        _pillarBar = pillarBar;
        _pillarSpawner = pillarSpawner;

        _warriors = warriors;
        _enemies = enemies;

        _monoBehaviour = monoBehaviour;

        SetPreparePhase();
    }

    public void SetPreparePhase()
    {
        SetPhase(new PreparePhase(this, _warriors, _cellField, _availableSteps, _pillarBar, _pillarSpawner));
    }

    public void SetBattlePhase()
    {
        SetPhase(new BattlePhase(this, _warriors, _enemies, _monoBehaviour));
    }

    public void InvokeWarriorsDied()
    {
        WarriorsDied?.Invoke();
    }

    public void InvokeEnemiesDied()
    {
        EnemiesDied?.Invoke();
    }

    private void SetPhase(IPhase phase)
    {
        Phase?.Exit();

        Phase = phase;
        Debug.Log($"{nameof(Phase)} = {Phase}");

        Phase.Enter();
    }
}