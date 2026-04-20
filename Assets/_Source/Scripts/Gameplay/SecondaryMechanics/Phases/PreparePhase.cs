using System;
using System.Collections.Generic;
using UnityEngine;

public class PreparePhase : IPhase
{
    private CellsField _cellsField;
    private int _generalPillarsCount;
    private PillarsBar _pillarsBar;
    private PillarSpawner _pillarSpawner;
    private List<TileConfig> _tileConfigs;

    private int _remainingPillars;
    private int _installedPillars;

    public PreparePhase(CellsField cellsField, int generalPillarsCount, PillarsBar pillarsBar, PillarSpawner pillarSpawner, List<TileConfig> tileConfigs)
    {
        _cellsField = cellsField;
        _generalPillarsCount = generalPillarsCount;
        _pillarsBar = pillarsBar;
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
        _cellsField.CellAttached += OnCellsFieldAttached;
    }

    private void Unsubscribe()
    {
        _cellsField.CellAttached -= OnCellsFieldAttached;
    }

    private void OnCellsFieldAttached()
    {
        _installedPillars++;

        TryClearCellsField();
        TrySpawnPillars();
        TryOverPhase();
    }

    private void TryClearCellsField()
    {
        if (_installedPillars != _generalPillarsCount)
            return;

        _cellsField.Clear();
    }

    private void TrySpawnPillars()
    {
        if (_remainingPillars == 0)
            return;

        if (_pillarsBar.IsEmpty == false)
            return;

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarsBar.Capacity);
        _pillarsBar.TakePillars(pillars);

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