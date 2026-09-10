using System;
using System.Linq;
using UnityEngine;

public class CellsField : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    private PillarsShuffler _pillarsShuffler;

    public event Action PillarsShuffled;

    private void Awake()
    {
        _pillarsShuffler = new PillarsShuffler(_cells);
    }

    private void OnEnable()
    {
        foreach (var cell in _cells)
        {
            cell.Occupied += Shuffle;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            cell.Occupied -= Shuffle;
        }
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

    private void Shuffle(IAttachable attachable)
    {
        Pillar pillar = attachable as Pillar;

        if (pillar == null)
            throw new ArgumentNullException($"{nameof(attachable)} must be only the {nameof(Pillar)}. {nameof(pillar)} = {pillar.GetType()}");

        _pillarsShuffler.Shuffle(pillar);
        PillarsShuffled?.Invoke();
    }
}