using UnityEngine;

public class WarriorCombatUnit : CombatUnit
{
    [SerializeField] private AttackEnergyDisplayerAtBar _attackEnergyDisplayerAtBar;

    protected override void Awake()
    {
        base.Awake();

        _attackEnergyDisplayerAtBar.Initialize(AttackEnergy);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _attackEnergyDisplayerAtBar.Subscribe();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _attackEnergyDisplayerAtBar.Unsubscribe();
    }
}