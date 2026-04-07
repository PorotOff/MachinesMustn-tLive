using UnityEngine;

public class CellsField : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    private PillarsShuffler _pillarsShuffler;

    private void Awake()
    {
        _pillarsShuffler = new PillarsShuffler(_cells);
    }

    private void OnEnable()
    {
        foreach (var cell in _cells)
        {
            cell.Attached += Shuffle;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in _cells)
        {
            cell.Attached -= Shuffle;
        }
    }

    private void Shuffle(IAttachable attachable)
    {
        if (attachable is not Pillar pillar)
            return;

        _pillarsShuffler.Shuffle(pillar);
    }
}