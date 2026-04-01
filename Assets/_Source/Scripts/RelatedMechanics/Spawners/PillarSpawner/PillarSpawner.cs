using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PillarSpawner : Spawner<Pillar>
{
    [SerializeField] private TileSpawner _tileSpawner;
    [SerializeField, Range(1, 7)] private int _minTilesCount = 1;
    [SerializeField, Range(1, 7)] private int _maxTilesCount = 7;
    [SerializeField, Range(0, 100)] private int _minChance = 95;
    [SerializeField, Range(0, 100)] private int _maxChance = 5;

    private ChanceWeighter _chanceWeighter;

    private List<TileConfig> _tileConfigs;

    public void Initialize(List<TileConfig> tileConfigs)
    {
        _tileConfigs = tileConfigs;
        _chanceWeighter = new ChanceWeighter(_maxChance, _minChance);
    }

    public Pillar[] Spawn(int count)
    {
        Pillar[] pillars = new Pillar[count];

        for (int i = 0; i < count; i++)
        {
            int generalTilesCount = Random.Range(_minTilesCount, _maxTilesCount);
            int finalTileConfigsCount = GetTileConfigsCount(generalTilesCount);
            // Тут должен быть метод получающий новый список tileConfigs

            List<Tile> tiles = SpawnTiles(generalTilesCount, _tileConfigs);

            Pillar pillar = Spawn();
            pillar.Initialize(tiles.ToList());

            StackTilesToPillar(pillar, tiles);

            pillars[i] = pillar;
        }

        return pillars;
    }

    private void StackTilesToPillar(Pillar pillar, List<Tile> tiles)
    {
        int tileIndex = 0;
        tiles[0].StackTo(pillar.transform, pillar.transform.position);
        tileIndex++;

        for (int i = tileIndex; i < tiles.Count; i++)
        {
            Vector3 previousTileAttachPosition = tiles[i - 1].AttachPosition;
            tiles[i].StackTo(pillar.transform, previousTileAttachPosition);
        }
    }

    private List<Tile> SpawnTiles(int count, List<TileConfig> tileConfigs)
    {
        List<Tile> tiles = new List<Tile>();

        for (int i = 1; i <= count; i++)
        {
            int localTilesCount = Random.Range(1, count - i);
            count -= localTilesCount;

            TileConfig tileConfig = GetRandomTileConfig(new List<TileConfig>(tileConfigs));
            tiles.AddRange(_tileSpawner.Spawn(localTilesCount, tileConfig));
        }

        return tiles;
    }

    private int GetTileConfigsCount(int tilesCount)
    {
        int minTileConfigsCount = 1;
        Dictionary<int, int> chancesCounts = _chanceWeighter.GetChances(minTileConfigsCount, _tileConfigs.Count, _minTilesCount, _maxTilesCount, tilesCount);
        int randomChance = Random.Range(0, 100 + 1);

        foreach (var chance in chancesCounts.Keys)
        {
            if (randomChance > chance)
                return Mathf.Min(chancesCounts[chance], tilesCount);
        }

        return minTileConfigsCount;
    }

    private TileConfig GetRandomTileConfig(List<TileConfig> tileConfigs)
    {
        TileConfig tileConfig = tileConfigs[Random.Range(0, tileConfigs.Count)];
        tileConfigs.Remove(tileConfig);

        return tileConfig;
    }
}