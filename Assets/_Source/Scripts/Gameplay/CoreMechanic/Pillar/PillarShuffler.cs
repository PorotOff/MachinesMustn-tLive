using System;

public class PillarShuffler : IReadOnlyPillarShuffler
{
    private PillarFinder _pillarFinder;
    
    public event Action ShuffleOver;

    public PillarShuffler(Cell[] cells)
    {
        _pillarFinder = new PillarFinder(cells);
    }

    public void Shuffle(Pillar pillar)
    {
        try
        {
            if (pillar.TileStack.IsMonotypic)
            {
                if (_pillarFinder.TryFindMonotypicPillar(pillar, out Pillar monotypicPillar) == false)
                    return;

                ShuffleTiles(monotypicPillar, pillar);
            }
            else
            {
                if (TryShuffleCombo(pillar))
                    return;

                DisassemblePillar(pillar);
            }
        }
        finally
        {
            ShuffleOver?.Invoke();
        }
    }

    private bool TryShuffleCombo(Pillar pillar)
    {
        if (_pillarFinder.TryFindMonotypicPillar(pillar, out Pillar firstMonotypicPillar) == false)
            return false;

        if (_pillarFinder.TryFindMonotypicPillar(firstMonotypicPillar, out Pillar secondMonotypicPillar) == false)
            return false;

        ShuffleTiles(firstMonotypicPillar, pillar);
        ShuffleTiles(pillar, secondMonotypicPillar);
        
        return true;
    }

    private void DisassemblePillar(Pillar pillar)
    {
        while (pillar.TileStack.IsMonotypic == false)
        {
            if (_pillarFinder.TryFindMonotypicPillar(pillar, out Pillar monotypicPillar) == false)
                return;

            ShuffleTiles(pillar, monotypicPillar);
        }
    }

    private void ShuffleTiles(Pillar pillarGiver, Pillar pillarTaker)
    {
        int tilesCount = pillarGiver.TileStack.Count;

        for (int i = 0; i < tilesCount; i++)
        {
            if (pillarGiver.TileStack.TopTile.Config.ID != pillarTaker.TileStack.TopTile.Config.ID)
                return;

            pillarTaker.AddTile(pillarGiver.PopTile());
        }
    }
}