using System;
using System.Collections.Generic;

public class PreparePhase : IPhase
{
    private CellField _cellField;
    private int _generalPillarsCount;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;
    private List<TileConfig> _tileConfigs;

    private int _remainingPillars;
    private int _installedPillars;

    public PreparePhase(CellField cellField, int generalPillarsCount, PillarBar pillarBar, PillarSpawner pillarSpawner, List<TileConfig> tileConfigs)
    {
        _cellField = cellField;
        _generalPillarsCount = generalPillarsCount;
        _pillarBar = pillarBar;
        _pillarSpawner = pillarSpawner;
        _tileConfigs = tileConfigs;

        _pillarSpawner.Initialize(_tileConfigs);

        Subscribe();
    }

    public event Action Over;

    public void Enter()
    {
        _remainingPillars = _generalPillarsCount;
        TrySpawnPillars();
    }

    private void Subscribe()
    {
        _cellField.PillarsShuffled += OnAnyCellOccupied;
    }

    private void Unsubscribe()
    {
        _cellField.PillarsShuffled -= OnAnyCellOccupied;
    }

    private void OnAnyCellOccupied()
    {
        _installedPillars++;

        TryClearCellField();
        TrySpawnPillars();
        TryOverPhase();
    }

    private void TryClearCellField()
    {
        if (_installedPillars != _generalPillarsCount)
            return;

        _cellField.Clear();
    }

    private void TrySpawnPillars()
    {
        if (_remainingPillars == 0)
            return;

        if (_pillarBar.IsEmpty == false)
            return;

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarBar.Capacity);
        _pillarBar.TakePillars(pillars);

        _remainingPillars -= pillars.Length;
    }

    private void TryOverPhase()
    {
        if (_installedPillars != _generalPillarsCount)
            return;

        Unsubscribe();
        Over?.Invoke();
    }
}