using System;
using System.Collections.Generic;
using UnityEngine;

public class PhasesService : MonoBehaviour
{
    [Header("Prepare phase settings")]
    [SerializeField, Min(0)] private int _generalPillarsCount;
    [SerializeField] private PillarsBar _pillarsBar;
    [SerializeField] private PillarSpawner _pillarSpawner;
    [SerializeField] private List<TileConfig> _tileConfigs;

    [Header("Battle phase settings")]
    [SerializeField] private List<CombatUnit> _warriors;
    [SerializeField] private List<CombatUnit> _enemies;

    private IPhase _currentPhase;

    public event Action WarriorsDied;
    public event Action EnemiesDied;

    private void Awake()
    {
        SetPhase(new PreparePhase(_generalPillarsCount, _pillarsBar, _pillarSpawner, _tileConfigs));
    }

    private void Subscribe()
    {
        _currentPhase.Over += OnPhaseOver;

        if (_currentPhase is BattlePhase battlePhase)
        {
            battlePhase.WarriorsDied += OnWarriorsDied;
            battlePhase.EnemiesDied += OnEnemiesDied;
        }
    }

    private void Unsubscribe()
    {
        _currentPhase.Over -= OnPhaseOver;

        if (_currentPhase is BattlePhase battlePhase)
        {
            battlePhase.WarriorsDied -= OnWarriorsDied;
            battlePhase.EnemiesDied -= OnEnemiesDied;
        }
    }

    private void SetPhase(IPhase phase)
    {
        _currentPhase = phase;
        Subscribe();        

        _currentPhase.Start();
    }

    private void OnPhaseOver()
    {
        Unsubscribe();
        
        if (_currentPhase is BattlePhase)
        {
            SetPhase(new PreparePhase(_generalPillarsCount, _pillarsBar, _pillarSpawner, _tileConfigs));
        }
        else
        {
            SetPhase(new BattlePhase(_warriors, _enemies));
        }
    }

    private void OnWarriorsDied()
    {
        Unsubscribe();
        WarriorsDied?.Invoke();
    }

    private void OnEnemiesDied()
    {
        Unsubscribe();
        EnemiesDied?.Invoke();
    }
}