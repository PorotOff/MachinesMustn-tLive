using System;
using System.Collections.Generic;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    [SerializeField, Min(0)] private int _generalPillarsCount;
    [SerializeField] private PillarsBar _pillarsBar;
    [SerializeField] private PillarSpawner _pillarSpawner;
    [SerializeField] private List<TileConfig> _tileConfigs;

    private int _remainingPillars;

    public event Action Over;

    private void Awake()
    {
        _pillarSpawner.Initialize(_tileConfigs);
        _remainingPillars = _generalPillarsCount;
    }

    private void Start()
    {
        SpawnPillars();
    }

    private void OnEnable()
    {
        _pillarsBar.CellDetached += SpawnPillars;
    }

    private void OnDisable()
    {
        _pillarsBar.CellDetached -= SpawnPillars;
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
            Over?.Invoke();
        }
    }
}