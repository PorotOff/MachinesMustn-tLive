using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePhase : IPhase
{
    private AutoCombatService _autoCombatService;

    public event Action Over;

    public BattlePhase(List<CombatUnit> warriors, List<CombatUnit> enemies, MonoBehaviour monoBehaviour)
    {
        warriors.CastExeption();
        enemies.CastExeption();

        if (monoBehaviour == null)
            throw new ArgumentNullException(nameof(monoBehaviour));

        _autoCombatService = new AutoCombatService(monoBehaviour);
        AddCombat(warriors, enemies);
        AddCombat(enemies, warriors);
    }

    public void Enter()
    {
        Subscribe();
        _autoCombatService.StartCombat();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void AddCombat(List<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        List<CombatUnit> aliveAttackers = attackers.Where(attacker => attacker.IsDead == false).ToList();
        Queue<CombatUnit> aliveAttackersQueue = new Queue<CombatUnit>(aliveAttackers);

        List<CombatUnit> aliveOpponents = opponents.Where(opponent => opponent.IsDead == false).ToList();
        
        _autoCombatService.AddCombat(aliveAttackersQueue, aliveOpponents);
    }

    private void Subscribe()
    {
        _autoCombatService.CombatOver += InvokeOver;
    }

    private void Unsubscribe()
    {
        _autoCombatService.CombatOver -= InvokeOver;
    }

    private void InvokeOver()
    {
        Over?.Invoke();
    }
}