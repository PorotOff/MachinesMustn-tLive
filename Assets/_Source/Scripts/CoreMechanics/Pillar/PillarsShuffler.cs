using UnityEngine;

public class PillarsShuffler
{
    private PillarsFinder _pillarsFinder;

    public PillarsShuffler(Cell[] cells)
    {
        _pillarsFinder = new PillarsFinder(cells);
    }

    public void Shuffle(Pillar attachedPillar)
    {
        if (attachedPillar.TilesStack.IsMonotypic)
        {
            Debug.Log($"{nameof(PillarsShuffler)}: attachedPillar Is Monotypic");

            if (_pillarsFinder.TryFindSameMonotypicPillar(attachedPillar, out Pillar foundPillar) == false)
                return;

            ShuffleTiles(foundPillar, attachedPillar);
        }
        else
        {
            
        }
    }

    public void ShuffleTiles(Pillar pillarGiver, Pillar pillarTaker)
    {
        int tilesCount = pillarGiver.TilesStack.Count;

        for (int i = 0; i < tilesCount; i++)
        {
            pillarTaker.AddTile(pillarGiver.PopTile());
        }
    }
}