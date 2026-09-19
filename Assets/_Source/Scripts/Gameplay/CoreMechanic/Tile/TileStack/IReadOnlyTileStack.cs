using System;

public interface IReadOnlyTileStack
{
    public event Action TilesOver;

    public int Count { get; }
    public Tile TopTile { get; }
    public Tile BottomTile { get; }
    public bool IsEmpty { get; }
    public bool IsMonotypic { get; }
}