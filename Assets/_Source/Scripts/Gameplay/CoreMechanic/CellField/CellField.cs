using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellField : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    
    public event Action<IAttachable> CellOcupied;
    
    public int OccupiedCellsCount => _cells.Count(cell => cell.IsFree == false);

    private void OnEnable()
    {
        foreach (var cell in _cells)
        {
            cell.Occupied += InvokeCellOcupied;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            cell.Occupied -= InvokeCellOcupied;
        }
    }

    public List<Pillar> GetPillars()
    {
        return _cells.Where(cell => cell.IsFree == false).Select(cell => cell.Attachable as Pillar).ToList();
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
        CellOcupied?.Invoke(attachable);
    }
}