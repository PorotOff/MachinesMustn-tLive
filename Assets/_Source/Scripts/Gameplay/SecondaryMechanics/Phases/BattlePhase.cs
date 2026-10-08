using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePhase : IPhase
{
    private PhaseService _phaseService;
    private List<CombatUnit> _warriors;
    private List<CombatUnit> _enemies;

    private AutoCombatService _autoCombatService;

    public BattlePhase(PhaseService phaseService, List<CombatUnit> warriors, List<CombatUnit> enemies, MonoBehaviour monoBehaviour)
    {
        warriors.CastExeption();
        enemies.CastExeption();

        if (monoBehaviour == null)
            throw new ArgumentNullException(nameof(monoBehaviour));

        _phaseService = phaseService;
        _warriors = warriors;
        _enemies = enemies;

        _autoCombatService = new AutoCombatService(monoBehaviour);
        AddCombat(_warriors, _enemies);
        AddCombat(_enemies, _warriors);
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
        _autoCombatService.CombatOver += OnCombatOver;
        _autoCombatService.OpponentsDied += OnAnyCombatUnitsDied;
    }

    private void Unsubscribe()
    {
        _autoCombatService.CombatOver -= OnCombatOver;
        _autoCombatService.OpponentsDied -= OnAnyCombatUnitsDied;
    }

    private void OnCombatOver()
    {
        _phaseService.SetPreparePhase();
    }

    private void OnAnyCombatUnitsDied()
    {
        Exit();

        if (IsAllCombatUnitsDead(_warriors))
        {
            _phaseService.InvokeWarriorsDied();
            return;
        }

        if (IsAllCombatUnitsDead(_enemies))
        {
            _phaseService.InvokeEnemiesDied();
            return;
        }
    }

    private bool IsAllCombatUnitsDead(List<CombatUnit> combatUnits)
    {
        return combatUnits.All(combatUnit => combatUnit.State is DeadCombatUnitState);
    }
}