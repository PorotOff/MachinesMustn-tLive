using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : ScriptableObject
{
    protected CombatUnit CombatUnit { get; private set; }
    protected List<CombatUnit> Opponents { get; private set; }

    public void Initialize(CombatUnit unit)
    {
        if (unit == null)
            throw new ArgumentNullException(nameof(unit));

        CombatUnit = unit;

        Subscribe();
    }

    public void Attack(List<CombatUnit> opponents)
    {
        opponents.CastExeption();
        Opponents = opponents;

        foreach (var opponent in opponents)
        {
            if (opponent.IsDead)
                throw new InvalidOperationException($"{nameof(opponent)} {nameof(opponent.IsDead)} = {opponent.IsDead}.");
        }

        CombatUnit.View.Animator.SetAttack();
    }

    protected abstract void Attack();

    private void Subscribe()
    {
        CombatUnit.View.AnimationEvents.Attacked += Attack;
        CombatUnit.View.AnimationEvents.DieAnimationComplete += OnCombatUnitDieAnimationComplete;
    }

    private void Unsubscribe()
    {
        CombatUnit.View.AnimationEvents.Attacked -= Attack;
        CombatUnit.View.AnimationEvents.DieAnimationComplete -= OnCombatUnitDieAnimationComplete;
    }

    private void OnCombatUnitDieAnimationComplete()
    {
        Unsubscribe();
    }
}