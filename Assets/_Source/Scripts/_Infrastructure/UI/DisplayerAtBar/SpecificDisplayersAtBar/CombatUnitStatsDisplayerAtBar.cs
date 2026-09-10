using UnityEngine;

public abstract class CombatUnitStatsDisplayerAtBar : MonoBehaviour
{
    [SerializeField] private MinToMaxValueIndicator _indicator;

    protected CombatUnit CombatUnit;

    protected DisplayerAtBar DisplayerAtBar;

    public virtual void Initialize(CombatUnit combatUnit)
    {
        CombatUnit = combatUnit;
        DisplayerAtBar = new DisplayerAtBar(_indicator);
        DisplayerAtBar.Subscribe();
    }

    private void OnEnable()
    {
        DisplayerAtBar?.Subscribe();
    }

    private void OnDisable()
    {
        DisplayerAtBar.Unsubscribe();
    }
}