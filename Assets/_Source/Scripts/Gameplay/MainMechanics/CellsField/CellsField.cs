using System;
using UnityEngine;

public class CellsField : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    private PillarsShuffler _pillarsShuffler;

    public event Action CellAttached;

    private void Awake()
    {
        _pillarsShuffler = new PillarsShuffler(_cells);
    }

    private void OnEnable()
    {
        foreach (var cell in _cells)
        {
            cell.Attached += OnCellAttached;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            cell.Attached -= OnCellAttached;
        }
    }

    public void Clear()
    {
        foreach (var cell in _cells)
        {
            cell.Clear();
        }
    }

    private void OnCellAttached(IAttachable attachable)
    {
        Shuffle(attachable);
        CellAttached?.Invoke();
    }

    private void Shuffle(IAttachable attachable)
    {
        if (attachable is not Pillar pillar)
            return;

        _pillarsShuffler.Shuffle(pillar);
    }
}