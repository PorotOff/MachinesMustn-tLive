using System;
using UnityEngine;
using Zenject;

public class PillarShuffleService : MonoBehaviour
{
    private CellField _cellField;
    private PillarShuffler _pillarShuffler;

    [Inject]
    public void Initialize(CellField cellField, PillarShuffler pillarShuffler)
    {
        _cellField = cellField;
        _pillarShuffler = pillarShuffler;
    }

    private void OnEnable()
    {
        _cellField.CellOcupied += ShufflePillars;
    }

    private void OnDisable()
    {
        _cellField.CellOcupied -= ShufflePillars;
    }

    private void ShufflePillars(IAttachable attachable)
    {
        if (attachable is not Pillar pillar)
            throw new InvalidOperationException($"{nameof(attachable)} must be only the {nameof(Pillar)}. {nameof(attachable)} = {attachable.GetType()}");

        if (_cellField.OccupiedCellsCount <= 1)
            return;

        _pillarShuffler.Shuffle(pillar);
    }
}