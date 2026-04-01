using System.Linq;

public class PillarsFinder
{
    private Cell[] _cells;

    public PillarsFinder(Cell[] cells)
    {
        _cells = cells;
    }

    public bool TryFindSameMonotypicPillar(Pillar basePillar, out Pillar pillar)
    {
        Pillar[] cellsPillars = GetOtherPillars(basePillar);
        pillar = cellsPillars.FirstOrDefault(cellPillar => cellPillar.TilesStack.IsMonotypic && cellPillar.TilesStack.TopTile.Config.ID == basePillar.TilesStack.TopTile.Config.ID);

        if (pillar == null)
            return false;

        return true;
    }

    private Pillar[] GetOtherPillars(Pillar basePillar)
    {                                        
        return _cells.Where(cell => cell.IsFree == false).Select(cell => cell.Attachable as Pillar).Where(pillar => pillar != basePillar).ToArray();
    }
}