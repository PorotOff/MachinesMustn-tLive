using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoCombat
{
    private Queue<CombatUnit> _attackers;
    private List<CombatUnit> _opponents;
    private MonoBehaviour _monoBehaviour;

    private CombatUnit _currentAttacker;
    private Coroutine _coroutine;

    public event Action CombatOver;
    public event Action OpponentsDied;

    public AutoCombat(Queue<CombatUnit> attackers, List<CombatUnit> opponents, MonoBehaviour monoBehaviour)
    {
        _attackers = attackers;
        _opponents = opponents;
        _monoBehaviour = monoBehaviour;
    }

    public void StartCombat()
    {
        _coroutine = _monoBehaviour.StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        while (_attackers.Count > 0)
        {
            _currentAttacker = _attackers.Dequeue();
            _currentAttacker.Attack(_opponents);

            yield return new WaitWhile(() => _currentAttacker.State is AttackCombatUnitState);

            bool isAllOpponentsDead = _opponents.All(opponent => opponent.State is DeadCombatUnitState);

            if (isAllOpponentsDead)
            {
                OpponentsDied?.Invoke();
                yield break;
            }
        }

        CombatOver?.Invoke();
    }
}