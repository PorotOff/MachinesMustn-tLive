using System;

public class IdleCombatUnitState : ICombatUnitState
{
    CombatUnit _combatUnit;

    public IdleCombatUnitState(CombatUnit combatUnit)
    {
        if (combatUnit == null)
            throw new ArgumentNullException(nameof(combatUnit));
            
        _combatUnit = combatUnit;
    }

    public void Enter()
    {
        _combatUnit.View.Animator.SetIdle();
    }

    public void Exit() { }
}