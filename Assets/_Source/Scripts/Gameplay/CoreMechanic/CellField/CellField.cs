using System;
using System.Linq;
using UnityEngine;

public class CellField : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private float _shuffleInterval; // temp: вынести из этого класса _pillarShuffler и _shuffleInterval соответственно тоже.
    // Это не его ответственность.

    private PillarShuffler _pillarShuffler;
    
    public event Action CellOcupied;

    public IReadOnlyPillarShuffler PillarShuffler => _pillarShuffler;

    private void OnEnable()
    {
        foreach (var cell in _cells)
        {
            cell.Occupied += InvokeCellOcupied;
            cell.Occupied += Shuffle;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            cell.Occupied -= InvokeCellOcupied;
            cell.Occupied -= Shuffle;
        }
    }

    public void Initialize()
    {
        _pillarShuffler = new PillarShuffler(_cells, _shuffleInterval, this);
    }

    public Pillar[] GetPillars()
    {
        return _cells.Where(cell => cell.IsFree == false).Select(cell => cell.Attachable as Pillar).ToArray();
    }

    public void Clear()
    {
        foreach (var cell in _cells)
        {
            cell.Clear();
        }
    }

    private void InvokeCellOcupied(IAttachable attachable)
    {
        CellOcupied?.Invoke();
    }

    private void Shuffle(IAttachable attachable)
    {
        Pillar pillar = attachable as Pillar;

        if (pillar == null)
            throw new ArgumentNullException($"{nameof(attachable)} must be only the {nameof(Pillar)}. {nameof(pillar)} = {pillar.GetType()}");

        _pillarShuffler.Shuffle(pillar);
    }
}