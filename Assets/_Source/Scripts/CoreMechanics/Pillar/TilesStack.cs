using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilesStack : IReadOnlyTilesStack
{
    private List<Tile> _tiles;

    public TilesStack(List<Tile> tiles)
    {
        _tiles = tiles;
    }

    public event Action TilesOver;

    public int Count => _tiles.Count;
    public Tile TopTile => _tiles[Count - 1];
    public Tile BottomTile => _tiles[0];
    public bool IsEmpty => _tiles.Count == 0;
    public bool IsMonotypic => BottomTile.Config.ID == TopTile.Config.ID;
    public List<Bounds> GeneralBounds => _tiles.Select(tile => tile.Bounds).ToList();

    public void Add(Tile tile)
    {
        _tiles.Add(tile);
    }

    public Tile Pop()
    {
        Tile topTile = TopTile;
        _tiles.Remove(TopTile);

        if (_tiles.Count == 0)
        {
            TilesOver?.Invoke();
        }

        return topTile;
    }

    public Tile Peek()
    {
        return _tiles[_tiles.Count - 1];
    }
}