using System;
using System.Collections;
using UnityEngine;

public class PillarShuffler : IReadOnlyPillarShuffler
{
    private PillarFinder _pillarFinder;
    private float _shuffleInterval;
    private CoroutineService _coroutineService;

    private Coroutine _coroutine;
    
    public event Action ShuffleOver;

    public PillarShuffler(Cell[] cells, float shuffleInterval, CoroutineService coroutineService)
    {
        _pillarFinder = new PillarFinder(cells);
        _shuffleInterval = shuffleInterval;
        _coroutineService = coroutineService;
    }

    public void Shuffle(Pillar pillar)
    {
        try
        {
            if (pillar.TileStack.IsMonotypic)
            {
                if (_pillarFinder.TryFindMonotypicPillar(pillar, out Pillar monotypicPillar) == false)
                    return;
                
                _coroutine = _coroutineService.StartCoroutine(ShuffleTiles(monotypicPillar, pillar));
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

        _coroutine = _coroutineService.StartCoroutine(ShuffleTiles(firstMonotypicPillar, pillar)); // bug: Тут нужно учитывать время шафла тайлов
        _coroutine = _coroutineService.StartCoroutine(ShuffleTiles(pillar, secondMonotypicPillar));
        
        return true;
    }

    private void DisassemblePillar(Pillar pillar)
    {
        while (pillar.TileStack.IsMonotypic == false)
        {
            if (_pillarFinder.TryFindMonotypicPillar(pillar, out Pillar monotypicPillar) == false)
                return;

            _coroutine = _coroutineService.StartCoroutine(ShuffleTiles(pillar, monotypicPillar));
        }
    }

    private IEnumerator ShuffleTiles(Pillar pillarGiver, Pillar pillarTaker)
    {
        var wait = new WaitForSecondsRealtime(_shuffleInterval);
        int tilesCount = pillarGiver.TileStack.Count;

        for (int i = 0; i < tilesCount; i++)
        {
            if (pillarGiver.TileStack.TopTile.Config.ID != pillarTaker.TileStack.TopTile.Config.ID)
                yield break;

            pillarTaker.AddTile(pillarGiver.PopTile());

            yield return wait;
        }
    }
}