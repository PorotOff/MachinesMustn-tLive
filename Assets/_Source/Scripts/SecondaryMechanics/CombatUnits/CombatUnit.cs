using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IDamageable
{
    [field: SerializeField] public CombatUnitConfig Config;
    [SerializeField] private HealthDisplayerAtBar _healthDisplayerAtBar;

    private Health _health;
    protected AttackEnergy AttackEnergy;

    public event Action AttackComplete;
    public event Action TakingDamageComplete;

    public IReadOnlyHealth Health => _health;

    protected virtual void Awake()
    {
        _health = new Health();
        AttackEnergy = new AttackEnergy(Config.EnergyStripeCapacity, Config.EnergyStripesCount, 100); // Temp

        _healthDisplayerAtBar.Initialize(_health);
    }

    protected virtual void OnEnable()
    {
        _healthDisplayerAtBar.Subscribe();
    }

    protected virtual void OnDisable()
    {
        _healthDisplayerAtBar.Unsubscribe();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        InvokeTakingDamageComplete();
    }

    public abstract void Attack(List<IDamageable> damageables);

    protected void InvokeAttackComplete()
    {
        AttackComplete?.Invoke();
    }

    protected void InvokeTakingDamageComplete()
    {
        TakingDamageComplete?.Invoke();
    }
}