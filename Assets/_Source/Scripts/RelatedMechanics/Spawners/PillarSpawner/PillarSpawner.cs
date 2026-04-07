using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PillarSpawner : Spawner<Pillar>
{
    [SerializeField] private TileSpawner _tileSpawner;
    [SerializeField, Range(0, 100)] private int _minChance = 95;
    [SerializeField, Range(0, 100)] private int _maxChance = 5;

    private ChanceWeighter _chanceWeighter;

    private List<TileConfig> _tileConfigs;

    public void Initialize(List<TileConfig> tileConfigs)
    {
        #region Preconditions
            if (tileConfigs == null)
                throw new NullReferenceException(nameof(tileConfigs));
    
            if (tileConfigs.Count <= 0)
                throw new IndexOutOfRangeException(nameof(tileConfigs));
        #endregion

        _tileConfigs = tileConfigs;
        _chanceWeighter = new ChanceWeighter(_maxChance, _minChance);
    }

    public Pillar[] Spawn(int count)
    {
        Pillar[] pillars = new Pillar[count];

        for (int i = 0; i < count; i++)
        {
            int generalTilesCount = UnityEngine.Random.Range(Constants.MinTilesCountAtPillar, Constants.MaxTilesCountAtPillar);
            int finalTileConfigsCount = GetTileConfigsCount(generalTilesCount);

            List<TileConfig> tileConfigs = GetRandomTileConfigs(_tileConfigs, finalTileConfigsCount);
            List<Tile> tiles = SpawnTiles(generalTilesCount, tileConfigs);

            Pillar pillar = Spawn();
            StackTilesToPillar(pillar, tiles);
            pillar.Initialize(tiles.ToList());
            pillars[i] = pillar;
        }

        return pillars;
    }

    private int GetTileConfigsCount(int tilesCount)
    {
        int minTileConfigsCount = 1;
        Dictionary<int, int> chancesCounts = _chanceWeighter.GetChances(minTileConfigsCount, _tileConfigs.Count, Constants.MinTilesCountAtPillar, Constants.MaxTilesCountAtPillar, tilesCount);
        int randomChance = UnityEngine.Random.Range(0, 100 + 1);

        foreach (var chance in chancesCounts.Keys)
        {
            if (randomChance > chance)
                return Mathf.Min(chancesCounts[chance], tilesCount);
        }

        return minTileConfigsCount;
    }

    private List<TileConfig> GetRandomTileConfigs(List<TileConfig> tileConfigs, int count)
    {
        #region Preconditions
            if (tileConfigs == null)
                throw new NullReferenceException(nameof(tileConfigs));
    
            if (tileConfigs.Count <= 0)
                throw new IndexOutOfRangeException(nameof(tileConfigs));

            if (count > tileConfigs.Count)
                throw new InvalidOperationException();
        #endregion

        List<TileConfig> randomTileConfigs = new List<TileConfig>(tileConfigs);

        while (randomTileConfigs.Count > count)
        {
            int randomDeleteableIndex = UnityEngine.Random.Range(0, randomTileConfigs.Count);
            randomTileConfigs.RemoveAt(randomDeleteableIndex);
        }

        return randomTileConfigs;
    }

    private List<Tile> SpawnTiles(int count, List<TileConfig> tileConfigs)
    {
        #region Preconditions
            if (tileConfigs == null)
                throw new NullReferenceException(nameof(tileConfigs));
    
            if (tileConfigs.Count <= 0)
                throw new IndexOutOfRangeException(nameof(tileConfigs));
        #endregion

        List<Tile> spawnedTiles = new List<Tile>();
        List<TileConfig> avaliableTileConfigs = new List<TileConfig>(tileConfigs);
        int tileStacksCount = avaliableTileConfigs.Count;
        int remainingTiles = count;

        for (int i = 0; i < tileStacksCount; i++)
        {
            int oneStackTilesCount = UnityEngine.Random.Range(1, remainingTiles + 1);
            remainingTiles -= oneStackTilesCount;

            TileConfig oneStackTilesConfig = PopRandomTileConfig(avaliableTileConfigs);
            Tile[] oneStackTiles = _tileSpawner.Spawn(oneStackTilesCount, oneStackTilesConfig);
            spawnedTiles.AddRange(oneStackTiles);
        }

        return spawnedTiles;
    }

    private TileConfig PopRandomTileConfig(List<TileConfig> tileConfigs)
    {
        #region Preconditions
            if (tileConfigs == null)
                throw new NullReferenceException(nameof(tileConfigs));
    
            if (tileConfigs.Count <= 0)
                throw new IndexOutOfRangeException(nameof(tileConfigs));
        #endregion

        TileConfig tileConfig = tileConfigs[UnityEngine.Random.Range(0, tileConfigs.Count)];
        tileConfigs.Remove(tileConfig);

        return tileConfig;
    }

    private void StackTilesToPillar(Pillar pillar, List<Tile> tiles)
    {
        #region Preconditions
            if (pillar == null)
                throw new NullReferenceException(nameof(tiles));

            if (tiles == null)
                throw new NullReferenceException(nameof(tiles));
    
            if (tiles.Count <= 0)
                throw new IndexOutOfRangeException(nameof(tiles));
        #endregion

        int tileIndex = 0;
        tiles[0].StackTo(pillar.transform, pillar.transform.position);
        tileIndex++;

        for (int i = tileIndex; i < tiles.Count; i++)
        {
            Vector3 previousTileAttachPosition = tiles[i - 1].AttachPosition;
            tiles[i].StackTo(pillar.transform, previousTileAttachPosition);
        }
    }
}