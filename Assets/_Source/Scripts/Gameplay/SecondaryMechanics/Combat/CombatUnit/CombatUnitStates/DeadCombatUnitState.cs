using System;

public class DeadCombatUnitState : ICombatUnitState
{
    CombatUnit _combatUnit;

    public DeadCombatUnitState(CombatUnit combatUnit)
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