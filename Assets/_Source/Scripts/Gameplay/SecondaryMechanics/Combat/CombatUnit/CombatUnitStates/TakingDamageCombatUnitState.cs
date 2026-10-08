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
        Subscribe();

        _damageTaker.TakeDamage(_combatUnit, _damage);

        if (_combatUnit.Health.Current <= 0)
        {
            _combatUnit.SetState(new DeadCombatUnitState(_combatUnit));
            return;
        }

        _combatUnit.View.Animator.PlayTakeDamage();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _combatUnit.View.AnimationEvents.TakeDamageAnimationComplete += OnTakeDamageAnimationComplete;
    }

    private void Unsubscribe()
    {
        _combatUnit.View.AnimationEvents.TakeDamageAnimationComplete -= OnTakeDamageAnimationComplete;
    }

    private void OnTakeDamageAnimationComplete()
    {
        _combatUnit.SetState(new IdleCombatUnitState(_combatUnit));
    }
}