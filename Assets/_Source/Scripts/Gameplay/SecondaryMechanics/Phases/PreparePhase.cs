using System;
using System.Collections.Generic;
using System.Linq;

public class PreparePhase : IPhase
{
    private PhaseService _phaseService;
    private List<CombatUnit> _warriors;
    private CellField _cellField;
    private int _availableSteps;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;

    private CombatUnitChargingService _combatUnitChargingService = new CombatUnitChargingService();
    private int _remainingSteps;
    private int _takenSteps;

    public PreparePhase(PhaseService phaseService, List<CombatUnit> warriors, CellField cellField, int availableSteps, PillarBar pillarBar, PillarSpawner pillarSpawner)
    {
        _phaseService = phaseService;
        _warriors = warriors;
        _cellField = cellField;
        _availableSteps = availableSteps;
        _pillarBar = pillarBar;
        _pillarSpawner = pillarSpawner;

        Subscribe();
    }

    public void Enter()
    {
        _remainingSteps = _availableSteps;
        SpawnPillars();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _cellField.CellOcupied += OnCellOcupied;
        _cellField.PillarShuffler.ShuffleOver += OnShuffleOver;
    }

    private void Unsubscribe()
    {
        _cellField.CellOcupied -= OnCellOcupied;
        _cellField.PillarShuffler.ShuffleOver -= OnShuffleOver;
    }

    private void OnCellOcupied()
    {
        _takenSteps++;
    }

    private void OnShuffleOver()
    {
        ChargeWarriors();

        if (_takenSteps == _availableSteps)
        {
            _cellField.Clear();
            _phaseService.SetBattlePhase();
            return;
        }
        
        SpawnPillars();
    }

    private void ChargeWarriors()
    {
        List<Pillar> pillars = _cellField.GetPillars().ToList();
        List<Pillar> fullPillars = pillars.Where(pillar => pillar.TileStack.Count >= Constants.MaxThresholdTilesAtPillar).ToList();

        if (fullPillars.Count != 0)
        {
            List<CombatUnit> aliveWarriors = _warriors.Where(warrior => warrior.IsDead == false).ToList();

            _combatUnitChargingService.Charge(aliveWarriors, fullPillars);
            fullPillars.ForEach(pillar => pillar.Release());
        }
    }

    private void SpawnPillars()
    {
        if (_remainingSteps == 0)
            return;

        if (_pillarBar.IsEmpty == false)
            return;

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarBar.Capacity);
        _pillarBar.TakePillars(pillars);

        _remainingSteps -= pillars.Length;
    }
}