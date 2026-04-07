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
            if (_pillarsFinder.TryFindMonotypicPillar(attachedPillar, out Pillar foundPillar) == false)
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
        if (_pillarsFinder.TryFindMonotypicPillar(attachedPillar, out Pillar firstFoundPillar) == false)
            return false;

        if (_pillarsFinder.TryFindMonotypicPillar(firstFoundPillar, out Pillar secondFoundPillar) == false)
            return false;

        ShuffleTiles(firstFoundPillar, attachedPillar);
        ShuffleTiles(attachedPillar, secondFoundPillar);

        return true;
    }

    private void DisassemblePillar(Pillar attachedPillar)
    {
        for (int i = 0; i < Constants.MaxTilesCountAtPillar; i++)
        {
            if (attachedPillar.TilesStack.IsEmpty)
                return;

            if (_pillarsFinder.TryFindMonotypicPillar(attachedPillar, out Pillar pillar) == false)
                return;

            if (attachedPillar.TilesStack.IsMonotypic)
                return;

            ShuffleTiles(attachedPillar, pillar);
        }
    }

    private void ShuffleTiles(Pillar pillarGiver, Pillar pillarTaker)
    {
        int tilesCount = pillarGiver.TilesStack.Count;

        for (int i = 0; i < tilesCount; i++)
        {
            if (pillarGiver.TilesStack.TopTile.Config.ID != pillarTaker.TilesStack.TopTile.Config.ID)
                return;

            pillarTaker.AddTile(pillarGiver.PopTile());
        }
    }
}