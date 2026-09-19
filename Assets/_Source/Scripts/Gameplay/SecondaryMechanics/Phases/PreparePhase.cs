using System;

public class PreparePhase : IPhase
{
    private CellField _cellField;
    private int _availableSteps;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;

    private int _remainingSteps;
    private int _takenSteps;

    public PreparePhase(CellField cellField, int availableSteps, PillarBar pillarBar, PillarSpawner pillarSpawner)
    {
        _cellField = cellField;
        _availableSteps = availableSteps;
        _pillarBar = pillarBar;
        _pillarSpawner = pillarSpawner;
    }

    public event Action Over;

    public void Enter()
    {
        Subscribe();

        _remainingSteps = _availableSteps;
        SpawnPillars();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _cellField.CellOcupied += OnCellOcupied;
        _cellField.PillarShuffler.ShuffleOver += OnShuffleOver;
    }

    private void Unsubscribe()
    {
        if (_cellField == null)
            return;

        _cellField.CellOcupied -= OnCellOcupied;
        _cellField.PillarShuffler.ShuffleOver -= OnShuffleOver;
    }

    private void OnCellOcupied()
    {
        _takenSteps++;
    }

    private void OnShuffleOver()
    {
        if (_takenSteps == _availableSteps)
        {
            _cellField.Clear();
            Over?.Invoke();
        }
        else
        {
            SpawnPillars();
        }
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