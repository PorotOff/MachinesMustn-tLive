using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PillarsBar : MonoBehaviour
{
    [SerializeField] private List<Cell> _spawnCells;

    public event Action CellDetached;

    public int Capacity => _spawnCells.Count;
    public bool IsEmpty => _spawnCells.All(cell => cell.IsFree);

    private void OnEnable()
    {
        foreach(var cell in _spawnCells)
        {
            cell.Detached += InvokeDetached;
        }
    }

    private void OnDisable()
    {
        foreach(var cell in _spawnCells)
        {
            cell.Detached -= InvokeDetached;
        }
    }

    public void TakePillars(Pillar[] pillars)
    {
        for (int i = 0; i < Capacity; i++)
        {
            pillars[i].Attach(_spawnCells[i]);
        }
    }

    private void InvokeDetached()
    {
        CellDetached?.Invoke();
    }
}