using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [Header("Prepare phase settings")]
    [SerializeField] private CellField _cellField;
    [SerializeField, Min(0)] private int _availableSteps;
    [SerializeField] private PillarBar _pillarBar;
    [SerializeField] private PillarSpawner _pillarSpawner;
    [SerializeField] private List<TileConfig> _tileConfigs;

    [Header("Battle phase settings")]
    [SerializeField] private CombatUnitRoot _warriorRootPrefab;
    [SerializeField] private List<CombatUnitConfig> _warriorConfigs;
    [SerializeField] private CombatUnitRoot _enemyRootPrefab;
    [SerializeField] private List<CombatUnitConfig> _enemieyConfigs;

    [Header("Game settings")]
    [SerializeField] private CombatUnitBuilder _combatUnitBuilder;
    [SerializeField] private Transform _combatUnitInstancesContainer;
    [SerializeField] private CombatField _combatField;

    private PhaseService _phaseService;

    List<WarriorCombatUnit> _warriors;
    List<EnemyCombatUnit> _enemies;

    private void Awake()
    {
        _warriors = CreateCombatUnits<WarriorCombatUnit>(_warriorRootPrefab, _warriorConfigs);
        _enemies = CreateCombatUnits<EnemyCombatUnit>(_enemyRootPrefab, _enemieyConfigs);

        _cellField.Initialize();
        _pillarSpawner.Initialize(_tileConfigs);

        _phaseService  = new PhaseService(
            _cellField,
            _availableSteps,
            _pillarBar,
            _pillarSpawner,
            _warriors.Select(warrior => warrior as CombatUnit).ToList(),
            _enemies.Select(enemy => enemy as CombatUnit).ToList(),
            this);
    }

    private void Start()
    {
        _combatField.TakeCombatUnits(_warriors.Select(warrior => warrior as CombatUnit).ToList());
        _combatField.TakeCombatUnits(_enemies.Select(enemy => enemy as CombatUnit).ToList());
    }

    private void OnEnable()
    {
        _phaseService.EnemiesDied += Win;
        _phaseService.WarriorsDied += Lose;

    }

    private void OnDisable()
    {
        _phaseService.EnemiesDied -= Win;
        _phaseService.WarriorsDied -= Lose;
    }

    private List<T> CreateCombatUnits<T>(CombatUnitRoot combatUnitRootPrefab, List<CombatUnitConfig> combatUnitConfigs) where T : CombatUnit
    {
        List<T> combatUnits = new List<T>();

        foreach (var config in combatUnitConfigs)
        {
            T combatUnit = _combatUnitBuilder.Build<T>(combatUnitRootPrefab, _combatUnitInstancesContainer, config);
            combatUnits.Add(combatUnit);
        }

        return combatUnits;
    }

    private void Win()
    {
        Debug.Log("Победа");
    }

    private void Lose()
    {
        Debug.Log("Поражение");
    }
}