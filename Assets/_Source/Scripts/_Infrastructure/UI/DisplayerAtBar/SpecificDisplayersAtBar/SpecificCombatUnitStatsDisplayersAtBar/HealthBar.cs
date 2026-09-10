public class HealthBar : CombatUnitStatsDisplayerAtBar
{
    public override void Initialize(CombatUnit combatUnit)
    {
        base.Initialize(combatUnit);
        DisplayerAtBar.Initialize(combatUnit.Health);
    }
}