using System;
using System.Collections.Generic;

public class PreparePhase : IPhase
{
    private int _generalPillarsCount;
    private PillarsBar _pillarsBar;
    private PillarSpawner _pillarSpawner;
    private List<TileConfig> _tileConfigs;

    private int _remainingPillars;

    public PreparePhase(int generalPillarsCount, PillarsBar pillarsBar, PillarSpawner pillarSpawner, List<TileConfig> tileConfigs)
    {
        _generalPillarsCount = generalPillarsCount;
        _pillarsBar = pillarsBar;
        _pillarSpawner = pillarSpawner;
        _tileConfigs = tileConfigs;

        _pillarSpawner.Initialize(_tileConfigs);
        _remainingPillars = _generalPillarsCount;

        _pillarsBar.CellDetached += OnCellDetached;
    }

    public event Action Over;

    public void Enter()
    {
        OnCellDetached();
    }

    public void OnCellDetached()
    {        
        if (_pillarsBar.IsEmpty == false)
            return;

        if (_remainingPillars == 0)
        {
            _pillarsBar.CellDetached -= OnCellDetached;
            Over?.Invoke();
            return;
        }

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarsBar.Capacity);
        _pillarsBar.TakePillars(pillars);

        _remainingPillars -= pillars.Length;
    }
}