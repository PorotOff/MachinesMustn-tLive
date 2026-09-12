public class PillarShuffler
{
    private PillarFinder _pillarFinder;

    public PillarShuffler(Cell[] cells)
    {
        _pillarFinder = new PillarFinder(cells);
    }

    public void Shuffle(Pillar attachedPillar)
    {
        if (attachedPillar.TileStack.IsMonotypic)
        {
            if (_pillarFinder.TryFindMonotypicPillar(attachedPillar, out Pillar foundPillar) == false)
                return;

            ShuffleTiles(foundPillar, attachedPillar);
        }
        else
        {
            if (TryMakeCombo(attachedPillar))
                return;

            DisassemblePillar(attachedPillar);
        }
    }

    private bool TryMakeCombo(Pillar attachedPillar)
    {
        if (_pillarFinder.TryFindMonotypicPillar(attachedPillar, out Pillar firstFoundPillar) == false)
            return false;

        if (_pillarFinder.TryFindMonotypicPillar(firstFoundPillar, out Pillar secondFoundPillar) == false)
            return false;

        ShuffleTiles(firstFoundPillar, attachedPillar);
        ShuffleTiles(attachedPillar, secondFoundPillar);

        return true;
    }

    private void DisassemblePillar(Pillar attachedPillar)
    {
        for (int i = 0; i < Constants.MaxTilesCountAtPillar; i++)
        {
            if (attachedPillar.TileStack.IsEmpty)
                return;

            if (_pillarFinder.TryFindMonotypicPillar(attachedPillar, out Pillar pillar) == false)
                return;

            if (attachedPillar.TileStack.IsMonotypic)
                return;

            ShuffleTiles(attachedPillar, pillar);
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