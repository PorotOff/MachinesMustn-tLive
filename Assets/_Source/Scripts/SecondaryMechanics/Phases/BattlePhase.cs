using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePhase : IPhase
{
    private CellsField _cellsField;
    private List<CombatUnit> _warriors;
    private List<CombatUnit> _enemies;

    private AutoBattler _autoBattler;

    private CombatUnit _currentAttackerType;

    public event Action Over;
    public event Action WarriorsDied;
    public event Action EnemiesDied;    

    public BattlePhase(CellsField cellsField, List<CombatUnit> warriors, List<CombatUnit> enemies)
    {
        _cellsField = cellsField;
        _warriors = warriors;
        _enemies = enemies;
    }

    public void Enter()
    {
        _cellsField.Clear();
        StartBattle(_warriors, _enemies);
    }

    private void StartBattle(List<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        _currentAttackerType = attackers[0];

        List<CombatUnit> sortedAliveAttackers = attackers.Where(attacker => attacker.IsDied == false).OrderByDescending(attacker => attacker.Config.AttackSpeed).ToList();
        Queue<CombatUnit> aliveAttackersQueue = new Queue<CombatUnit>(sortedAliveAttackers);

        List<CombatUnit> aliveOpponents = opponents.Where(attacker => attacker.IsDied == false).ToList();

        _autoBattler = new AutoBattler(aliveAttackersQueue, aliveOpponents);        
        _autoBattler.AttackersOver += OnAttackersOver;
        _autoBattler.OpponentsDied += OnOpponentsDied;

        _autoBattler.StartBattle();
    }

    private void Unsubscribe()
    {
        _autoBattler.AttackersOver -= OnAttackersOver;
        _autoBattler.OpponentsDied -= OnOpponentsDied;
    }

    private void OnAttackersOver()
    {
        Unsubscribe();

        if (_currentAttackerType is EnemyCombatUnit)
        {
            Debug.Log($"Враги закончили атаку");
            Over?.Invoke();
            return;
        }

        Debug.Log($"Воины закончили атаку");
        StartBattle(_enemies, _warriors);
    }

    private void OnOpponentsDied()
    {
        Unsubscribe();

        if (_currentAttackerType is EnemyCombatUnit)
        {
            Debug.Log($"Воины проиграли");
            WarriorsDied?.Invoke();
        }
        else
        {
            Debug.Log($"Воины победили");
            EnemiesDied?.Invoke();
        }
    }
}