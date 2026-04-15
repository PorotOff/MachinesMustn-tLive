using System;
using System.Collections.Generic;
using System.Linq;

public class BattlePhase : IPhase
{
    private List<CombatUnit> _warriors;
    private List<CombatUnit> _enemies;

    private AutoBattler _autoBattler;

    private CombatUnit _currentOpponentType;

    public event Action Over;
    public event Action WarriorsDied;
    public event Action EnemiesDied;    

    public BattlePhase(List<CombatUnit> warriors, List<CombatUnit> enemies)
    {
        _warriors = warriors;
        _enemies = enemies;
    }

    public void Start()
    {
        StartBattle(_warriors, _enemies);
    }

    private void StartBattle(List<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        _currentOpponentType = opponents[0];

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

        if (_currentOpponentType is WarriorCombatUnit)
        {
            Over?.Invoke();
            return;
        }

        StartBattle(_enemies, _warriors);
    }

    private void OnOpponentsDied()
    {
        Unsubscribe();

        if (_currentOpponentType is WarriorCombatUnit)
        {
            WarriorsDied?.Invoke();
        }
        else
        {
            EnemiesDied?.Invoke();
        }
    }
}