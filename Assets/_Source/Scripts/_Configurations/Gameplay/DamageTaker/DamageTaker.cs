using System;

public abstract class DamageTaker
{
    protected CombatUnit CombatUnit { get; private set; }

    public void Initialize(CombatUnit unit)
    {
        if (unit == null)
            throw new ArgumentNullException(nameof(unit));
            
        CombatUnit = unit;
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new InvalidOperationException(nameof(damage));
    }
}