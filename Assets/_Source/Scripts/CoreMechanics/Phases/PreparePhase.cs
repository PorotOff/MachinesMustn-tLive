using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    [SerializeField] private PillarsBar _pillarsBar;
    [SerializeField, Min(0)] private int _generalPillarsCount;
    [SerializeField] private PillarSpawner _pillarSpawner;
    [SerializeField] private List<TileConfig> _tileConfigs;

    private void Awake()
    {
        _pillarSpawner.Initialize(_tileConfigs);
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
        if (_pillarsBar.IsEmpty == false)
            return;

        Pillar[] pillars = _pillarSpawner.Spawn(_pillarsBar.Capacity);
        _pillarsBar.TakePillars(pillars);
    }
}