using System;
using System.Collections.Generic;

public class PreparePhase : IPhase
{
    private int _generalPillarsCount;
    private PillarsBar _pillarsBar;
    private PillarSpawner _pillarSpawner;
    private List<TileConfig> _tileConfigs;

    private int _remainingPillars;

    public event Action Over;

    public PreparePhase(int generalPillarsCount, PillarsBar pillarsBar, PillarSpawner pillarSpawner, List<TileConfig> tileConfigs)
    {
        _generalPillarsCount = generalPillarsCount;
        _pillarsBar = pillarsBar;
        _pillarSpawner = pillarSpawner;
        _tileConfigs = tileConfigs;

        _pillarSpawner.Initialize(_tileConfigs);
        _remainingPillars = _generalPillarsCount;

        _pillarsBar.CellDetached += SpawnPillars;
    }

    public void Start()
    {
        SpawnPillars();
    }

    public void SpawnPillars()
    {        
        if (_pillarsBar.IsEmpty == false || _remainingPillars == 0)
            return;

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarsBar.Capacity);
        _pillarsBar.TakePillars(pillars);

        _remainingPillars -= pillars.Length;

        if (_remainingPillars == 0)
        {
            _pillarsBar.CellDetached -= SpawnPillars;
            Over?.Invoke();
        }
    }
}