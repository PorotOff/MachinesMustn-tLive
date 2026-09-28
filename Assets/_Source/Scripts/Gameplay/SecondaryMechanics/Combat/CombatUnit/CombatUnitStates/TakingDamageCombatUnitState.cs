using System;

public class TakingDamageCombatUnitState : ICombatUnitState
{
    private CombatUnit _combatUnit;
    private DamageTaker _damageTaker;
    private int _damage;

    public TakingDamageCombatUnitState(CombatUnit combatUnit, DamageTaker damageTaker, int damage)
    {
        if (combatUnit == null)
            throw new ArgumentNullException(nameof(combatUnit));

        if (damageTaker == null)
            throw new ArgumentNullException(nameof(damageTaker));

        if (damage < 0)
            throw new InvalidOperationException(nameof(damage));

        _combatUnit = combatUnit;
        _damageTaker = damageTaker;
        _damage = damage;
    }

    public void Enter()
    {
        _damageTaker.TakeDamage(_damage);
        _combatUnit.View.Animator.SetTakeDamage();
    }

    public void Exit() { }
}