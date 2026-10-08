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
        _combatUnit.View.Animator.PlayAttack();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _combatUnit.View.AnimationEvents.Attacked += OnAttacked;

        foreach (var opponent in _opponents)
        {
            opponent.View.AnimationEvents.TakeDamageAnimationComplete += Attack;
            opponent.View.AnimationEvents.DieAnimationComplete += Attack;
        }
    }

    private void Unsubscribe()
    {
        _combatUnit.View.AnimationEvents.Attacked -= OnAttacked;

        foreach (var opponent in _opponents)
        {
            opponent.View.AnimationEvents.TakeDamageAnimationComplete -= Attack;
            opponent.View.AnimationEvents.DieAnimationComplete -= Attack;
        }
    }

    private void OnAttacked()
    {
        List<CombatUnit> aliveOpponents = _opponents.Where(opponent => opponent.State is not DeadCombatUnitState).ToList();
        _attacker.Attack(_combatUnit, aliveOpponents);
    }

    private void Attack()
    {
        if (HasAliveOpponents() == false || _combatUnit.AttackEnergy.AvailableAttacks == 0)
        {
            SetIdleCombatUnitState();
            return;
        }

        _combatUnit.View.Animator.PlayAttack();
    }

    private void SetIdleCombatUnitState()
    {
        _combatUnit.SetState(new IdleCombatUnitState(_combatUnit));
    }

    private bool HasAliveOpponents()
    {
        return _opponents.Any(opponent => opponent.State is not DeadCombatUnitState);
    }
}