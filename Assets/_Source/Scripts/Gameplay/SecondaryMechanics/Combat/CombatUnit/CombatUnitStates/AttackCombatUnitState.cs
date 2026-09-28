using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class AttackCombatUnitState : ICombatUnitState
{
    private CombatUnit _combatUnit;
    private Attacker _attacker;
    private List<CombatUnit> _opponents;

    public AttackCombatUnitState(CombatUnit combatUnit, Attacker attacker, List<CombatUnit> opponents)
    {
        if (combatUnit == null)
            throw new ArgumentNullException(nameof(combatUnit));

        if (attacker == null)
            throw new ArgumentNullException(nameof(attacker));

        opponents.CastExeption();

        _combatUnit = combatUnit;
        _attacker = attacker;
        _opponents = opponents;
    }

    public void Enter()
    {
        Subscribe();
        Attack();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        foreach (var opponent in _opponents)
        {
            opponent.View.AnimationEvents.TakeDamageAnimationComplete += OnOpponentTakeDamageAnimationComplete;
        }
    }

    private void Unsubscribe()
    {
        foreach (var opponent in _opponents)
        {
            opponent.View.AnimationEvents.TakeDamageAnimationComplete -= OnOpponentTakeDamageAnimationComplete;
        }
    }

    private void OnOpponentTakeDamageAnimationComplete()
    {
        if (IsAllOpponentsIdle() == false)
            return;

        Attack();
    }

    private void Attack()
    {
        if (_combatUnit.AttackEnergy.AvailableAttacks == 0)
        {
            SetIdleCombatUnitState();
            return;
        }

        if (TryGetAliveOpponents(out List<CombatUnit> aliveOpponents) == false)
        {
            SetIdleCombatUnitState();
            return;
        }

        _attacker.Attack(aliveOpponents);
    }

    private void SetIdleCombatUnitState()
    {
        _combatUnit.SetState(new IdleCombatUnitState(_combatUnit));
    }

    private bool TryGetAliveOpponents(out List<CombatUnit> aliveOpponents)
    {
        aliveOpponents = _opponents.Where(opponent => opponent.IsDead == false).ToList();

        if (aliveOpponents.Count > 0)
            return true;

        return false;
    }

    private bool IsAllOpponentsIdle()
    {
        bool hasNotIdleOpponent = _opponents.FirstOrDefault(opponent => opponent.State is not IdleCombatUnitState);

        if (hasNotIdleOpponent)
            return false;

        return true;
    }
}