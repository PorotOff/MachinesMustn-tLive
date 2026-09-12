using System.Collections.Generic;
using UnityEngine;

public class WaveService : MonoBehaviour
{
    [SerializeField] private int _enemiesPerWave;
    [SerializeField] private List<CombatUnit> _enemies;

    public int WavesRemaining => _enemies.Count / _enemiesPerWave;
}