public class PreparePhase : IPhase
{
    private PhaseService _phaseService;
    private CombatUnitRepository _combatUnitRepository;

    private CellField _cellField;
    private IReadOnlyPillarShuffler _pillarShuffler;
    private CombatUnitChargingService _combatUnitChargingService;

    private PreparePhaseConfig _preparePhaseConfig;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;

    private int _remainingSteps;
    private int _takenSteps;

    public PreparePhase
    (
        PhaseService phaseService,
        CombatUnitRepository combatUnitRepository,
        CellField cellField,
        IReadOnlyPillarShuffler pillarShuffler,
        CombatUnitChargingService combatUnitChargingService,
        PreparePhaseConfig preparePhaseConfig,
        PillarBar pillarBar,
        PillarSpawner pillarSpawner
    )
    {
        _phaseService = phaseService;
        _combatUnitRepository = combatUnitRepository;
        _cellField = cellField;
        _pillarShuffler = pillarShuffler;
        _combatUnitChargingService = combatUnitChargingService;
        _preparePhaseConfig = preparePhaseConfig;
        _pillarBar = pillarBar;
        _pillarSpawner = pillarSpawner;

        _remainingSteps = _preparePhaseConfig.AvailableSteps;
        _takenSteps = 0;
    }

    public void Enter()
    {
        Subscribe();
        SpawnPillars();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _cellField.CellOcupied += OnCellOcupied;
        _pillarShuffler.ShuffleOver += OnShuffleOver;
    }

    private void Unsubscribe()
    {
        _cellField.CellOcupied -= OnCellOcupied;
        _pillarShuffler.ShuffleOver -= OnShuffleOver;
    }

    private void OnCellOcupied(IAttachable attachable)
    {
        _takenSteps++;
    }

    private void OnShuffleOver()
    {
        _combatUnitChargingService.Charge(_combatUnitRepository.Warriors, _cellField.GetPillars());

        if (_takenSteps == _preparePhaseConfig.AvailableSteps)
        {
            _cellField.Clear();
            _phaseService.SetBattlePhase();
            return;
        }

        SpawnPillars();
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