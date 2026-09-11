public class CombatUnitHealthIndicator : CombatUnitStatIndicator
{
    public override void Initialize(CombatUnit combatUnit)
    {
        base.Initialize(combatUnit);
        Indicator.Initialize(CombatUnit.Health);
    }
}