public class WarriorCombatUnitView : CombatUnitView
{    
    private AttackEnergyDisplayerAtBar _attackEnergyDisplayerAtBar;

    public void Initialize(HealthDisplayerAtBar healthDisplayerAtBar, AttackEnergyDisplayerAtBar attackEnergyDisplayerAtBar)
    {
        Initialize(healthDisplayerAtBar);
        _attackEnergyDisplayerAtBar = attackEnergyDisplayerAtBar;
    }

    public void Initialize(Health health, AttackEnergy attackEnergy)
    {
        Initialize(health);
        _attackEnergyDisplayerAtBar.Initialize(attackEnergy);
    }

    public override void Subscribe()
    {
        base.Subscribe();
        _attackEnergyDisplayerAtBar.Subscribe();
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        _attackEnergyDisplayerAtBar.Unsubscribe();
    }
}