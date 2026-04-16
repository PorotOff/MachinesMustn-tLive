using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattler
{
    private Queue<CombatUnit> _attackers;
    private List<CombatUnit> _opponents;
    private CombatUnit _currentAttacker;

    public event Action AttackersOver;
    public event Action OpponentsDied;

    public AutoBattler(Queue<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        _attackers = attackers;
        _opponents = opponents;
    }

    public void StartBattle()
    {
        Unsubscribe();
        _currentAttacker = _attackers.Dequeue();
        Subscribe();

        _currentAttacker.Attack(_opponents);
    }

    public bool IsOpponentsDied()
    {
        foreach (var opponent in _opponents)
        {
            if (opponent.IsDied == false)
                return false;
        }

        return true;
    }

    private void Subscribe()
    {
        _currentAttacker.AttackComplete += OnCombatUnitBattleOver;

        foreach (var opponent in _opponents)
        {
            opponent.TakingDamageComplete += OnCombatUnitBattleOver;
        }
    }

    private void Unsubscribe()
    {
        if (_currentAttacker != null)
        {
            _currentAttacker.AttackComplete -= OnCombatUnitBattleOver;
        }

        foreach (var opponent in _opponents)
        {
            opponent.TakingDamageComplete -= OnCombatUnitBattleOver;
        }
    }

    private void OnCombatUnitBattleOver()
    {
        if (IsAnybodyBattling())
            return;

        if (IsOpponentsDied())
        {
            OpponentsDied?.Invoke();
            return;
        }

        if (_attackers.Count == 0)
        {
            AttackersOver?.Invoke();
            return;
        }

        StartBattle();
    }

    private bool IsAnybodyBattling()
    {
        if (_currentAttacker.IsBattling)
            return true;
            
        foreach (var opponent in _opponents)
        {
            if (opponent.IsBattling)
                return true;
        }

        return false;
    }
}