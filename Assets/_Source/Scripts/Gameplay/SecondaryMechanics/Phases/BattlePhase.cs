using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePhase : IPhase
{
    private List<CombatUnit> _warriors;
    private List<CombatUnit> _enemies;

    private AutoBattler _autoBattler;

    private CombatUnit _currentAttacker;

    public event Action Over;
    public event Action WarriorsDied;
    public event Action EnemiesDied;

    public BattlePhase(List<CombatUnit> warriors, List<CombatUnit> enemies)
    {
        warriors.CastExeption();
        warriors.CastExeption();

        _warriors = warriors;
        _enemies = enemies;
    }

    public void Enter()
    {
        // StartBattle(_warriors, _enemies);
        Over?.Invoke();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _autoBattler.AttackersOver += OnAttackersOver;
        _autoBattler.OpponentsDied += OnOpponentsDied;
    }

    private void Unsubscribe()
    {
        if (_autoBattler == null)
            return;

        _autoBattler.AttackersOver -= OnAttackersOver;
        _autoBattler.OpponentsDied -= OnOpponentsDied;
    }

    private void StartBattle(List<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        _currentAttacker = attackers[0];

        List<CombatUnit> sortedAliveAttackers = attackers.Where(attacker => attacker.IsDied == false).OrderByDescending(attacker => attacker.Config.AttackSpeed).ToList();
        Queue<CombatUnit> aliveAttackersQueue = new Queue<CombatUnit>(sortedAliveAttackers);

        List<CombatUnit> aliveOpponents = opponents.Where(attacker => attacker.IsDied == false).ToList();

        _autoBattler = new AutoBattler(aliveAttackersQueue, aliveOpponents);
        
        Subscribe();
        _autoBattler.StartBattle();
    }

    private void OnAttackersOver()
    {
        if (_currentAttacker is EnemyCombatUnit)
        {
            Over?.Invoke();
        }
        else
        {
            StartBattle(_enemies, _warriors);
        }
    }

    private void OnOpponentsDied()
    {
        if (_currentAttacker is EnemyCombatUnit)
        {
            WarriorsDied?.Invoke();
        }
        else
        {
            EnemiesDied?.Invoke();
        }
    }
}