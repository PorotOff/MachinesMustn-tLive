public abstract class EnemyCombatUnit : CombatUnit
{
    private EnemyCombatUnitView _view;

    public void Initialize(EnemyCombatUnitView view)
    {
        // _view = view;

        // Initialize();
        // _view.Initialize(Health);
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