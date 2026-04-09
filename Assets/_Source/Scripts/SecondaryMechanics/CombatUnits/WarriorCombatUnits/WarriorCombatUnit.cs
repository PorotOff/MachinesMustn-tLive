using UnityEngine;

public class WarriorCombatUnit : CombatUnit
{
    [SerializeField] private AttackEnergyView _attackEnergyView;

    protected override void Awake()
    {
        base.Awake();

        _attackEnergyView.Initialize(AttackEnergy);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _attackEnergyView.Subscribe();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _attackEnergyView.Unsubscribe();
    }
}