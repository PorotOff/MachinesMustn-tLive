using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoBattler : MonoBehaviour
{
    private Queue<CombatUnit> _attackers;
    private CombatUnit _currentCombatUnit;

    public event Action BattleOver;

    public void Initialize(Queue<CombatUnit> attackers)
    {
        _attackers = attackers;
    }

    public void StartBattle(List<CombatUnit> opponents)
    {
        _attackers.Clear();
        // _attackers.EnqueueRange();

        LaunchAttack();
    }

    private void LaunchAttack()
    {
        if (_currentCombatUnit != null)
        {
            _currentCombatUnit.TakingDamageComplete -= LaunchAttack;
        }

        if (_attackers.Count == 0)
        {
            BattleOver?.Invoke();
            return;
        }

        _currentCombatUnit = _attackers.Dequeue();
        _currentCombatUnit.TakingDamageComplete += LaunchAttack;

        List<IDamageable> damageables;

        // if (_currentCombatUnit is WarriorCombatUnit warriorCombatUnit)
        // {
        //     damageables = _enemies.Select(enemy => enemy as IDamageable).ToList();
        //     warriorCombatUnit.Attack(damageables);
        // }
        // else if (_currentCombatUnit is EnemyCombatUnit enemyCombatUnit)
        // {
        //     damageables = _attackers.Select(enemy => enemy as IDamageable).ToList();
        //     enemyCombatUnit.Attack(damageables);
        // }
    }

    private bool IsAllDead(List<CombatUnit> combatUnits)
    {
        foreach(var combatUnit in combatUnits)
        {
            if (combatUnit.Health.Current > 0)
                return true;
        }

        return false;
    }
}