public abstract class WarriorCombatUnit : CombatUnit
{
    private WarriorCombatUnitView _view;

    public void Initialize(WarriorCombatUnitConfig config, WarriorCombatUnitView view)
    {
        _view = view;

        Initialize(config);
        _view.Initialize(Health, AttackEnergy);

        Subscribe();
    }

    protected override void Subscribe()
    {
        _view.Subscribe();
    }

    protected override void Unsubscribe()
    {
        _view.Unsubscribe();
    }
}