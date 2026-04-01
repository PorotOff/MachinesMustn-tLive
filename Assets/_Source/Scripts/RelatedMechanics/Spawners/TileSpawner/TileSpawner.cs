using UnityEngine;

public class TileSpawner : Spawner<Tile>
{
    public Tile[] Spawn(int count, TileConfig tileConfig)
    {
        Tile[] tiles = new Tile[count];

        for (int i = 0; i < count; i++)
        {
            Tile tile = Spawn();
            tile.Initialize(tileConfig);

            tiles[i] = tile;
        }
        
        return tiles;
    }
}