using System;
using System.Collections.Generic;

public class PreparePhase : IPhase
{
    private CellField _cellField;
    private int _generalPillarsCount;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;
    private List<TileConfig> _tileConfigs;

    private int _remainingPillarsCount;
    private int _installedPillarsCount;

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
        _remainingPillarsCount = _generalPillarsCount;
        SpawnPillars();
    }

    private void Subscribe()
    {
        _cellField.CellOcupied += IncreaseInstalledPillarsCount;
        _cellField.PillarShuffler.PillarsShuffled += OnPillarsShuffled;
    }

    private void Unsubscribe()
    {
        _cellField.CellOcupied -= IncreaseInstalledPillarsCount;
        _cellField.PillarShuffler.PillarsShuffled -= OnPillarsShuffled;
    }

    private void IncreaseInstalledPillarsCount()
    {
        _installedPillarsCount++;
    }

    private void OnPillarsShuffled()
    {
        if (_installedPillarsCount != _generalPillarsCount)
        {
            SpawnPillars();
        }
        else
        {
            _cellField.Clear();
            OverPhase();
        }
    }

    private void SpawnPillars()
    {
        if (_remainingPillarsCount == 0)
            return;

        if (_pillarBar.IsEmpty == false)
            return;

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarBar.Capacity);
        _pillarBar.TakePillars(pillars);

        _remainingPillarsCount -= pillars.Length;
    }

    private void OverPhase()
    {
        Unsubscribe();
        Over?.Invoke();
    }
}