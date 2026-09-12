using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IPooledObject<CombatUnit>, IDamageable, IPurchasable
{
    private CombatUnitView _view;

    public event Action<CombatUnit> Released;
    public event Action Attacked;
    public event Action TakedDamage;

    public CombatUnitConfig Config { get; private set; }
    public HealthStat Health { get; private set; }
    public AttackEnergyStat AttackEnergy { get; private set; }
    public bool IsDied => Health.Current == 0;
    public bool IsBattling { get; private set; }

    public void Initialize(CombatUnitConfig config)
    {
        Config = config;

        Health = new HealthStat(Config.MinHealth, Config.MaxHealth, Config.Health);
        AttackEnergy = new AttackEnergyStat(Config.EnergyStripeCapacity, Config.EnergyStripesCount, Config.MinAttackEnergy, Config.MaxAttackEnergy, Config.AttackEnergy);

        Subscribe();
    }

    public void Release()
    {
        Unsubscribe();
        Released?.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        IsBattling = true;

        Health.Reduce(damage);

        IsBattling = false;
        InvokeTakedDamage();
    }

    public virtual void Attack(List<CombatUnit> opponents)
    {
        IsBattling = true;

        int randomOpponentIndex = UnityEngine.Random.Range(0, opponents.Count);
        CombatUnit opponent = opponents[randomOpponentIndex];

        opponent.TakeDamage(Config.Damage);
        SpendEnergy(Config.EnergyStripeCapacity);

        IsBattling = false;
        InvokeAttacked();
    }

    protected void SpendEnergy(int amount)
    {
        AttackEnergy.Reduce(amount);
    }

    protected void InvokeAttacked()
    {
        Attacked?.Invoke();
    }

    protected void InvokeTakedDamage()
    {
        TakedDamage?.Invoke();
    }

    protected void Subscribe()
    {
        // _view.Subscribe();
    }

    protected void Unsubscribe()
    {
        // _view.Unsubscribe();
    }
}