using System.Linq;

public class PillarFinder
{
    private Cell[] _cells;

    public PillarFinder(Cell[] cells)
    {
        _cells = cells;
    }

    public bool TryFindMonotypicPillar(int tileConfigID, out Pillar pillar)
    {
        Pillar[] cellsPillars = GetPillars();
        pillar = cellsPillars.FirstOrDefault(cellPillar => cellPillar.TileStack.IsMonotypic && cellPillar.TileStack.TopTile.Config.ID == tileConfigID);

        if (pillar == null)
            return false;

        return true;
    }

    public bool TryFindMonotypicPillar(Pillar basePillar, out Pillar pillar)
    {
        Pillar[] cellsPillars = GetOtherPillars(basePillar);
        pillar = cellsPillars.FirstOrDefault(cellPillar => cellPillar.TileStack.IsMonotypic && cellPillar.TileStack.TopTile.Config.ID == basePillar.TileStack.TopTile.Config.ID);

        if (pillar == null)
            return false;

        return true;
    }

    private Pillar[] GetPillars()
    {
        return _cells.Where(cell => cell.IsFree == false).Select(cell => cell.Attachable as Pillar).ToArray();
    }

    private Pillar[] GetOtherPillars(Pillar basePillar)
    {                                        
        return _cells.Where(cell => cell.IsFree == false).Select(cell => cell.Attachable as Pillar).Where(pillar => pillar != basePillar).ToArray();
    }
}